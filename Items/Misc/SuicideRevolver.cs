using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Misc
{
    //中文自尽左轮 但是这里自尽更多指自杀 所以替换无妨
    //这个翻译SuicideRevolver并不满意 Suicide可以翻译为自杀性的 也能翻译自杀的 左轮无妨 很有可能变成自杀的左轮（其实不用担心
    //有趣的事MC的mod将这个自尽用的左轮翻译成“翻转左轮(Flipped Revolver)” Ode的翻译之后可以更有趣些
    //而且设定是反向发射子弹 翻转一词也不错 或许可以右键自杀？
    internal class SuicideRevolver : ModItem, IMiscItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 32;
            Item.height = 38;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 10;
            Item.crit = 5;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.Suicide>();
            Item.shootSpeed = 10f;
            Item.useAnimation = 10;
            Item.useTime = 10;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(6f, 2f);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse != 2)
            {
                Item.useAnimation = 12;
                Item.useTime = 12;
                Item.useAmmo = AmmoID.Bullet;
                Item.shoot = ProjectileID.Bullet;
                Item.shootSpeed = 15f;
                return true;
            }
            else if (player.altFunctionUse == 2)
            {
                Item.useAnimation = 12;
                Item.useTime = 6;
                Item.useAmmo = AmmoID.Bullet;
                Item.shoot = ModContent.ProjectileType<Projectiles.Misc.Suicide>();
                Item.shootSpeed = 1f;
            }
            return true;
        }
        //射击出口有问题 
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                velocity.X *= -1;
                velocity.Y *= -1;
                Projectile.NewProjectile(source, position, velocity, type, 30, knockback, player.whoAmI, 1f);
            }
            else if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.Misc.Suicide>(), 999, knockback, player.whoAmI, 1f);
            }
            return false;
        }
    }
}
