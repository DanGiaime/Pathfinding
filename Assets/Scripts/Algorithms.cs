using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class Algorithms {

	public static List<Node> Dijkstras(Graph g)
	{
		Heap<Node, int> structure = new Heap<Node, int>();
		return Search(g, structure);
	}

	public static List<Node> BFS(Graph g)
	{
		Queue<Node, int> structure = new Queue<Node, int>();
		return Search(g, structure);
	}

    public static List<Node> DFS(Graph g) {
        Stack<Node, int> structure = new Stack<Node, int>();
        return Search(g, structure);
    }

	/// <summary>
	/// Performs Search on the specified graph.
	/// </summary>
	/// <param name="graph">Graph.</param>
	private static List<Node> Search(Graph graph, IPriorityQueue<Node, int> unvisited)
    {
		//TODO: error check

		//Initial setup from graph
        List<Node> solution = new List<Node>();
        Node curr = graph.StartNode;
		Node target = graph.TargetNode;

		//Create necessary lists for any pathfinding
		Dictionary<Node, Node> visited = new Dictionary<Node, Node>();
		Dictionary<Node, Node> previous = new Dictionary<Node, Node>();
        Dictionary<Node, int> finalDists = new Dictionary<Node, int>();

		//Set up initial values
		int currDist = 0;
		unvisited.Add (curr, currDist);
		previous[graph.StartNode] = graph.StartNode;

		//Loop until we've hit our target
		while(visited.ContainsKey(target) == false) {

			//Grab the next node and it's distance
			KeyValuePair<Node, int> info = unvisited.Next();
			curr = info.Key;
			currDist = info.Value;
            visited.Add(curr, previous[curr]);
            finalDists.Add (curr, currDist);
            curr.Visit();

			//Add new nodes to unvisited 
			foreach (Node neighbor in curr.Neighbors) {

				//Check if we've seen this node before
				if (!visited.ContainsKey(neighbor)) {

                    //Could add edges here if instead of 1
                    if (unvisited.ContainsKey(neighbor))
                    {
						if (finalDists[curr] + 1 < unvisited[neighbor] && unvisited is Heap<Node, int>)
                        {
                            unvisited[neighbor] = finalDists[curr] + 1;
							previous[neighbor] = curr;
						}
                    }
                    else
                    {
                        unvisited.Add(neighbor, finalDists[curr] + 1);
                        previous.Add(neighbor, curr);
                    }
                }
			}

		}

        curr = graph.TargetNode;
        while (curr != graph.StartNode)
        {
            solution.Add(curr);
            curr = visited[curr];
        }
        solution.Add(curr);

        return solution;
    }


}
