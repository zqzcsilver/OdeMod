namespace OdeMod.CardMode.GameInfos
{
    /// <summary>
    /// 职业
    /// </summary>
    public enum CharacterType
    {
        /// <summary>
        /// 无
        /// </summary>
        None,

        /// <summary>
        /// 战士
        /// </summary>
        Melee,

        /// <summary>
        /// 射手
        /// </summary>
        Ranger,

        /// <summary>
        /// 法师
        /// </summary>
        Mage,

        /// <summary>
        /// 召唤师
        /// </summary>
        Summoner,

        /// <summary>
        /// 画师
        /// </summary>
        Painter
    }

    /// <summary>
    /// 网络状态
    /// </summary>
    public enum NetMode
    {
        /// <summary>
        /// 无
        /// </summary>
        None,

        /// <summary>
        /// 单人模式
        /// </summary>
        Singleplayer,

        /// <summary>
        /// 多人模式
        /// </summary>
        Multiplayer,

        /// <summary>
        /// 客户端
        /// </summary>
        Client,

        /// <summary>
        /// 服务器
        /// </summary>
        Server
    }

    internal class GameInfo
    {
        public NetMode NetMode = NetMode.None;
        public CharacterType CharacterType = CharacterType.None;

        public void Reset()
        {
            NetMode = NetMode.None;
            CharacterType = CharacterType.None;
        }
    }
}