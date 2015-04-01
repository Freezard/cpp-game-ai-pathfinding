using System;
using System.Collections.Generic;
using System.IO;

// Program.cs
// The main program for pathfinding
// Requires the filename of the map
// as argument
namespace Laboration_2
{
    class Pathfinding
    {
        static void Main(string[] args)
        {
            Console.Write("Enter filename of the map: ");
            String filename = Console.ReadLine();
            if (!File.Exists(filename))
            {
                Console.WriteLine("{0} does not exist.", filename);
                return;
            }
            Console.WriteLine();

            Graph g = new Graph();
            // Load all nodes
            g.BuildNodes(filename);
            // Construct the edges
            g.BuildEdges();

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            TimeSpan elapsed;

            // DFS algorithm
            stopwatch.Start();
            DFS dfs = new DFS(g);
            for (int i = 0; i < 999; i++)
                dfs = new DFS(g);
            elapsed = stopwatch.Elapsed;
            Console.WriteLine("DFS shortest path:");
            PrintPath(dfs.GetPathToTarget());
            Console.WriteLine("\nElapsed time: " + elapsed.TotalSeconds + "\n");

            // BFS algorithm
            stopwatch.Restart();
            BFS bfs = new BFS(g);
            for (int i = 0; i < 999; i++)
                bfs = new BFS(g);
            elapsed = stopwatch.Elapsed;
            Console.WriteLine("BFS shortest path:");            
            PrintPath(bfs.GetPathToTarget());
            Console.WriteLine("\nElapsed time: " + elapsed.TotalSeconds + "\n");

            // Dijkstra's algorithm
            stopwatch.Restart();
            Dijkstras dijkstras = new Dijkstras(g);
            for (int i = 0; i < 999; i++)
                dijkstras = new Dijkstras(g);
            elapsed = stopwatch.Elapsed;
            Console.WriteLine("Dijkstra's shortest path:");           
            PrintPath(dijkstras.GetPathToTarget());
            Console.WriteLine("\nElapsed time: " + elapsed.TotalSeconds + "\n");

            // A* algorithm
            stopwatch.Restart();
            AStar astar = new AStar(g);
            for (int i = 0; i < 999; i++)
                astar = new AStar(g);
            elapsed = stopwatch.Elapsed;
            Console.WriteLine("A* shortest path:");            
            PrintPath(astar.GetPathToTarget());
            Console.WriteLine("\nElapsed time: " + elapsed.TotalSeconds + "\n");

            Console.ReadLine();
        }

        // Prints all visited edges
        static void PrintEdges(List<Edge> edges)
        {
            foreach (Edge e in edges)
                Console.Write(e.GetFrom().GetRow() + "," + e.GetFrom().GetColumn()
                    + "-" + e.GetTo().GetRow() + "," + e.GetTo().GetColumn() + " ");
        }

        // Prints the shortest path
        static void PrintPath(List<Node> path)
        {
            foreach (Node n in path)
                Console.Write(n.GetRow() + "," + n.GetColumn() + " ");
        }
    }    
}
