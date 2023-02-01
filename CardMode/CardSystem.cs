using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.Rooms;
using OdeMod.Systems;

using Terraria.ModLoader;

namespace OdeMod.CardMode
{
    internal class CardSystem : ModSystem, ICardMode, IOdeSystem
    {
        public bool OpenCardMode = false;
        public static CardSystem Instance => ModContent.GetInstance<CardSystem>();
        public RoomBase FightScene;

        public void Draw(SpriteBatch sb)
        {
            //FightScene?.Draw(sb);
        }
    }
}