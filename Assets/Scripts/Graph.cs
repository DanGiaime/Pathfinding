using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph {
	List<Node> vertices;

	public Graph() {
		vertices = new List<Node> ();
	}

	public List<Node> Vertices {
		get { return this.vertices; }
	}

	public void addNode(Node n) {
		vertices.Add (n);
	}
}
