using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace OdeMod.Buffs
{
    /// <summary>
    /// 星锁 没做好效果，银烛用绘制救救吧（
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
            
            //虽然增加buff时检测过了但是不放心在检测一边
            if (!npc.boss)
            {
                for(int i = 0; i < 4; i++)
                {
                    Num += 0.1f;
                    int num = Dust.NewDust(npc.Center + 30 * new Vector2((float)Math.Cos(Num) * 2, (float)Math.Sin(Num)).RotatedBy(0.75f), 1, 1, DustID.BlueCrystalShard, 0, 0, 0, default, 0.8f);
                    Main.dust[num].noGravity = true;
                    Main.dust[num].velocity *= 0.5f;
                    int num2 = Dust.NewDust(npc.Center - 30 * new Vector2((float)Math.Cos(Num) * 2, (float)Math.Sin(Num)).RotatedBy(-0.75f), 1, 1, DustID.Torch, 0, 0, 0, default, 0.9f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= 0.5f;
                }
                npc.velocity = Vector2.Zero;
                
            }
            base.Update(npc, ref buffIndex);
        }
    }
}
