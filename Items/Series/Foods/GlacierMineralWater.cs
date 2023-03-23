using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using OdeMod.Players;
using OdeMod.Globals.GlobalItems;

namespace OdeMod.Items.Series.Foods
{
    /// <summary>
    /// 矿物质水 隐含大量病毒 纯纯的奸商乐色玩意。
    /// 喝下后随机获得一些debuff。
    /// </summary>
    internal class GlacierMineralWater : ModItem, IFoods
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
            ItemID.Sets.DrinkParticleColors[Item.type] = new Color[3] {
                Color.Green,
                Color.Purple,
                new Color(255,248,220)
            };
            ItemID.Sets.IsFood[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 32;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.consumable = true;
            Item.maxStack = 99;
            Item.healLife = 200;
        }

        public int[] Debufftype = new int[10]
        {
            20,33,32,46,44,35,70,31,68,334
        };

        public override bool? UseItem(Player player)
        {
            int num = Main.rand.Next(3, 6);
            for (int i = 0; i < num; i++)
            {
                if (Main.rand.NextBool(4))
                {
                    player.AddBuff(Debufftype[Main.rand.Next(5, 10)], 240);
                }
                else
                {
                    player.AddBuff(Debufftype[Main.rand.Next(0, 5)], 360);
                }
            }
            return base.UseItem(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
        }
    }

    /// <summary>
    /// 普通水 打着冰川的旗号 实际上与冰川一点关系也没 好在加个跟普通水无异
    /// </summary>
    internal class GlacierWater : ModItem, IFoods
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
            ItemID.Sets.DrinkParticleColors[Item.type] = new Color[3] {
                new Color(248,248,255),
                new Color(253,245,230),
                new Color(255,248,220)
            };
            ItemID.Sets.IsFood[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 32;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.consumable = true;
            Item.maxStack = 99;
        }

        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }
    }

    /// <summary>
    /// 真正的冰川矿泉水，经魔法去除杂质，造价极高。
    /// </summary>
    internal class GlacierMineralSpringWater : ModItem, IFoods
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
            ItemID.Sets.DrinkParticleColors[Item.type] = new Color[3] {
                new Color(248,248,255),
                new Color(253,245,230),
                new Color(255,248,220)
            };
            ItemID.Sets.IsFood[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Clone();
            Item.width = 32;
            Item.height = 32;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.consumable = true;
            Item.maxStack = 99;
        }

        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }
    }
}