using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OdeMod.Buffs.Foods;
using OdeMod.Players;
using OdeMod.Globals.GlobalItems;
using Microsoft.Xna.Framework;

namespace OdeMod.Items.Series.Foods
{
    /// <summary>
    /// 7餐中有>=3餐吃的是可乐时，会附加效果打嗝 期间会不时附加0.5s的沉默。
    /// 喝下后给予快乐buff
    /// </summary>
    internal class Cola : ModItem, IFoods
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
            Item.width = 38;
            Item.height = 38;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.consumable = true;
            Item.maxStack = 99;
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<OdeAddPlayer>().Food_Variety_Test(Item);
            player.AddBuff(ModContent.BuffType<Happy>(), 6000);
            return true;
        }
    }
}