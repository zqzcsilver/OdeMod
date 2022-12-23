using Microsoft.Xna.Framework.Graphics;

using OdeMod.CardMode.PublicComponents;
using OdeMod.CardMode.PublicComponents.DrawComponents;

using System;

namespace OdeMod.CardMode.CardComponents.DrawComponents
{
    internal abstract class CardDrawComponentBase : DrawComponentBase
    {
        public Texture2D Texture;

        public CardDrawComponentBase(Texture2D texture)
        {
            Texture = texture;
        }

        public override void Load(DrawComponent drawComponent)
        {
            drawComponent.RegisterHook(DrawComponent.HookType.OnDraw, this, OnCardDraw);
        }

        public virtual void OnCardDraw(Entity entity, BaseInfoComponent infoComponent, SpriteBatch sb, HookInfo hookInfo)
        {
        }

        public override DrawComponentBase PrimitiveClone(DrawComponent drawComponent)
        {
            return (DrawComponentBase)Activator.CreateInstance(GetType(), Texture);
        }
    }
}