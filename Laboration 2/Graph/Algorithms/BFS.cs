using System.Collections.Generic;

// BFS.cs
// Breadth-first search algorithm
namespace Laboration_2
{
    class BFS
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

        public BFS(Graph g)
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
            Queue<Edge> queue = new Queue<Edge>();

            Edge dummy = new Edge(source, source);

            queue.Enqueue(dummy);

            visited.Add(source);

            while (queue.Count != 0)
            {
                Edge next = queue.Dequeue();

                route[next.GetTo().GetIndex()] = next.GetFrom();

                if (next != dummy)
                    spanningTree.Add(next);               

                if (next.GetTo() == target)
                    return true;

                foreach (Edge e in g.IncidentEdges(next.GetTo()))
                    if (!visited.Contains(e.GetTo()))
                    {
                        queue.Enqueue(e);

                        visited.Add(e.GetTo());
                    }
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
