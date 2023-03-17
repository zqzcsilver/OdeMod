using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.UI.OdeUISystem.UIElements;

namespace OdeMod.CardMode.UI.Map
{
    internal class MapElement : BaseElement
    {
        private CardMode.Map _map;
        private Vector2 _drawSize;
        private Vector2 _drawOffset;

        public MapElement(CardMode.Map map, Vector2 drawSize)
        {
            _map = map;
            _drawSize = drawSize;
        }

        public void SetMap(CardMode.Map map)
        {
            _map = map;
        }

        public override void Calculation()
        {
            base.Calculation();
            if (_map != null)
            {
                Info.Width.SetValue(_map.MaxSize.X, 0f);
                Info.Height.SetValue(_map.MaxSize.Y, 0f);
            }
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            
            Calculation();
        }

        public void UpdateMapDrawOffset(Vector2 vector) => _drawOffset = vector;

        public void UpdateMapDrawSize(Vector2 vector) => _drawSize = vector;

        protected override void DrawSelf(SpriteBatch sb)
        {
            _map?.Draw(sb, new Rectangle((int)_drawOffset.X, (int)_drawOffset.Y, (int)_drawSize.X, (int)_drawSize.Y));
        }
    }
}