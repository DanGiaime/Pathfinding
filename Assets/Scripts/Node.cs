using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : System.IEquatable<Node>{

	private GameObject go;
	private Vector2 location;
	private bool isWall;
	private List<Node> neighbors;
	private int loc;
	private bool isVisited;

	/// <summary>
	/// Create a new Node object
	/// </summary>
	/// <param name="go">GameObject associated with this graph Node</param>
	/// <param name="location">Location in grid of this Node</param>
	/// <param name="isWall">If set to <c>true</c> Whether or not this node is a wall</param>
    public Node(GameObject go, Vector2 location, bool isWall) {
		this.go = go;
		this.location = location;
		this.isWall = isWall;
		this.neighbors = new List<Node> ();
		this.loc = 0;
		this.isVisited = false;
	}

	public void Visit() {
		this.go.GetComponent<Renderer> ().material.color = Color.red;
		this.isVisited = true;
	}
		
	public void HighLight() {
		this.go.GetComponent<Renderer> ().material.color = Color.green;
		this.isVisited = true;
	}

	public bool IsVisited {
		get { return this.isVisited; }
		set { this.isVisited = value; }
	}

	public Vector2 Location {
		get { return this.location; }
	}

	public List<Node> Neighbors {
		get { return this.neighbors; }
	}

	public void resetColor() {
		this.go.GetComponent<Renderer> ().material.color = Color.white;
		this.isVisited = false;
	}

	public bool IsWall {
		get { return isWall; }
	}

	public void clearNeighbors() {
		neighbors.Clear ();
	}

	public void ToggleWall() {
		this.isWall = !this.isWall;
		if (this.isWall) {
			this.go.GetComponent<Renderer> ().material.color = Color.black;
			//TODO: Disconnect
		} 
		else {
			this.go.GetComponent<Renderer> ().material.color = Color.white;
			//TODO: Reconnect
		}	
	}

	public void AddNode(Node n) {
		neighbors.Add (n);
	}

    public bool Equals(Node other)
    {
        return (this == other);
    }

    public Node this[int i]
	{
		get { return neighbors[i]; }
		set { neighbors[i] = value; }
	}
}
