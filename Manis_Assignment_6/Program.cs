using System;
using System.Diagnostics;
using static System.Console;

public class DijkstrasAlgorithm
{
    private static readonly int NO_PARENT = -1;
    private static void dijkstra(int[,] adjacencyMatrix, int startVertex, int endVertex)
    {
        int LoopCount = 0;  // initiate loop counter

        int nVertices = adjacencyMatrix.GetLength(0);
        int[] shortestDistances = new int[nVertices];
        bool[] added = new bool[nVertices];

        for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
        {
            shortestDistances[vertexIndex] = int.MaxValue;
            added[vertexIndex] = false;
        }

        shortestDistances[startVertex] = 0;
        int[] parents = new int[nVertices];
        parents[startVertex] = NO_PARENT;
        for (int i = 1; i < nVertices; i++)
        {
            int nearestVertex = -1;
            int shortestDistance = int.MaxValue;

            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                if (!added[vertexIndex] && shortestDistances[vertexIndex] < shortestDistance)
                {
                    nearestVertex = vertexIndex;
                    shortestDistance = shortestDistances[vertexIndex];
                }
            }

            added[nearestVertex] = true;
            
            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                int edgeDistance = adjacencyMatrix[nearestVertex, vertexIndex];

                if (edgeDistance > 0 && ((shortestDistance + edgeDistance) < shortestDistances[vertexIndex]))
                {
                    parents[vertexIndex] = nearestVertex;
                    shortestDistances[vertexIndex] = shortestDistance + edgeDistance;

                    LoopCount++;// increment loop counter
                }
            }
        }

        printSolution(startVertex, endVertex, shortestDistances, parents);
        WriteLine();
        Write("Loop Count: " + LoopCount);
    }
    private static void printSolution(int startVertex, int endVertex, int[] distances, int[] parents)
    {
        int nVertices = distances.Length;
        Write("Vertex\t Distance\tPath");

        int vertexIndex = endVertex;          
        {
            if (vertexIndex != startVertex)
            {
                Write("\n" + startVertex + " -> ");
                Write(vertexIndex + " \t\t ");
                Write(distances[vertexIndex] + "\t\t");
                printPath(vertexIndex, parents);
            }
        }
    }
    private static void printPath(int currentVertex, int[] parents)
    {
        if (currentVertex == NO_PARENT)
        {
            return;
        }
        printPath(parents[currentVertex], parents);
        Write(currentVertex + " ");
    }
    public static void Main(String[] args)
    {
        int N = 4;
        Stopwatch sw = new Stopwatch();
        int[] from = { 0, 0, 0, 7 };
        int[] to = {5, 4, 2, 0};
        
        int[,] adjacencyMatrix = { {0,1,0,5,0,2,0,0},  // A = 0
                                    {1,0,0,0,2,0,0,0}, // B = 1
                                    {0,0,0,0,4,0,0,1}, // C = 2
                                    {5,0,0,0,3,0,2,0 }, // D = 3
                                    {0,2,4,3,0,0,2,0 }, // E = 4
                                    {2,0,0,0,0,0,4,0 }, // F = 5
                                    {0,0,0,2,2,4,0,5 }, // G = 6
                                    {0,0,1,0,0,0,5,0} };  // H = 7


        for(int i = 0; i<N; i++)
        {
            sw.Restart();
            dijkstra(adjacencyMatrix, from[i], to[i]);
            sw.Stop();
            double ms = sw.Elapsed.TotalMilliseconds;
            WriteLine();
            WriteLine("Time: " + ms / 4);
            WriteLine();
        }

        ReadLine();
    }
}