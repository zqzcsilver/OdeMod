using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OdeMod.Items.Series.Spooky
{
    internal class SpookyStone : ModItem, ISpooky
    {
        public override string Texture => "OdeMod/Items/Misc/Wan";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ModContent.ItemType<Misc.Wan>());
            base.SetDefaults();
        }
        public int Num = 0;//确认次数
        public override bool? UseItem(Player player)
        {
            var pls = player.GetModPlayer<Players.OdeAddPlayer>();
            if (pls.SpookyMode)
            {
                Num++;
                switch (Num)
                {
                    case 1:
                        Main.NewText("警告，此模式并不适用于所有人，你是否要真的开启？(再次使用以确定)");
                        return false;
                    case 2:
                        Main.NewText("最后警告，对于此模式带给您的身心伤害，制作组不负任何责任，请三思后开启.(再次使用以确认)");
                        return false;
                    case 3:
                        pls.StrangeEvent(1, 1,player); pls.SpookyMode = true;
                        Main.NewText("已█启");
                        return true;
                }
            }
            return false;
        }
    }
}
