using Microsoft.Xna.Framework;
using OdeMod.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.HollowKnight
{
    internal class HallownestSeal : ModItem, IHollowKnight
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("HallownestSeal");
            //DisplayName.AddTranslation(LanguageType.Chinese, "圣巢印章");
            //Tooltip.SetDefault("长按左键开启圣巢模式\n你的角色魔力值与生命值将不再自动恢复\n玩家免疫所有伤害性负面buff\n玩家的速度和跳跃高度略微提升\n玩家受伤固定受到25伤害\n玩家受伤后拥有更长时间的无敌帧\n  \n高等生灵啊，这些话只说给你听。\n走过这里你就会进入国王和造物主的领土。\n跨过这道门槛，遵从我们的法律。\n见证这最后和唯一的文明，永恒的国度。\n圣巢");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            //规范<固定>顺序 RAAAAAAAADAWDAHHUDWAUIDGIAGWIYDGI
            Item.width = 60;
            Item.height = 60;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<Projectiles.Series.Items.HollowKnight.BeFall>();
            Item.useAnimation = 60;
            Item.useTime = 60;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = false;
            Item.useTurn = true;
            Item.consumable = true;
            Item.channel = true;
        }
        /*public override bool CanUseItem(Player player)
        {
            if (Static.hallow != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }*/
    }
}
