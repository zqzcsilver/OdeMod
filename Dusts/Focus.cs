using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.Dusts
{
    internal class Focus : ModDust, IOdeDusts
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.1f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 0.85f;
            dust.alpha = 0;
        }
        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.scale *= 0.972f;
            Lighting.AddLight(dust.position, 0.5f * (float)dust.scale / 2, 0.5f * (float)dust.scale / 2, 0.5f * (float)dust.scale / 2);
            if (dust.scale < 0.4f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}
