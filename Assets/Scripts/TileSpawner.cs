using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour {

	/// <summary>
	/// Height of square grid
	/// </summary>
	public int height;

	/// <summary>
	/// Width of square grid
	/// </summary>
	public int width;

	/// <summary>
	/// Tile prefab
	/// </summary>
	public GameObject tile;

	/// <summary>
	/// Scene Manager
	/// </summary>
	public GameObject sceneManager;

	static string[] algorithms = new string[] { "BFS", "DFS", "Dijkstra's" };
	static Rect algorithmsPosition = new Rect(10, 10, 100, 75);
	public int selected;


	private Node[,] grid;
	private Graph graph;
	private Node currPathNode;
    private Node currGoalNode;

	// Spawn initial tiles and create graph
	void Start () {
		this.selected = 0;
		grid = new Node[width, height];
		graph = new Graph ();
		createGrid ();

		Destroy (tile);
		currPathNode = grid [0, 0];
        currGoalNode = grid [width - 1, height -1];	
	}

	/// <summary>
	/// Creates the grid without any internal graph
	/// </summary>
	private void createGrid() {
		Vector3 startPos = tile.transform.position;
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				GameObject go = Instantiate (tile, startPos + new Vector3 (i, j, 0), Quaternion.identity);
				grid[i, j] = new Node (go, new Vector2(i, j), false);
				graph.addNode (grid[i, j]);
				go.AddComponent (typeof(GameObjectNode));
				go.GetComponent<GameObjectNode> ().InternalNode = grid [i, j];

			}
		}
	}

	/// <summary>
	/// Recreates the internal graph, never connecting to walls.
	/// </summary>
	private void createGraph() {

		//Remove existing graph
		foreach(Node n in graph.Vertices) {
			n.clearNeighbors ();
		}

		//Create new attachments
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				Node currNode = grid [i, j];
				if (j + 1 < height) {
					Node nodeAbove = grid [i, j + 1];
					if(!currNode.IsWall && !nodeAbove.IsWall) {
						currNode.AddNode (nodeAbove);
						nodeAbove.AddNode (currNode);
					}
				}
				if (i + 1 < width) {
					Node nodeRight = grid [i + 1, j];
					if (!currNode.IsWall && !nodeRight.IsWall) {
						currNode.AddNode (nodeRight);
						nodeRight.AddNode (currNode);
					}
				}
			}
		}

		currPathNode = grid [0, 0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void runPath () {

		// Recreate graph with current walls
		createGraph ();

		//Run pathfinding algorithm
		graph.StartNode = currPathNode;
        graph.TargetNode = currGoalNode;
		List<Node> path;

		switch(this.selected) {
			case 0:
				path = Algorithms.BFS (graph);
				break;
			case 1:
				path = Algorithms.DFS (graph);
				break;
			case 2:
				path = Algorithms.Dijkstras (graph);
				break;
			default:
				path = new List<Node> ();
				break;
		}
        foreach (Node n in path) {
			n.HighLight();
        }
        Debug.Log(path.Count);
	}

	void erasePath() {
		foreach (Node n in graph.Vertices) {
			if (n.IsVisited) {
				n.resetColor ();
			}
		}
	}

	void OnGUI () {
		this.selected = GUI.SelectionGrid(algorithmsPosition, selected, algorithms, 1, GUI.skin.toggle);

		if (GUI.Button (new Rect (10, 100, 100, 30), "Run algorithm")) {
			runPath ();
		}

		if (GUI.Button (new Rect (10, 130, 100, 30), "Erase Path")) {
			erasePath ();
		}
	}
}
