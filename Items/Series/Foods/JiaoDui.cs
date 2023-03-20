using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OdeMod.Items.Series.Foods
{
    internal class CandiedFruit : ModItem, IFoods
    {
        public override string Texture => "OdeMod/Items/Series/Foods/JiaoDui";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("糖油果子");
            Tooltip.SetDefault("再来一串！");
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
            Item.DefaultToFood(30, 30, BuffID.WellFed2, 36000);
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.consumable = true;
            Item.maxStack = 99;
            Item.value = Item.buyPrice(0, 0, 10, 0);
        }
        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }
        public override bool? UseItem(Player player)
        {
            player.AddBuff(BuffID.WellFed2, 36000);
            return true;
        }
    }
    internal class JiaoDui : ModItem, IFoods
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("焦䭔");
            Tooltip.SetDefault("看起来十分像糖油果子，但是上面的外壳看起来硬邦邦的\n上面仿佛还带着一些沙土...这东西真的能吃吗？");
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
            ItemID.Sets.FoodParticleColors[Item.type] = new Color[3] {
                new Color(248,248,255),
                Color.Green,
                Color.Red
            };
            ItemID.Sets.IsFood[Type] = true;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 30;
            Item.height = 30;
            Item.UseSound = SoundID.Item2;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.consumable = true;
            Item.maxStack = 30;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }
        public override bool? UseItem(Player player)
        {
            Rectangle rectangle = new Rectangle((int)player.Center.X - 75, (int)player.Center.Y - 5, 150, 50);
            var pls = player.GetModPlayer<Players.OdeAddPlayer>();
            if (Main.rand.NextBool(100 - pls.FoodPutrefaction))//这个判定之后会移动到Player里
            {
                player.AddBuff(ModContent.BuffType<Buffs.Foods.Vomit>(), 600);
                CombatText.NewText(rectangle, Color.Green, "肚子好难受...");
            }
            else
            {
                CombatText.NewText(rectangle, Color.Green, "感觉这个已经完全坏掉了...");
                pls.Satiety++;
            }
            return true;
        }
    }
}
