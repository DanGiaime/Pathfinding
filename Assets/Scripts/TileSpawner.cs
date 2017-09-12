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

	// Use this for initialization
	void Start () {
		grid = new Node[width, height];
		graph = new Graph ();
		Vector3 startPos = tile.transform.position;
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
			 	GameObject go = Instantiate (tile, startPos + new Vector3 (i, j, 0), Quaternion.identity);
				grid[i, j] = new Node (go, new Vector2(i, j), false);
				graph.addNode (grid[i, j]);
			}
		}

		Destroy (tile);

	}
	
	// Update is called once per frame
	void Update () {
	}
}
