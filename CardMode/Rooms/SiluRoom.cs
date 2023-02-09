namespace OdeMod.CardMode.Rooms
{
    internal class SiluRoom : RoomBase
    {
        public override bool PreBuild()
        {
            return IsSilu && !IsEnd;
        }
    }
}