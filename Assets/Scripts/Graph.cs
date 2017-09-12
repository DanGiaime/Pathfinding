using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph {
	List<Node> vertices;

	public Graph() {
		vertices = new List<Node> ();
	}

	public void addNode(Node n) {
		vertices.Add (n);
	}
}
