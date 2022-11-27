using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.RiftValley
{
    internal class RosewoodCrystalStaff : ModItem, IRiftValley
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 38;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.staff[Item.type] = true;
            Item.damage = 12;
            Item.crit = 5;
            Item.knockBack = 0f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.RiftValley.RosewoodCrystal>();
            Item.shootSpeed = 20f;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 5, 50, 0);
        }
    }
}
