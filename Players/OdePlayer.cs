using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
namespace OdeMod.Players
{
    internal class OdePlayer : ModPlayer, IOdePlayer
    {
        public static int shakeInt = 0;
        private static int shaketick = 0;
        public override void ModifyScreenPosition()
        {
            if (!Main.gameMenu)
            {
                shaketick++;
                if (shakeInt >= 0 && shaketick >= 60)
                    shakeInt--;
                if (shakeInt > 30)
                    shakeInt = 30;
                if (shakeInt < 0)
                    shakeInt = 0;
                if (!Main.gamePaused && Main.hasFocus)
                {
                    Main.screenPosition += new Vector2(
                        shakeInt * Main.rand.NextFloatDirection() / 2f,
                        shakeInt * Main.rand.NextFloatDirection() / 2f);
                }
            }
            else
            {
                shakeInt = 0;
                shaketick = 0;
            }

            /* ÕðÆÁÓÃ·¨
             
             try
            {
                float demo = 1 + Vector2.DistanceSquared(Main.player[Main.myPlayer].Center, player.Center) / 500000;
                OdePlayer.shakeInt = Math.Max(OdePlayer.shakeInt, (int)(15 / demo));
            }
            catch
            {

            }
             
             */
        }
    }
}