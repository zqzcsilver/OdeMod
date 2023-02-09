namespace OdeMod.CardMode.Rooms
{
    internal class EndRoom : RoomBase
    {
        public override bool PreBuild()
        {
            return IsEnd && IsSilu;
        }
    }
}