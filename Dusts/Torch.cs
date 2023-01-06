using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OdeMod.Dusts
{
    internal class Torch : ModDust, IOdeDusts
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.7f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 0.85f;
        }
        public override bool Update(Dust dust)
        { 

            return false;
        }
    }
}