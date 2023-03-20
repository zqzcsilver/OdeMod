using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Players;
using OdeMod.ShaderDatas.ScreenShaderDatas;
using OdeMod.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;

namespace OdeMod.NPCs.Boss.DargonDemo
{
    internal class Galaxy
    {
        private int StarNum;
        public void SetDefaults(int starnum)
        {
            StarNum = starnum;
        }
    }
    internal class GalaxyDemo : ModNPC
    {
        public override string Texture => "OdeMod/Items/Misc/Wan";
        private enum GalaxyType
        {
            Libra
        }
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            GalaxyStart((int)GalaxyType.Libra, player);
            base.AI();
        }
        public void GalaxyStart(int type,Player target)
        {
            Vector2 Center = target.Center + new Vector2(0, -100);
            switch (type)
            {

            }
        }
        public void SetGalaxy(int num)
        {
            Galaxy gal = new Galaxy();
            gal.SetDefaults(num);
        }
    }
}
