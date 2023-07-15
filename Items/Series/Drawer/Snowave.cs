using Microsoft.Xna.Framework;

using OdeMod.Players;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Drawer
{
    internal class Snowave : BaseDrawer, IDrawer
    {

        public override void SetDefaults()
        {
            BaseDamage = 20;
            DrawerWidth = 3;
            DrawerHeight = 3;

            base.SetDefaults();
            Item.width = 34;
            Item.height = 28;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useTurn = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 6, 50, 0);
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.Frosted.Flycutter1>();
            Item.shootSpeed = 11;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            OdeMod.OdeUISystem.Elements["OdeMod.UI.OdeUISystem.Containers.Drawer.SnowaveD"].Info.IsVisible = true;
            Main.NewText(1);
            return false;
        }
    }
}