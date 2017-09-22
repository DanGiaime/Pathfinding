using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithms {

	public List<Node> Dijkstras(Graph g) {
		//Construct heap
		//return Search(g, unvisited)
	}

	/// <summary>
	/// Performs Search on the specified graph.
	/// </summary>
	/// <param name="graph">Graph.</param>
	private List<Node> Search(Graph graph, IPriorityQueue<Node, int> unvisited)
    {
		//TODO: error check

		//Initial setup from graph
        List<Node> solution = new List<Node>();
        Node curr = graph.StartNode;
		Node target = graph.TargetNode;

		//Create necessary lists for any pathfinding
		Dictionary<Node, int> visited = new Dictionary<Node, int> ();

		//Set up initial values
		int currDist = 0;
		unvisited.Add (curr, currDist);

		//Loop until we've hit our target
		while(visited.ContainsKey(target) == false) {

			//Grab the next node and it's distance
			KeyValuePair<Node, int> info = unvisited.Next();
			curr = info.Key;
			currDist = info.Value;
			visited.Add (curr, currDist);

			//Add new nodes to unvisited 
			foreach (Node neighbor in curr.Neighbors) {

				//Check if we've seen this node before
				if (!unvisited.ContainsKey (neighbor) && !visited.ContainsKey(neighbor)) {

					//Could add edges here if instead of 1
					unvisited.Add (neighbor, visited[curr] + 1);
				}
			}
		}


        return solution;
    }


}
