using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OdeMod.Items.Series.Foods
{
    /// <summary>
    /// 吃完后获得吃饱2buff，连续吃2碗后获得吃撑buff，部分食物无法摄入。
    /// </summary>
    internal class Dumpling : ModItem, IFoods
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("水饺");
            Tooltip.SetDefault("好吃嘿嘿嘿 再来一碗");
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
            Item.DefaultToFood(40, 22, BuffID.WellFed3, 3600);
            Item.useAnimation = 60;
            Item.useTime = 60;
            Item.consumable = true;
            Item.maxStack = 99;
        }
        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }
    }
}
