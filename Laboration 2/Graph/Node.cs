using System.Collections.Generic;

// Node.cs
// Represents a node in a graph
namespace Laboration_2
{
    class Node
    {
        private int index;
        // Position
        private int row;
        private int column;
        // Edges connected to this node
        private List<Edge> edges;

        public Node(int index, int row, int column)
        {
            this.index = index;
            this.row = row;
            this.column = column;
            edges = new List<Edge>();
        }

        public void AddEdge(Edge edge)
        {
            edges.Add(edge);
        }

        public int GetIndex()
        {
            return index;
        }

        public int GetRow()
        {
            return row;
        }

        public int GetColumn()
        {
            return column;
        }

        public List<Edge> GetEdges()
        {
            return edges;
        }
    }
}
