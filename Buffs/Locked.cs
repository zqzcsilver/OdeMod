using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using OdeMod.Utils;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace OdeMod.Buffs
{
    /// <summary>
    /// 星锁
    /// </summary>
    internal class Locked : ModBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("星锁");
            Description.SetDefault("困");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public float Num = 0;

        public override void Update(NPC npc, ref int buffIndex)
        {
            
            if (!npc.boss)
            {
                npc.velocity = Vector2.Zero;
            }
            if (npc.buffTime[buffIndex] % 60 == 0) 
            {
                Vector2 pVEC = new Vector2(npc.Center.X, Main.screenPosition.Y - Main.rand.Next(50, 100)) +
                   new Vector2(Main.rand.Next(-60, 60), Main.rand.Next(-60, 60));
                Vector2 tVEC = Vector2.Normalize(npc.Center - pVEC) * 3f;
                Projectile.NewProjectile(Entity.GetSource_None(), pVEC, tVEC, ModContent.ProjectileType<Projectiles.Misc.FriendlyStar>(), 15, 0, Main.LocalPlayer.whoAmI);
            }
            base.Update(npc, ref buffIndex);
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            npc.buffTime[buffIndex] = time;
            return true;
        }
    }
}
