﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectNode : MonoBehaviour {

	private Node node;

	public Node InternalNode {
		get {return node;}
		set {node = value;}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Toggles isWall of related Node
	/// </summary>
	void OnMouseEnter() {
		if (Input.GetMouseButton(0)) {
			node.resetColor ();
			node.ToggleWall ();
		}
	}

	void OnMouseDown() {
		node.resetColor ();
		node.ToggleWall ();
	}
		
}
