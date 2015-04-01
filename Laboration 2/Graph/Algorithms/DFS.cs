using System.Collections.Generic;

// DFS.cs
// Depth-first search algorithm
namespace Laboration_2
{
    class DFS
    {
        private Graph g;
        // List of all visited nodes
        private List<Node> visited;
        // The shortest route to the target
        private Node[] route;
        // All visited edges
        private List<Edge> spanningTree;
        private Node source;
        private Node target;
        bool found;

        public DFS(Graph g)
        {
            this.g = g;
            visited = new List<Node>();
            route = new Node[g.GetCurrentNodes()];
            spanningTree = new List<Edge>();
            source = g.GetSource();
            target = g.GetTarget();
            found = Search();
        }

        // Returns true if target found
        private bool Search()
        {
            Stack<Edge> stack = new Stack<Edge>();

            Edge dummy = new Edge(source, source);

            stack.Push(dummy);

            while (stack.Count != 0)
            {
                Edge next = stack.Pop();

                route[next.GetTo().GetIndex()] = next.GetFrom();

                if (next != dummy)
                    spanningTree.Add(next);

                visited.Add(next.GetTo());

                if (next.GetTo() == target)
                    return true;

                foreach (Edge e in g.IncidentEdges(next.GetTo()))
                    if (!visited.Contains(e.GetTo()))
                        stack.Push(e);
            }

            return false;
        }

        public List<Edge> GetSearchTree()
        {
            return spanningTree;
        }

        public bool Found()
        {
            return found;
        }

        // Returns the shortest path
        public List<Node> GetPathToTarget()
        {
            List<Node> path = new List<Node>();

            if (!found || target == null)
                return path;

            Node n = target;

            path.Add(n);

            while (n != source)
            {
                n = route[n.GetIndex()];

                path.Insert(0, n);
            }

            return path;
        }
    }
}
