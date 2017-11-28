using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class Algorithms {

    public delegate float Heuristic(Node from, Node to);

    private static float Distance(Node from, Node to) {
		return Vector2.Distance(from.Location, to.Location);
    }

    public static List<Node> IDDFS(Graph g, int length)
    {
        Dictionary<Node, Node> visited;
        for (int i = 0; i < length; i++)
        {
            visited = new Dictionary<Node, Node>();
            Debug.Log("Target: " + g.TargetNode.uid);
            foreach (Node n in g.Vertices)
            {
                n.IsVisited = false;
            }
            List<Node> path = DLS(g.StartNode, g.TargetNode, i);
            Debug.Log("IDDFS " + i);
            if (path != null)
            {
                Debug.Log("Path found");
                return path;
            }
        }

        return null;
    }

    private static List<Node> DLS(Node currNode, Node targetNode, int depth)
    {
        if (depth == 0 && currNode == targetNode) {
            Debug.Log("FOUND");
            currNode.Visit();
            return new List<Node>(){currNode};
        }
        else if (depth > 0)
        {
            List<Node> found;
            currNode.Visit();
            foreach (Node neighbor in currNode.Neighbors)
            {
                if (!neighbor.IsVisited)
                {
                    found = DLS(neighbor, targetNode, depth - 1);
                    if (found != null)
                    {
                        found.Add(currNode);
                        return found;
                    }
                }
            }
        }

        return null;
    }

	/// <summary>
	/// Performs AStar on the given graph.
	/// </summary>
	/// <returns>The star.</returns>
	/// <param name="g">The green component.</param>
	public static List<Node> AStar(Graph g)
	{
		Heap<Node, float> structure = new Heap<Node, float>();
		return Search(g, structure, Distance);
	}

	/// <summary>
	/// Performs Dijkstra's Algorithm on the given graph.
	/// </summary>
	/// <param name="g">The green component.</param>
	public static List<Node> Dijkstras(Graph g)
	{
		Heap<Node, float> structure = new Heap<Node, float>();
		return Search(g, structure, null);
	}

	/// <summary>
	/// Performs a Breadth First Search on the given graph.
	/// </summary>
	/// <param name="g">The green component.</param>
	public static List<Node> BFS(Graph g)
	{
		Queue<Node, float> structure = new Queue<Node, float>();
		return Search(g, structure, null);
	}

	/// <summary>
	/// Performs a Depth First Search on the given graph.
	/// </summary>
	/// <param name="g">The green component.</param>
    public static List<Node> DFS(Graph g) {
        Stack<Node, float> structure = new Stack<Node, float>();
        return Search(g, structure, null);
    }

	/// <summary>
	/// Performs Search on the specified graph.
	/// </summary>
	/// <param name="graph">Graph.</param>
    private static List<Node> Search(Graph graph, IPriorityQueue<Node, float> unvisited, Heuristic heuristic)
    {
		//TODO: error check

		//Initial setup from graph
        List<Node> solution = new List<Node>();
        Node curr = graph.StartNode;
		Node target = graph.TargetNode;

		//Create necessary lists for any pathfinding

        //List of nodes we've visited
		Dictionary<Node, Node> visited = new Dictionary<Node, Node>();

        //List of Node: Node that came before it
		Dictionary<Node, Node> previous = new Dictionary<Node, Node>();

        //Distances of visited Nodes
        Dictionary<Node, float> finalDists = new Dictionary<Node, float>();

		//Set up initial values
		float currDist = 0;
		unvisited.Add (curr, currDist);
		previous[graph.StartNode] = graph.StartNode;

		//Loop until we've hit our target
		while(visited.ContainsKey(target) == false) {

			//Grab the next node and it's distance
			KeyValuePair<Node, float> info = unvisited.Next();
			curr = info.Key;
			currDist = info.Value;
            visited.Add(curr, previous[curr]);
            finalDists.Add (curr, currDist);
            curr.Visit();

			//Add new nodes to unvisited 
			foreach (Node neighbor in curr.Neighbors) {

				//Check if we've seen this node before
				if (!visited.ContainsKey(neighbor)) {

					float heur = (heuristic == null) ? 1 : heuristic(neighbor, target) + 1;

					//Could add edges here if instead of 1
					if (unvisited.ContainsKey(neighbor))
                    {
                        //If Dijkstras/AStar we want to update
                        if (finalDists[curr] + heur < unvisited[neighbor] && unvisited is Heap<Node, int>)
                        {
                            unvisited[neighbor] = finalDists[curr] + heur;
							previous[neighbor] = curr;
						}
                    }
                    else
                    {
                        unvisited.Add(neighbor, finalDists[curr] + heur);
                        previous.Add(neighbor, curr);
                    }
                }
			}

		}

		//Construct actual path
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
