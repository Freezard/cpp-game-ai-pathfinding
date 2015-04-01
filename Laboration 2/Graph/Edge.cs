
// Edge.cs
// Represents an edge in a graph 
namespace Laboration_2
{
    class Edge
    {
        private Node from;
        private Node to;
        private double cost;

        // Cost is 1.4 if diagonal edge else 1
        public Edge(Node from, Node to)
        {
            this.from = from;
            this.to = to;
            if (from.GetRow() != to.GetRow() &&
                from.GetColumn() != to.GetColumn())
                cost = 1.4;
            else cost = 1;
        }

        // Not used
        public static bool operator <(Edge e1, Edge e2)
        {
            int e1Row = e1.to.GetRow();
            int e2Row = e2.to.GetRow();

            if (e1Row < e2Row)
                return true;
            else if (e1Row == e2Row &&
                    e1.to.GetColumn() < e2.to.GetColumn())
                return true;
            else return false;
        }

        // Not used
        public static bool operator >(Edge e1, Edge e2)
        {
            int e1Row = e1.to.GetRow();
            int e2Row = e2.to.GetRow();

            if (e1Row > e2Row)
                return true;
            else if (e1Row == e2Row &&
                    e1.to.GetColumn() > e2.to.GetColumn())
                return true;
            else return false;
        }

        public Node GetFrom()
        {
            return from;
        }

        public Node GetTo()
        {
            return to;
        }

        public double GetCost()
        {
            return cost;
        }
    }
}
