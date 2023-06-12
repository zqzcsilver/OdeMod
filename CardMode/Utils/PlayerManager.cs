using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.PlayerComponents.BaseComponents;
using OdeMod.CardMode.PlayerComponents.LogicComponents;

namespace OdeMod.CardMode.Utils
{
    internal class PlayerManager
    {
        private readonly List<Entity> players;
        public Entity this[int index] => players[index];

        public PlayerManager()
        {
            players = new List<Entity>();
        }

        public static Entity CreatePlayer()
        {
            Entity entity = new Entity(CommonSources.ENTITY_SOURCE_FROM_SYSTEM);
            entity.AddComponent<PlayerComponent>();
            entity.AddComponent<PlayerInfoComponent>();
            entity.AddComponent(CardSystem.Instance.Map.CreateMoveComponent());
            entity.AddComponent<PlayerControlComponent>();
            return entity;
        }

        public void Update(GameTime gt)
        {
            players.ForEach(p => p.Update(gt));
        }

        public void Draw(SpriteBatch sb)
        {
            players.ForEach(p => p.Draw(sb));
        }

        public static bool IsPlayer(Entity entity)
        {
            return entity.HasComponent<PlayerComponent>();
        }

        public void AddPlayer(Entity entity)
        {
            if (IsPlayer(entity) && !players.Contains(entity))
            {
                players.Add(entity);
            }
        }

        public bool RemovePlayer(Entity entity)
        {
            if (IsPlayer(entity) && players.Contains(entity))
            {
                players.Remove(entity);
                return true;
            }
            return false;
        }

        public bool RemovePlayer(int index)
        {
            if (index >= 0 && index < players.Count)
            {
                players.RemoveAt(index);
                return true;
            }
            return false;
        }

        public int GetPlayerIndex(Entity entity)
        {
            if (IsPlayer(entity) && players.Contains(entity))
            {
                return players.IndexOf(entity);
            }
            return -1;
        }
    }
}