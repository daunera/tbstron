using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Pathfinding
{
    public class AGrid
    {
        Dictionary<Vector3, Node> nodes = new Dictionary<Vector3, Node>();

        private TileBehaviour[,] tiles;
        private Vector3 backward;

        public AGrid(TileBehaviour[,] tiles, Vector3 backward)
        {
            this.tiles = tiles;
            this.backward = backward;
        }

        Vector3[] neighbourVectrors = new Vector3[]
        {
            Vector3.left,
            Vector3.right,
            Vector3.up,
            Vector3.down,
        };

        public List<Node> getNeighbours(Node node)
        {
            List<Node> neigbours = new List<Node>();
            Vector3 currentNeighbourPosition = Vector3.zero;
            for (int i = 0; i < neighbourVectrors.Length; i++)
            {
                currentNeighbourPosition = node.worldPosition + neighbourVectrors[i];

                if (currentNeighbourPosition != backward)
                {
                    Node neighbour = getNode(currentNeighbourPosition, false);
                    if (node != null)
                    {
                        neigbours.Add(neighbour);
                    }
                }
            }

            return neigbours;
        }

        public Node getNode(Vector3 wordPosition, bool forceEmpty)
        {
            if (!nodes.ContainsKey(wordPosition))
            {
                int x = (int)wordPosition.x;
                int y = (int)wordPosition.y;
                nodes.Add(wordPosition, new Node(wordPosition, forceEmpty ? enTileType.Empty : tiles[x,y].type));
            }

            return nodes[wordPosition];
        }
    }
}
