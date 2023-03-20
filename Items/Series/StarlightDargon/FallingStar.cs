using Microsoft.Xna.Framework;
using OdeMod.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.StarlightDargon
{
    //麻了 这个异象真不知道怎么翻译
    internal class FallingStar : ModItem, IStarlightDargon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 38;
            Item.maxStack = 1;
        }
        public override bool OnPickup(Player player)
        {
            switch (Main.moonPhase)
            {
                case 0: Main.NewText("这是个满月！");
                    break;
            }
            return base.OnPickup(player);
        }
        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
        }
    }
}
