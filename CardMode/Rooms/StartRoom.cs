namespace OdeMod.CardMode.Rooms
{
    internal class StartRoom : RoomBase
    {
        public override bool PreBuild()
        {
            return IsBegin;
        }
    }
}