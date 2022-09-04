using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.Dusts
{
    internal interface IOdeDusts : IOde
    {
        /// <summary>
        /// 是否使用独立绘制
        /// </summary>
        public bool UseMyDraw
        {
            get => false;
        }
        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="self"></param>
        /// <param name="dust"></param>
        /// <param name="alpha"></param>
        /// <param name="scale"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(ModDust self, Dust dust, Color alpha, float scale, SpriteBatch spriteBatch)
        {

        }
    }
}
