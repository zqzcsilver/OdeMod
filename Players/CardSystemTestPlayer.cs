using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode;
using OdeMod.CardMode.CardComponents.AnimationComponents;
using OdeMod.CardMode.CardComponents.BaseComponents;
using OdeMod.CardMode.CardComponents.DrawComponents;
using OdeMod.CardMode.PublicComponents;
using OdeMod.Configs;

using ReLogic.Content;

using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace OdeMod.Players
{
    internal class CardSystemTestPlayer : ModPlayer, IOdePlayer
    {
        private CardMode.Entity entity;
        private CardMode.Map map;

        public override void PostUpdate()
        {
            base.PostUpdate();
            entity?.Update(Main.gameTimeCache);
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(item, target, damage, knockback, crit);
            if (ModContent.GetInstance<CardSystemTestConfig>().EnableDrawTest)
                CardSystem.Instance.CardModeVisible = true;
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);

            if (!ModContent.GetInstance<CardSystemTestConfig>().EnableDrawTest || Main.gameMenu)
                return;

            if (entity == null)
            {
                entity = new CardMode.Entity(null);
                entity.AddComponent<CardComponent>();

                var bodyTex = ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Summoner/CardBody", AssetRequestMode.ImmediateLoad).Value;
                BaseInfoComponent baseInfoComponent = new BaseInfoComponent();
                baseInfoComponent.Scale = 1f;
                baseInfoComponent.Rotation = 0f;
                baseInfoComponent.HitBox = new Rectangle(0, 0,
                    (int)(bodyTex.Width * baseInfoComponent.Scale), (int)(bodyTex.Height * baseInfoComponent.Scale));
                baseInfoComponent.Center = Main.ScreenSize.ToVector2() / 4f;
                entity.AddComponent(baseInfoComponent);

                CardInfoComponent cardInfoComponent = new CardInfoComponent();
                cardInfoComponent.CardID = "XXX";
                cardInfoComponent.CardName = "星辰天使";
                cardInfoComponent.CardTip = "代偿：鲜血(8) 或 灵魂\n(5)！\n战吼：选择一个敌人！休眠8回合！\n亡语：消灭选择的敌人！";
                cardInfoComponent.CardCost = 3;
                entity.AddComponent(cardInfoComponent);

                DrawComponent drawComponent = new DrawComponent();
                drawComponent.DrawSize = new Point((int)(bodyTex.Width * baseInfoComponent.Scale), (int)(bodyTex.Height * baseInfoComponent.Scale));
                entity.AddComponent(drawComponent);

                drawComponent.AddComponent(new CardBodyComponent(bodyTex));
                drawComponent.AddComponent(new CardIllustrationComponent(ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Summoner/CardIllustration", AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("OdeMod/Items/Series/Recharge/StarAngel", AssetRequestMode.ImmediateLoad).Value));
                drawComponent.AddComponent(new CardCostComponent(ModContent.Request<Texture2D>("OdeMod/Images/Card/Original/Summoner/CardCost", AssetRequestMode.ImmediateLoad).Value));
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

            //if (map == null)
            //{
            //    map = new CardMode.Map(100, 100);
            //    map.Build();
            //}
            //map.Draw(Main.spriteBatch, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight));
        }
    }
}