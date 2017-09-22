using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph {
	private List<Node> vertices;
    private Node startNode;
    private Node targetNode;

	public Graph() {
		vertices = new List<Node> ();
	}

	public List<Node> Vertices {
		get { return this.vertices; }
	}

	public void addNode(Node n) {
		vertices.Add (n);
	}

    public Node StartNode
    {
        get { return this.startNode; }
        set { this.startNode = value; }
    }

    public Node TargetNode
    {
        get { return this.targetNode; }
        set { this.targetNode = value; }
    }
}
