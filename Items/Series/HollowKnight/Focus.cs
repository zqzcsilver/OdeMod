using OdeMod.Players;
using OdeMod.Utils;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.HollowKnight
{
    internal class Focus : ModItem, IHollowKnight
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Focus");
            DisplayName.AddTranslation(LanguageType.Chinese, "聚集");
            Tooltip.SetDefault("消耗50点法力值，回复50点生命。");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            //规范<固定>顺序 其中两条改了下顺序 看的舒服点(
            Item.width = 44;
            Item.height = 46;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 60;
            Item.crit = -4;
            Item.knockBack = 9;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.Focus>();//我一直好奇为什么银烛这里写的都是同一个PRO
            Item.shootSpeed = 15f;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.DrinkLong;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.channel = true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.statMana > 0)
                return !player.GetModPlayer<OdePlayer>().OnHollowKnightItemUsing;
            else
            {
                return false;
            }
        }
    }
}
