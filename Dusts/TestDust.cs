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
    internal class TestDust : ModDust, IOdeDusts
    {
        bool IOdeDusts.UseMyDraw => true;
        void IOdeDusts.Draw(ModDust self, Dust dust, Color alpha, float scale, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Items/Series/Sharpsand/纯砂炸弹").Value, 
                dust.position - Main.screenPosition, Color.White);
        }
    }
}
