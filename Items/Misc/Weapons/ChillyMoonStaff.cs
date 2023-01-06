using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
namespace OdeMod.Items.Misc.Weapons
{
    //这里不用MoonClearingStaff以及原本的MoonStaff而是用的ChillyMoonStaff原因是...清修饰的不是月 而是风...
    //这个法杖取名原本应该是取自“风清月明”吧？
    internal class ChillyMoonStaff : ModItem, IMiscItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 36;
            Item.height = 40;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 15;
            Item.crit = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.BigTouchPro>();
            Item.shootSpeed = 10f;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.value = Item.sellPrice(0, 0, 50, 0);
        }
    }
}
