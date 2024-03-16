using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
namespace OdeMod.Items.Misc.Weapons
{
    internal class NaturalGift : ModItem, IMiscItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 36;
            Item.height = 40;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 15;
            Item.crit = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.BigTouchPro>();
            Item.shootSpeed = 10f;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.value = Item.sellPrice(0, 0, 50, 0);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(3))
            {
                target.AddBuff(ModContent.BuffType<Buffs.NaturalPower>(), 300, true);
            }
            base.OnHitNPC(player, target, hit, damageDone);
        }
    }
}
