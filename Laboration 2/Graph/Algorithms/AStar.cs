using System;
using System.Collections.Generic;
using System.Linq;

// AStar.cs
// A* search algorithm
namespace Laboration_2
{
    class AStar
    {
        private Graph g;
        // Contains the shortest path to every node from the source node
        private Edge[] shortestPathTree;
        // Contains parent edges to each node thathave not yet been added to the SPT
        private Edge[] searchFrontier;
        // Total cost to a node
        private double[] GCosts;
        // G cost + H cost
        private List<double> FCosts;
        private Node source;
        private Node target;

        public AStar(Graph g)
        {
            this.g = g;
            GCosts = new double[g.GetCurrentNodes()];
            FCosts = new List<double>(Enumerable.Repeat<double>(0, g.GetCurrentNodes()));
            shortestPathTree = new Edge[g.GetCurrentNodes()];
            searchFrontier = new Edge[g.GetCurrentNodes()];
            source = g.GetSource();
            target = g.GetTarget();
            Search();
        }

        // Searches for the target
        private void Search()
        {
            // Create a new priority queue with F cost as priority and the number of nodes as size
            PriorityQueue pq = new PriorityQueue(FCosts, g.GetCurrentNodes());

            // Insert the source node
            pq.insert(source.GetIndex());

            // While queue is not empty
            while (!pq.Empty())
            {
                // Get the node on the queue with the lowest cost
                int NextClosestNode = pq.Pop();

                // Move this edge from the frontier to the shortest path tree
                shortestPathTree[NextClosestNode] = searchFrontier[NextClosestNode];

                // If the target has been found exit
                if (NextClosestNode == target.GetIndex())
                    return;

                // For each connected edge
                foreach (Edge e in g.IncidentEdges(g.Find(NextClosestNode)))
                {
                    // Calculate the heuristic cost from this node to the target (H)        
                    double ySeparation = e.GetTo().GetRow() - target.GetRow();
                    double xSeparation = e.GetTo().GetColumn() - target.GetColumn();
                    double HCost = Math.Sqrt(ySeparation * ySeparation + xSeparation * xSeparation);

                    // Calculate the total cost to this node from the source (G)
                    double GCost = GCosts[NextClosestNode] + e.GetCost();

                    // If this edge has never been on the frontier make a note of the cost
                    // to get to the node it points to plus the F cost, then add the edge
                    // to the frontier and the destination node to the queue
                    if (searchFrontier[e.GetTo().GetIndex()] == null)
                    {
                        FCosts[e.GetTo().GetIndex()] = GCost + HCost;
                        GCosts[e.GetTo().GetIndex()] = GCost;

                        pq.insert(e.GetTo().GetIndex());

                        searchFrontier[e.GetTo().GetIndex()] = e;
                    }
                    // Else test to see if the cost to reach the destination node via the
                    // current node is cheaper than the cheapest cost found so far. If
                    // this path is cheaper, we assign the new costs to the destination
                    // node, update its entry in the queue to reflect the change and add the
                    // edge to the frontier
                    else if (GCost < GCosts[e.GetTo().GetIndex()] && shortestPathTree[e.GetTo().GetIndex()] == null)
                    {
                        FCosts[e.GetTo().GetIndex()] = GCost + HCost;
                        GCosts[e.GetTo().GetIndex()] = GCost;

                        pq.ChangePriority(e.GetTo().GetIndex());

                        searchFrontier[e.GetTo().GetIndex()] = e;
                    }
                }
            }
        }

        // Returns the shortest path to the target
        public List<Node> GetPathToTarget()
        {
            List<Node> path = new List<Node>();

            if (target == null)
               return path;

            Node n = target;

            path.Add(n);

            while (n != source && shortestPathTree[n.GetIndex()] != null)
            {
                n = shortestPathTree[n.GetIndex()].GetFrom();

                path.Insert(0, n);
            }

            return path;
        }

        public List<Edge> GetSPT()
        {
            return shortestPathTree.ToList<Edge>();
        }

        public double GetCostToTarget()
        {
            return GCosts[target.GetIndex()];
        }
    }
}
