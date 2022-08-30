using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc
{
    internal class Redmoon : ModItem, IMiscItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 60;
            Item.height = 60;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 4f;
            Item.damage = 390;
            Item.crit = 16;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.Accumulation>();
            Item.shootSpeed = 0;
            Item.channel = true;
        }
    }
}