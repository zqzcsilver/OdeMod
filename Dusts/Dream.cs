using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace OdeMod.Dusts
{
    public class Dream : ModDust, IOdeDusts
    {
        public override void OnSpawn(Dust dust)
        {
            dust.alpha = 255;
            dust.noLight = true;
            dust.noGravity = true;
            base.OnSpawn(dust);
        }
        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.scale -= 0.02f;
            dust.velocity *= 0.97f;
            if (dust.scale <= 0.1)
                dust.active = false;

            return false;
        }
    }
}
