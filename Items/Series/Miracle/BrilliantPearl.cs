using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using OdeMod.NPCs.Boss.MiracleRecorder;

namespace OdeMod.Items.Series.Miracle
{
    internal class BrilliantPearl : ModItem, IMiracle
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<MiracleRecorder>());
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.maxStack = 1;
            Item.rare = 8;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                int type = ModContent.NPCType<MiracleRecorder>();
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (player.ZoneHallow && Main.dayTime)
                    {
                        NPC.SpawnOnPlayer(player.whoAmI, type);//生成Boss
                    }
                }
                else
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: player.whoAmI, number2: type);//发包，用来联机同步
                }
            }
            return true;
        }
    }
}