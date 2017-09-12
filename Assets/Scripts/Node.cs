using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

	private GameObject go;
	private Vector2 location;
	public bool isWall;

	public Node(GameObject go, Vector2 location, bool isWall) {
		this.go = go;
		this.location = location;
		this.isWall = isWall;
	}

	public void visit() {
		this.isWall = true;
		this.go.renderer.material.color = Color.red;

	}
}
