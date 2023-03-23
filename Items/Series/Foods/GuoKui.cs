using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OdeMod.Items.Series.Foods
{
    /// <summary>
    /// 需三次才能完全吃下 无法堆叠
    /// </summary>
    internal class GuoKui : ModItem, IFoods
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
            ItemID.Sets.DrinkParticleColors[Item.type] = new Color[2] {
                Color.White,
                Color.Brown
            };
            ItemID.Sets.IsFood[Type] = true;
        }

        public int Num = 0;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 28;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.maxStack = 1;
        }

        public override bool CanUseItem(Player player)
        {
            if (Num >= 2)
            {
                Item.consumable = true;
            }
            return base.CanUseItem(player);
        }

        public override bool? UseItem(Player player)
        {
            Rectangle rectangle = new Rectangle((int)player.Center.X - 25, (int)player.Center.Y + 32, 50, 50);
            Num++;
            if (Num >= 3)
            {
                CombatText.NewText(rectangle, Color.White, "嗝~");
            }
            else
            {
                CombatText.NewText(rectangle, Color.Red, "嘎嘣");
            }
            return true;
        }
    }
}