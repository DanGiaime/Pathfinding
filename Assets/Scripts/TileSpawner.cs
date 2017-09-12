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

	private Node[,] grid;
	private Graph graph;
	private Node currPathNode;

	// Spawn initial tiles and create graph
	void Start () {
		grid = new Node[width, height];
		graph = new Graph ();
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

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				Node currNode = grid [i, j];
				if (j + 1 < height) {
					currNode.AddNode (grid [i, j + 1]);
					grid [i, j + 1].AddNode (currNode);
				}
				if (i + 1 < width) {
					currNode.AddNode (grid [i + 1, j]);
					grid [i + 1, j].AddNode (currNode);
				}
			}
		}

		Destroy (tile);
		currPathNode = grid [0, 0];
			
	}
	
	// Update is called once per frame
	void Update () {

		//Run pathfinding algorithm
//		if (currPathNode != null) {
//			currPathNode.Visit ();
//			currPathNode = currPathNode[0];
//		}
	}

	void OnGUI () {
		
	}
}
