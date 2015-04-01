using System;
using System.Collections.Generic;
using System.IO;

// Graph.cs
// A sparse graph
namespace Laboration_2
{
    class Graph
    {
        // Nodes by position
        private Dictionary<int, Dictionary<int, Node>> rows;
        // Nodes by index
        private Dictionary<int, Node> nodes;
        // All edges in the graph
        private List<Edge> edges;
        // Source node
        private Node source;
        // Target node
        private Node target;
        private int currentRows;
        private int currentCols;
        private int currentNodes;

        public Graph()
        {
            rows = new Dictionary<int, Dictionary<int, Node>>();
            nodes = new Dictionary<int, Node>();
            edges = new List<Edge>();            
        }

        // Inserts a node at specified position, indexed auto
        public Node InsertNode(int row, int column)
        {
            Node n = new Node(currentNodes++, row, column);
            Dictionary<int, Node> cols = rows[row];
            cols.Add(column, n);
            nodes.Add(n.GetIndex(), n);
            return n;
        }

        // Inserts an edge between two nodes
        public void InsertEdge(Node from, Node to)
        {
            Edge e = new Edge(from, to);
            from.AddEdge(e);
            edges.Add(e);
        }

        // Finds a node by its position
        public Node Find(int row, int column)
        {
            Dictionary<int, Node> cols = rows[row];

            if (cols != null)
            {
                if (cols.ContainsKey(column))                               
                    return cols[column];
            }            
            return null;
        }

        // Finds a node by its index
        public Node Find(int index)
        {
            if (nodes.ContainsKey(index))
                return nodes[index];
            else return null;
        }

        // Gets all incident edges to a node
        public List<Edge> IncidentEdges(Node n)
        {
            return n.GetEdges();
        }

        // Gets the opposite node connected to this edge
        public Node Opposite(Node n, Edge e)
        {
            if (e.GetFrom() == n)
                return e.GetTo();
            else if (e.GetTo() == n)
                return e.GetFrom();
            else return null;
        }

        // Gets the source node of this edge
        public Node Origin(Edge e)
        {
            return e.GetFrom();
        }

        // Gets the target node of this edge
        public Node Destination(Edge e)
        {
            return e.GetTo();
        }

        // Returns true if two nodes are connected
        public bool AreAdjacent(Node v, Node w)
        {
            foreach (Edge e in IncidentEdges(v))
                if (e.GetFrom() == w || e.GetTo() == v)
                    return true;
            return false;
        }

        public Node GetSource()
        {
            return source;
        }

        public Node GetTarget()
        {
            return target;
        }

        // Total nodes in the graph
        public int GetCurrentNodes()
        {
            return currentNodes;
        }

        // Removes a node
        public void RemoveNode(Node n)
        {
            foreach (Edge e in IncidentEdges(n))
                edges.Remove(e);
            rows[n.GetRow()].Remove(n.GetColumn());
            nodes.Remove(n.GetIndex());
        }

        // Removes an edge
        public void RemoveEdge(Edge e)
        {
            Node from = Origin(e);
            Node to = Destination(e);
            rows[from.GetRow()].Remove(from.GetColumn());
            rows[to.GetRow()].Remove(to.GetColumn());
            edges.Remove(e);           
        }

        // Returns all nodes in the graph
        public List<Node> Nodes()
        {
            List<Node> nodeList = new List<Node>();
            for (int i = 0; i < currentRows; i++)
                nodeList.AddRange(rows[i].Values);
            return nodeList;
        }

        // Returns all edges in the graph
        public List<Edge> Edges()
        {
            return edges;
        }

        // Inserts every node in a map specified by a filename
        // 0 = walkable, X = unwalkable, S = source, G = target
        public void BuildNodes(String filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("{0} does not exist.", filename);
                return;
            }
            using (StreamReader sr = File.OpenText(filename))
            {
                String input;

                input = sr.ReadLine();
                string[] rowCols = input.Split(',');
                currentRows = int.Parse(rowCols[0]);
                currentCols = int.Parse(rowCols[1]);

                for (int row = 0; row < currentRows; row++)
                {
                    rows[row] = new Dictionary<int, Node>();
                    input = sr.ReadLine();

                    for (int col = 0; col < currentCols; col++)
                    {
                        char symbol = input[col];
                        if (symbol != 'X')
                        {
                            if (symbol == '0')
                                InsertNode(row, col);
                            else if (symbol == 'S')
                                source = InsertNode(row, col);
                            else if (symbol == 'G')
                                target = InsertNode(row, col);
                        }
                    }
                    Console.WriteLine(input);
                }
                Console.WriteLine();
            }
        }

        // Inserts all the edges between connected nodes
        // Directional, two edges for two nodes
        // Pre: BuildNodes()
        public void BuildEdges()
        {
            for (int i = 0; i < currentRows; i++)
                foreach (Node from in rows[i].Values)
                {
                    int column = from.GetColumn();
                    for (int row = i - 1; row <= i + 1; row++)
                    {
                        for (int col = column - 1; col <= column + 1; col++)
                        {
                            Node to = Find(row, col);
                            if (to != null && from != to)
                                InsertEdge(from, to);
                        }
                    }
                }
        }
    }
}