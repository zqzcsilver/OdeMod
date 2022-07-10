using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.Dusts
{
    internal interface IOdeDusts : IOde
    {
        public bool UseMyDraw
        {
            get => false;
        }
        public void Draw(ModDust self, Dust dust, Color alpha, float scale, SpriteBatch spriteBatch)
        {

        }
    }
}
