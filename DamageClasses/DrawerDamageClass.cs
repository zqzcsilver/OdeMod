using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace OdeMod.DamageClasses
{
	public class DrawerDamageClass : DamageClass
	{
		public override StatInheritanceData GetModifierInheritance(DamageClass damageClass) {
			if (damageClass == DamageClass.Generic)
				return StatInheritanceData.Full;

			return new StatInheritanceData(
				damageInheritance: 0f,
				critChanceInheritance: 0f,
				attackSpeedInheritance: 0f,
				armorPenInheritance: 0f,
				knockbackInheritance: 0f
			);
		}

		public override bool GetEffectInheritance(DamageClass damageClass) {
			return false;
		}

		public override bool UseStandardCritCalcs => true;

		public override bool ShowStatTooltipLine(Player player, string lineName) {
			if (lineName == "Damage")
				return true;

			return false;
		}
	}
}