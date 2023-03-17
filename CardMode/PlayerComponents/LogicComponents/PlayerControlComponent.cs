using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using OdeMod.CardMode.PublicComponents.LogicComponents;

namespace OdeMod.CardMode.PlayerComponents.LogicComponents
{
    internal class PlayerControlComponent : Component
    {
        public MoveComponent MoveComponent => Entity.GetComponent<MoveComponent>();

        public override List<Type> GetDependComponents()
        {
            return new List<Type> { typeof(MoveComponent) };
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            var mc = MoveComponent;
            if (mc == null || mc.Map == null)
                return;
            if (CardSystem.GetMouseInfo.MouseLeftClick && mc.Map.FocusRoom != null)
            {
                var targetRoom = mc.Map.FocusRoom;
                if (mc.CanMove)
                {
                    if (mc.CanTakeInRoom(targetRoom) && mc.PreTakeInRoom(targetRoom))
                        mc.TakeInRoom(targetRoom);
                }
                else
                {
                    if (mc.Position == targetRoom.Position && mc.CanTakeInRoom(targetRoom) && mc.PreTakeInRoom(targetRoom))
                        mc.TakeInRoom(targetRoom);
                }
            }
        }

        public override IComponent Clone(Entity cloneEntity)
        {
            throw new System.NotImplementedException();
        }
    }
}