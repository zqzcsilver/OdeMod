using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace OdeMod.Items.Series.Miracle
{
    internal class MiracleRecorderBossBag : ModItem, IMiracle
    {
		public override void SetStaticDefaults()
		{
			ItemID.Sets.BossBag[Type] = true;
			ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
			base.SetStaticDefaults();
		}
		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 32;
			Item.rare = ItemRarityID.LightPurple;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.expert = true;
		}
		public override bool CanRightClick()
		{
			return true;
		}
		public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			//这里按uy所说会根据boss僚机而掉落的 银烛之后记得改
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ShiningSpirit>(), 3));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Glory>(), 3));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<CrystalSpur>(), 3));
			//上面这三把武器

			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<HolyElement>(), 1, 12, 16));
			itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<NPCs.Boss.MiracleRecorder.MiracleRecorder>()));
		}
	}
}