using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Players;
using OdeMod.ShaderDatas.ScreenShaderDatas;
using OdeMod.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;
using static Terraria.Utils;

namespace OdeMod.NPCs.Boss.DargonDemo
{
    internal class DStar : ModNPC
    {
        public override string Texture => "OdeMod/Items/Misc/Wan";//懒了
        public override void SetDefaults()
        {
            NPC.lifeMax = 100;
            NPC.damage = 1;
            NPC.defense = 1;
            NPC.knockBackResist = 0f;
            NPC.width = 24;
            NPC.height = 24;
            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.alpha = 255;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.TrailCacheLength[NPC.type] = 8;
        }
        float pi = (float)Math.PI;
        public override void AI()
        {
            for(float i = -pi; i< pi; i+= pi / 10)
            {
                Vector2 vel = new Vector2((float)Math.Cos(i), (float)Math.Sin(i)) * 10f;
                //Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vel, ModContent.ProjectileType<Projectiles.Misc.PurpleSteel>(), damage, knockback, player.whoAmI);
            }
            base.AI();
        }
    }
}
