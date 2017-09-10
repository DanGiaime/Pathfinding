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

	private GameObject[,] grid;

	// Use this for initialization
	void Start () {
		grid = new GameObject[height, width];
		Vector3 startPos = tile.transform.position;
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				grid[i, j] = Instantiate (tile, startPos + new Vector3 (i, j, 0), Quaternion.identity);
			}
		}

		Destroy (tile);

	}
	
	// Update is called once per frame
	void Update () {
	}
}
