using Microsoft.Xna.Framework;
using OdeMod.Players;
using OdeMod.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.HollowKnight
{
    internal class VengefulSpirit : ModItem, IHollowKnight
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("VengefulSpirit");
            //DisplayName.AddTranslation(LanguageType.Chinese, "复仇之魂");
            //Tooltip.SetDefault("召唤一个向前飞行的灵魂，燃烧路径上的敌人。");
        }
        public override void SetDefaults()
        {

            Item.damage = 50;
            Item.crit = -4;
            Item.useStyle = 50000000;
            Item.width = 54;
            Item.height = 32;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 25;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.VengefulSpirit>();
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.shootSpeed = 5.5f;
            Item.knockBack = 9;
            Item.rare = ItemRarityID.Blue;
            Item.noUseGraphic = true;
            Item.autoReuse = false;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            /* for(int i=1;i<Main.maxTilesX;i++)
             {
                 for(int j=1;j< Main.maxTilesY; j++)
                 {
                     if (j % 24 == 0)
                     {
                         WorldGen.PlaceTile(i, j, 19);
                         if (i % 32 == 0)
                         {
                             WorldGen.PlaceTile(i, j-1, 4);
                         }
                     }

                 }
             }*/
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing;
        }
    }
}