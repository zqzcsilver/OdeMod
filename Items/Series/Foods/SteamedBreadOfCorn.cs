using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace OdeMod.Items.Series.Foods
{
    /// <summary>
    /// 窝窝头 食用速度极其慢，玩家使用的时候可以按下按键加速过程，同时有50%几率获得“噎住”debuff，无法使用武器
    /// 同时有1%几率直接噎死（暴论
    /// </summary>
    internal class SteamedBreadOfCorn : ModItem, IFoods
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
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
            Item.scale = 0.5f;
            Item.width = 24;
            Item.height = 24;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 100;
            Item.useTime = 100;
            Item.consumable = true;
            Item.maxStack = 99;
        }

        public override bool CanUseItem(Player player)
        {
            return true;
        }
    }
}