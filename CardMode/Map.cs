using Microsoft.Xna.Framework;

using OdeMod.CardMode.Rooms;

using System.Collections.Generic;

namespace OdeMod.CardMode
{
    internal class Map
    {
        public void Build()
        {
        }

        public void Destroy()
        {
        }

        public void Update()
        {
        }

        public void Draw()
        {
        }

        private class Node
        {
            public List<Node> Parents, Childs;
            public Point Position;
            public RoomBase Room;

            public Node(RoomBase room)
            {
                Room = room;
                Parents = new List<Node>();
                Childs = new List<Node>();
            }

            public void LinkToChilds(Node node)
            {
                if (!Childs.Contains(node))
                    Childs.Add(node);
            }

            public void LinkToParents(Node node)
            {
                if (!Parents.Contains(node))
                    Parents.Add(node);
            }

            public bool IsLink(Node node)
            {
                return Parents.Contains(node) || Childs.Contains(node);
            }
        }
    }
}