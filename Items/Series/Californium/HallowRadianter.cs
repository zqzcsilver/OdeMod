using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Californium
{
    internal class HallowRadianter : ModItem, ICalifornium
    {
        public override void SetStaticDefaults()
        {
            /*
            base.SetStaticDefaults();
            DisplayName.SetDefault("Hallow Radianter");
            DisplayName.AddTranslation(LanguageType.Chinese, "神圣放射者");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(LanguageType.Chinese, "");
            */
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 56;
            Item.height = 56;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 7.2f;
            Item.damage = 85;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 9, 42, 68);
            Item.autoReuse = true;
        }
        public override bool? UseItem(Player player)
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && npc.immune[player.whoAmI] <= 0 &&
                    Collision.CanHit(player.Center, player.width, player.height, npc.Center, npc.width, npc.height))
                {
                    npc.buffImmune[BuffID.OnFire3] = false;//ModContent.BuffType<DebuffRadiation>()
                    npc.AddBuff(BuffID.OnFire3, 600);
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
               .AddIngredient(ItemID.Excalibur)
               .AddIngredient(ModContent.ItemType<CaliforniumBar>(), 8)
               .AddTile(TileID.LunarCraftingStation)
               .Register();
        }
    }
}
