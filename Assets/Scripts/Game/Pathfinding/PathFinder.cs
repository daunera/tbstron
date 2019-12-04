using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Pathfinding
{
    public static class PathFinder
    {
        //For characters local level navigation
        public static Vector3? FindPath(TileBehaviour[,] tiles, Vector3 startPosition, Vector3 targetPosition, Vector3 backward)
        {
            startPosition.x = (int)startPosition.x;
            startPosition.y = (int)startPosition.y;
            startPosition.z = (int)startPosition.z;

            targetPosition.x = (int)targetPosition.x;
            targetPosition.y = (int)targetPosition.y;
            targetPosition.z = (int)targetPosition.z;

            backward.x = (int)backward.x;
            backward.y = (int)backward.y;
            backward.z = (int)backward.z;

            AGrid grid = new AGrid(tiles, backward);
            Node startNode = grid.getNode(startPosition, true);
            Node targetNode = grid.getNode(targetPosition, true);
            return FindPath(grid, startNode, targetNode, 0);
        }

        public static Vector3? FindPath(AGrid grid, Node startNode, Node targetNode, int distance = 1)
        {
            SortedList<Node, Node> openSet = new SortedList<Node,Node>();

            openSet.Add(startNode, startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.Values[0];

                currentNode.SearchState = SearchState.Closed;
                openSet.Remove(currentNode);

                if (currentNode != startNode && !currentNode.isByPassable)
                {
                    return null;
                }

                if (getDistance(currentNode, targetNode) <= distance)
                {
                    return RetracePath(startNode, currentNode);
                }

                foreach (Node neighbour in grid.getNeighbours(currentNode))
                {
                    if (neighbour.SearchState == SearchState.Closed)
                        continue;

                    int newMovementCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);

                    if (newMovementCostToNeighbour < neighbour.gCost || neighbour.SearchState != SearchState.Open)
                    {
                        if (neighbour.SearchState == SearchState.Open)
                        {
                            openSet.Remove(neighbour);
                        }
                        else
                        {
                            neighbour.SearchState = SearchState.Open;
                        }

                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = getDistance(neighbour, targetNode);
                        neighbour.Parent = currentNode;

                        openSet.Add(neighbour, neighbour);
                    }
                }
            }

            return null;
        }

        private static int getDistance(Node a, Node b)
        {
            return (int)Math.Abs(Vector3.Distance(a.worldPosition, b.worldPosition));
        }

        private static Vector3 RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();

            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            path.Reverse();

            return path.Select(x => x.worldPosition).ToList()[0];
        }
    }
}
