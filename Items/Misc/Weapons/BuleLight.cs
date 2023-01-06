using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OdeMod.Items.Misc.Weapons
{
    internal class BuleLight : ModItem,IMiscItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 44;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 40;
            Item.crit = 5;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.BlueLightPro>();
            Item.shootSpeed = 20f;
            Item.useAnimation = 10;
            Item.useTime = 10;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.NaturalPower>(), 120);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.JungleGrassSeeds,10)
                .AddIngredient(ItemID.MushroomGrassSeeds,10)
                .AddIngredient(ItemID.RedPressurePlate,15)
                .AddTile(TileID.CrystalBall)
                .Register();
        }
    }
}
