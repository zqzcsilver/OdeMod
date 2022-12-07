using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.CardComponents.AnimationComponents;
using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.CardComponents.DrawComponents;
using OdeMod.CardMode.PublicComponents;

using ReLogic.Content;

using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace OdeMod.Players
{
    internal class OdePlayer : ModPlayer, IOdePlayer
    {
        public bool Carapace = false;//生命甲壳
        private bool Carapace_Open = false;
        private int Carapace_Num = 0;
        public bool HallowMode = false;//圣巢模式
        public int ShakeInt = 0;//震屏
        private int shaketick = 0;
        public int Fall = 0;
        public bool OnHollowKnightItemUsing = false;
        protected int FallTimer = 0;
        public bool HollowKnightMovement = false;
        public int Hallow = 0;
        public int Rolling = 0;
        public int MiracleRecorderShader = 0;

        public override void SaveData(TagCompound tag)
        {
            tag.Add("Carapace", Carapace);
            tag.Add("Carapace_Open", Carapace_Open);
            tag.Add("HallowMode", HallowMode);
        }

        public override void LoadData(TagCompound tag)
        {
            Carapace = tag.Get<bool>("Carapace");
            Carapace_Open = tag.Get<bool>("Carapace_Open");
            HallowMode = tag.Get<bool>("HallowMode");
        }

        public override void PreUpdateMovement()
        {
            if (HollowKnightMovement)
                Player.velocity *= 0f;
        }

        public override void PostUpdate()
        {
            if (Carapace && !Carapace_Open)
            {
                Carapace_Num++;
                if (Carapace_Num > 1200)
                {
                    Carapace_Open = true;
                    Carapace_Num = 0;
                }
            }
            base.PostUpdate();

            entity?.Update(Main.gameTimeCache);
        }

        private CardMode.Entity entity;

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
            if (Main.gameMenu)
                return;

            if (entity == null)
            {
                entity = new CardMode.Entity(null);
                entity.AddComponent<CardComponent>();

                var bodyTex = ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Summoner/CardBody", AssetRequestMode.ImmediateLoad).Value;
                BaseInfoComponent baseInfoComponent = new BaseInfoComponent();
                baseInfoComponent.Scale = 4f;
                baseInfoComponent.Rotation = 0f;
                baseInfoComponent.HitBox = new Rectangle(0, 0, bodyTex.Width * 4, bodyTex.Height * 4);
                baseInfoComponent.Center = Main.ScreenSize.ToVector2() / 4f;
                entity.AddComponent(baseInfoComponent);

                CardInfoComponent cardInfoComponent = new CardInfoComponent();
                cardInfoComponent.CardBodyTexture = bodyTex;
                cardInfoComponent.CardNameTexture = ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Rare/CardName", AssetRequestMode.ImmediateLoad).Value;
                cardInfoComponent.CardFrameworkTexture = ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Rare/CardFramework", AssetRequestMode.ImmediateLoad).Value;
                cardInfoComponent.CardCostTexture = ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Summoner/CardCost", AssetRequestMode.ImmediateLoad).Value;
                cardInfoComponent.CardTipTexture = ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Summoner/CardTip", AssetRequestMode.ImmediateLoad).Value;
                cardInfoComponent.CardIllustrationTexture = ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Summoner/CardIllustration", AssetRequestMode.ImmediateLoad).Value;

                cardInfoComponent.CardIllustration = ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/StarAngel", AssetRequestMode.ImmediateLoad).Value;

                cardInfoComponent.CardID = "XXX";
                cardInfoComponent.CardName = "星辰天使";
                cardInfoComponent.CardTip = "它的任务仅仅是带你去见上帝";
                cardInfoComponent.CardCost = 2;
                entity.AddComponent(cardInfoComponent);

                DrawComponent drawComponent = new DrawComponent();
                drawComponent.DrawSize = new Point(bodyTex.Width * 4, bodyTex.Height * 4);
                entity.AddComponent(drawComponent);

                entity.AddComponent<CardBodyComponent>();
                entity.AddComponent<CardCostComponent>();
                entity.AddComponent(new CardIllustrationComponent());
                entity.AddComponent<CardTipComponent>();
                entity.AddComponent<CardFrameworkComponent>();
                entity.AddComponent<CardNameComponent>();

                DragComponent dragComponent = new DragComponent();
                dragComponent.OriginalPos = Main.ScreenSize.ToVector2() / 4f;
                dragComponent.TargetPos = Main.ScreenSize.ToVector2() / 2f;
                entity.AddComponent(dragComponent);
            }
            entity.Draw(Main.spriteBatch);
            entity.GetComponent<DragComponent>().Dragging = Main.mouseLeft;
            entity.GetComponent<DragComponent>().TargetPos = Main.MouseScreen;
        }

        public override void ModifyScreenPosition()
        {
            if (!Main.gameMenu)
            {
                shaketick++;
                if (ShakeInt >= 0 && shaketick >= 60)
                    ShakeInt--;
                if (ShakeInt > 50)
                    ShakeInt = 50;
                if (ShakeInt < 0)
                    ShakeInt = 0;
                if (!Main.gamePaused && Main.hasFocus)
                {
                    Main.screenPosition += new Vector2(
                        ShakeInt * Main.rand.NextFloatDirection() / 2f,
                        ShakeInt * Main.rand.NextFloatDirection() / 2f);
                }
            }
            else
            {
                ShakeInt = 0;
                shaketick = 0;
            }

            ////震屏
            //try
            //{
            //    float demo = 1 + Vector2.DistanceSquared(Main.player[Main.myPlayer].Center, player.Center) / 500000;
            //    OdePlayer.shakeInt = Math.Max(OdePlayer.shakeInt, (int)(15 / demo));
            //}
            //catch
            //{
            //}
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            if (HallowMode)
            {
                damage = 25 + Player.statDefense / 2;
            }
            return true;
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            base.PostHurt(pvp, quiet, damage, hitDirection, crit, cooldownCounter);

            if (HallowMode)
            {
                Player.immune = true;
                Player.immuneTime = 60;
            }
        }

        public override void ResetEffects()
        {
            if (Fall == 1)
            {
                FallTimer = 3;
                Player.maxFallSpeed = 25f;
                Player.velocity.Y += 25f;
                Player.immune = true;
                Player.immuneTime = 10;
            }
            if (Fall == 0)
            {
                if (FallTimer > 0)
                {
                    Player.maxFallSpeed = 10.01f;
                    Player.velocity.Y *= 0.01f;
                    FallTimer--;
                }
            }
            if (HallowMode)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegenCount = 0;
                if (Player.manaRegen > 0)
                {
                    Player.manaRegen = 0;
                }
                Player.manaRegenCount = 0;
                Player.manaRegenBuff = false;
                Player.manaRegenBonus = 0;
                Player.manaRegenDelay = 10;
                Player.maxRunSpeed = 4.2f;
                Player.runAcceleration = 1f;
                Player.runSlowdown = 0.6f;
                Player.jumpSpeedBoost = 2f;
                Player.noFallDmg = true;
            }
        }
    }
}