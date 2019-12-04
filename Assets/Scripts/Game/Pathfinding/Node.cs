using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Pathfinding
{
    internal enum SearchState
    {
        None,
        Open,
        Closed
    }


    public class Node : IComparable<Node>
    {
        public bool isByPassable;
        public Vector3 worldPosition;

        public int gCost;
        public int hCost;
        public int tCost;
        public int fCost { get { return gCost + hCost + tCost; } set { } }

        public Node Parent;

        internal SearchState SearchState = SearchState.None;

        public int CompareTo(Node nodeToCompare)
        {
            int compare = fCost.CompareTo(nodeToCompare.fCost);
            if (compare == 0)
            {
                compare = hCost.CompareTo(nodeToCompare.hCost);
                if (compare == 0)
                {
                    compare = worldPosition.x.CompareTo(nodeToCompare.worldPosition.x);
                    if (compare == 0)
                    {
                        compare = worldPosition.y.CompareTo(nodeToCompare.worldPosition.y);
                        if (compare == 0)
                        {
                            compare = worldPosition.z.CompareTo(nodeToCompare.worldPosition.z);
                        }
                    }
                }
            }

            return compare;
        }

        public Node(Vector3 pos, enTileType typeType)
        {
            worldPosition = pos;
            isByPassable = typeType != enTileType.Wall;
            tCost = isByPassable ? 0 : 100;
        }

        public static implicit operator Vector3(Node node)
        {
            return node.worldPosition;
        }
    }
}
