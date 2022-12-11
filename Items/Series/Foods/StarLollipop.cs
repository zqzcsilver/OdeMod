﻿using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OdeMod.Items.Series.Foods
{
    internal class StarLollipop : ModItem, IFoods
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("我要吃!星辰棒棒糖!");
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
            ItemID.Sets.FoodParticleColors[Item.type] = new Color[3] {
                new Color(248,248,255),
                new Color(253,245,230),
                new Color(255,248,220)
            };
            ItemID.Sets.IsFood[Type] = true;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.DefaultToFood(26, 28, BuffID.WellFed3, 3600,false);
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.consumable = true;
            Item.maxStack = 99;
        }
        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-0f, 10f);
        }
    }
}