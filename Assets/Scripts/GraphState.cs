using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphState {
    public IPriorityQueue<Node, float> unvisited;
    public Dictionary<Node, Node> visited;
    public Dictionary<Node, Node> previous;
    public Dictionary<Node, float> finalDists;
    public Algorithms.Heuristic heuristic;
    public List<Node> solution;
    public Node target;
    public Graph graph;
    public bool isComplete;

    public GraphState(Graph graph, IPriorityQueue<Node, float> unvisited, Dictionary<Node, Node> visited, Dictionary<Node, Node> previous, Dictionary<Node, float> finalDists, Algorithms.Heuristic heuristic, Node target, List<Node> solution = null)
    {
        this.graph = graph;
        this.unvisited = unvisited;
        this.visited = visited;
        this.previous = previous;
        this.finalDists = finalDists;
        this.heuristic = heuristic;
        this.solution = solution;
        this.target = target;
        this.isComplete = false;
    }

}
