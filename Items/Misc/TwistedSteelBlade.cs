using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OdeMod.Buffs;

namespace OdeMod.Items.Misc
{
    internal class TwistedSteelBlade : ModItem,IMiscItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 28;
            Item.height = 40;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 10;
            Item.crit = 4;
            Item.useAnimation = 15;
            Item.useTime = 15;
        }
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (Main.rand.NextBool(5))
            {
                target.AddBuff(ModContent.BuffType<NaturalPower>(), 120);
            }
            base.ModifyHitNPC(player, target, ref damage, ref knockBack, ref crit);
        }
    }
}
