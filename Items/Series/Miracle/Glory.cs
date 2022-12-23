using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Miracle
{
    internal class Glory : ModItem, IMiracle
    {
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 54;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 80;
            Item.crit = 4;
            Item.knockBack = 1.5f;
            Item.useTime = 15;
            Item.useAnimation = 15;
            //Item.shoot = ModContent.ProjectileType<Projectiles.Series.>();
            //Item.shootSpeed = 20f;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            base.SetDefaults();
        }
    }
}
