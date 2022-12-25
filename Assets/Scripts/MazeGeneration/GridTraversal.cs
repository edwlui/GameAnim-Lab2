using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace ShareefSoftware
{
    /// Utility class to traverse a grid.
    public class GridTraversal<T>
    {

        private readonly IGridGraph<T> grid;


        private System.Random rnd = new System.Random();
        private Dictionary<(int Row, int Column), (int Row, int Column)> edges = new Dictionary<(int Row, int Column), (int Row, int Column)>();
        private bool[,] visitedCells;
        private int visitedCount = 0;
        private int row, col;

        /// Constructor
        public GridTraversal(IGridGraph<T> grid)
        {
            this.grid = grid;
        }

        /*
         * This code generates the maze by creating a visited cell 2D array and calls Kruskal's Alg
         */
        public IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> GenerateMaze(int startRow, int startColumn)
        {
            /*
             * Implement your maze generation algorithm here
             * Use helper methods as needed
             */
            visitedCells = new bool[grid.NumberOfRows, grid.NumberOfColumns];
            return KruskalsAlg();
        }

        /*
         * This code runs Kruskal's Alg to generate a maze
         */
        private IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> KruskalsAlg()
        {
            GameObject lightGameObject = new GameObject("Lights");
            lightGameObject.transform.parent = GameObject.Find("CreatedMaze").transform;
            // Ensures all cells get visited
            while (visitedCount < visitedCells.Length)
            {
                // Gets random vertice
                row = rnd.Next(grid.NumberOfRows);
                col = rnd.Next(grid.NumberOfColumns);
                if (!visitedCells[row, col])
                {
                    lightPlacer(row, col, lightGameObject);
                    // Runs through all neighbors necessary
                    foreach (var neighbor in grid.Neighbors(row, col))
                    {
                        // Adds initial vertices to edge and visited
                        if (edges.Count == 0)
                        {
                            addEdge((row, col), neighbor);
                            visitedCells[neighbor.Row, neighbor.Column] = true;
                            visitedCount++;
                            yield return ((row, col), neighbor);
                            break;
                        }

                        //Cycle checker
                        if (edges.ContainsKey(neighbor) || edges.ContainsValue((row, col)) && !visitedCells[neighbor.Row, neighbor.Column])
                        {
                            addEdge((row, col), neighbor);
                            yield return ((row, col), neighbor);
                            break;
                        }
                    }
                }
            }
        }

        /*
         * This code adds the edge given
         */
        private void addEdge((int Row, int Column) From, (int Row, int Column) To)
        {
            // Marks vertice as visited and adds edge
            visitedCells[From.Row, From.Column] = true;
            edges.Add(From, To);
            visitedCount++;
        }

        /*
         * This places a light at a 1 in 10 chance at the given cell
         */
        private void lightPlacer(int row, int col, GameObject lightObject)
        {
            if(rnd.Next(9) == 1)
            {
                GameObject singleLight = new GameObject("Light");
                Light light = singleLight.AddComponent<Light>();
                light.intensity = 0.75f;
                singleLight.transform.position = new Vector3(col * 8 + 4, 8, row * 8 + 4);
                light.transform.parent = GameObject.Find(lightObject.name).transform;
            }
        }
    }
}