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
        public int MiracleLogic = 0;

        public bool MagicBoneShield = false;//魔法骨盾
        public bool HolyFlameCrown = false;

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
                baseInfoComponent.HitBox = new Rectangle(0, 0,
                    (int)(bodyTex.Width * baseInfoComponent.Scale), (int)(bodyTex.Height * baseInfoComponent.Scale));
                baseInfoComponent.Center = Main.ScreenSize.ToVector2() / 4f;
                entity.AddComponent(baseInfoComponent);

                CardInfoComponent cardInfoComponent = new CardInfoComponent();

                cardInfoComponent.CardID = "XXX";
                cardInfoComponent.CardName = "星辰天使";
                cardInfoComponent.CardTip = "代偿：鲜血(8) 或 灵魂(5)\n战吼：选择一个敌人，休眠8回合\n亡语：消灭选择的敌人";
                cardInfoComponent.CardCost = 3;
                entity.AddComponent(cardInfoComponent);

                DrawComponent drawComponent = new DrawComponent();
                drawComponent.DrawSize = new Point((int)(bodyTex.Width * baseInfoComponent.Scale), (int)(bodyTex.Height * baseInfoComponent.Scale));
                entity.AddComponent(drawComponent);

                drawComponent.AddComponent(new CardBodyComponent(bodyTex));
                drawComponent.AddComponent(new CardCostComponent(ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Summoner/CardCost", AssetRequestMode.ImmediateLoad).Value));
                drawComponent.AddComponent(new CardIllustrationComponent(ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Summoner/CardIllustration", AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/StarAngel", AssetRequestMode.ImmediateLoad).Value));
                drawComponent.AddComponent(new CardTipComponent(ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Summoner/CardTip", AssetRequestMode.ImmediateLoad).Value));
                drawComponent.AddComponent(new CardTipFrameworkComponent(ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Rare/CardTipRare", AssetRequestMode.ImmediateLoad).Value));
                drawComponent.AddComponent(new CardNameComponent(ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Summoner/CardName", AssetRequestMode.ImmediateLoad).Value));
                drawComponent.AddComponent(new CardNameFrameworkComponent(ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Rare/CardNameRare", AssetRequestMode.ImmediateLoad).Value));

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
            MagicBoneShield = false;
            HolyFlameCrown = false;
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

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (HolyFlameCrown && proj.type != ModContent.ProjectileType<Projectiles.Misc.HolyFlame>() && Main.rand.NextBool(10))
            {
                Vector2 vector2 = new(0, 0);
                Projectile.NewProjectile(proj.GetSource_OnHit(target), target.position, vector2, ModContent.ProjectileType<Projectiles.Misc.HolyFlame>(), proj.damage / 2, 0, proj.whoAmI);
            }
            base.ModifyHitNPCWithProj(proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (HolyFlameCrown && Main.rand.NextBool(10))
            {
                Vector2 vector2 = new(0, 0);
                Projectile.NewProjectile(item.GetSource_OnHit(target), target.position, vector2, ModContent.ProjectileType<Projectiles.Misc.HolyFlame>(), item.damage / 2, 0, item.whoAmI);
            }
            base.ModifyHitNPC(item, target, ref damage, ref knockback, ref crit);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            base.ModifyHitByProjectile(proj, ref damage, ref crit);
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (MagicBoneShield)
            {
                proj.friendly = true;
                proj.hostile = false;
                proj.damage = damage / 2;
                NPC target = null;
                float dismax = 3200;
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && !npc.friendly)
                    {
                        float dis = Vector2.Distance(npc.Center, proj.Center);
                        if (dis < dismax)
                        {
                            dismax = dis;
                            target = npc;
                        }
                    }
                }
                if (target != null)
                {
                    Vector2 targetVec = target.Center - proj.Center;
                    targetVec.Normalize();
                    targetVec *= 20f;
                    proj.velocity = targetVec;
                }
                base.OnHitByProjectile(proj, damage, crit);
            }
        }
    }
}