using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace OdeMod.Items.Series.Foods
{
    /// <summary>
    /// 阴影柚
    /// </summary>
    internal class ShadyShaddock_C : ModItem, IFoods
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 24;
            Item.height = 24;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.consumable = true;
            Item.maxStack = 99;
        }

        public override bool? UseItem(Player player)
        {
            var pls = player.GetModPlayer<Players.OdeAddPlayer>();
            pls.FoodPutrefaction += 1;
            Rectangle rectangle = new Rectangle((int)player.Center.X - 75, (int)player.Center.Y + 32, 150, 50);
            switch (pls.FoodPutrefaction)
            {
                case 1:
                    CombatText.NewText(rectangle, Color.Red, "有股怪怪的味道...");
                    break;

                case 2:
                    CombatText.NewText(rectangle, Color.Red, "我的肚子突然好疼...");
                    break;

                case 3:
                    CombatText.NewText(rectangle, Color.Red, "啊！忍不住啦！");

                    break;

                default:
                    SoundEngine.PlaySound(SoundID.Item16, player.Center);
                    pls.FoodPutrefaction -= 1;
                    break;
            }
            return true;
        }
    }
}