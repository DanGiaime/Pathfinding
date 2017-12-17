## Pathfinding
Pathfinding is a visualization tool for various pathfinding algorithms written in C# and Unity.
This project was made for an Independent Study of Pathfinding Algorithms.

##### Algorithms
The algorithms this tool is capable of are as follows:
  1. Breadth-First Search (BFS)
  2. Depth-First Search (DFS)
  3. Dijkstra's Algorithm
  4. A*
  5. Iterative-Deepening Depth-First Search (IDDFS)

##### Architecture
This tool is made in Unity for the sake of having a nice visual interface that compiles to WebGL.

The algorithm architecture is modeled to express the similarities in most of the algorithms.
Each algorithm has a high level function of its own name (BFS, DFS, etc.) that calls into a lower level function called Search.

Search is the generalization of all these algorithms, and follows a simple pattern with a few options being handed in.

Search begins at the start node, iterates over all neighbors, adding them to the unvisited structure, updates distances if necessary, then repeats the process on the next node in unvisited until either unvisited is empty or we've found the target node. We keep a predecessors list as we visit nodes, and reconstruct the path at the end of the algorithm.

The options we can hand in are the graph, the unvisited structure, and the heuristic function. The unvisited structures used as of now can be a Stack, a Queue, or a Heap.

The Stack is used for DFS, the Queue is used for BFS, and the Heap is used for both Dijkstra's and A*. The heuristic is used only in A*, and currently is only using Euclidean Distance.

##### Step Function
This architecture is designed around being able to step around through almost every algorithm one iteration at a time to understand how they work. This functionality is available for every algorithm except for IDDFS, as IDDFS is recursive which makes it much more difficult to accurately represent stepwise.

### Post Mortem

###### The Good
This was a really fun project. I feel really comfortable with all of these algorithms now. I think that Unity was a good choice for the visuals, but it definitely caused me a headache once or twice since I tried my best to avoid the actual Unity parts of Unity for this project in particular. On the bright side though, it compiles to WebGL, so that is a huge benefit. Also, learning IDDFS was awesome.

###### The Bad
I definitely wish I had put more time into this. The semester definitely got ahead of me, and I ended up spending more time on algorithms I was already familiar with than I had originally intended. Also, progress was more discrete than continuous. Some days I made huge leaps, some weeks I couldn't give any time to this. In the future I'll definitely try to schedule work more consistently.
