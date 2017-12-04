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

    /// <summary>
    /// When or not the full algorithm should be run (step or no step)
    /// </summary>
    public bool runFullAlgorithm = true;

    /// <summary>
    /// The curr state of the graph
    /// </summary>
    public GraphState currState = null;

    /// <summary>
    /// Path to highlight (the solution)
    /// </summary>
    public List<Node> path;

	static string[] algorithms = new string[] { "BFS", "Iterative DFS", "Dijkstra's", "AStar", "IDDFS", "RecursiveDFS"};
    static Rect algorithmsPosition = new Rect(10, 10, 100, 20 * algorithms.Length);
	public int selected;


	private Node[,] grid;
	private Graph graph;
	private Node currPathNode;
    private Node currGoalNode;

	// Spawn initial tiles and create graph
	void Start () {
        path = null;
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

        path = null;

		// Recreate graph given current walls
		createGraph ();

		//Set up needed initial nodes
		graph.StartNode = currPathNode;
        graph.TargetNode = currGoalNode;

		//Run pathfinding algorithm
		switch(this.selected) {
			case 0:
                currState = Algorithms.BFS (graph, runFullAlgorithm);
				break;
			case 1:
                currState = Algorithms.DFS (graph, runFullAlgorithm);
				break;
			case 2:
                currState = Algorithms.Dijkstras(graph, runFullAlgorithm);
                break;
            case 3:
                currState = Algorithms.AStar(graph, runFullAlgorithm);
                break;
            case 4:
                path = Algorithms.IDDFS(graph, 200);
                break;
            case 5:
                path = Algorithms.RecursiveDFS(graph.StartNode, graph.TargetNode);
                break;
			default:
				path = new List<Node> ();
				break;
		}

        if(path == null) {
            path = currState.solution;
        }

        WritePath();

	}

    void WritePath() {
        //Highlight path
        if (path != null)
        {
            foreach (Node n in path)
            {
                n.HighLight();
            }

            //Denote length of path taken
            Debug.Log("Algorithm: " + algorithms[selected] + ", Length of path taken: " + path.Count);
        }
        else
        {
            Debug.Log("No path found");
        }
    }

	void erasePath() {
		foreach (Node n in graph.Vertices) {
            if (!n.IsWall)
            {
                n.resetColor();
            }
		}
	}

	void OnGUI () {
		this.selected = GUI.SelectionGrid(algorithmsPosition, selected, algorithms, 1, GUI.skin.toggle);

        runFullAlgorithm = GUI.Toggle(new Rect(10, 220, 100, 30), runFullAlgorithm, "Run Full Algorithm");

		if (GUI.Button (new Rect (10, 160, 100, 30), "Run algorithm")) {
			runPath ();
		}

        if (GUI.Button(new Rect(10, 190, 100, 30), "Erase Path"))
        {
            erasePath();
        }


        if (GUI.Button(new Rect(10, 260, 100, 30), "Step"))
        {
            currState = Algorithms.Step(currState);
            path = currState.solution;
            WritePath();
        }
	}
}
