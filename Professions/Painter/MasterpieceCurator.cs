using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using System.Collections.Generic;
using System.Linq;

using Terraria;

namespace OdeMod.Professions.Painter
{
    internal class MasterpieceCurator
    {
        private List<Masterpiece> masterpieces = new List<Masterpiece>();

        public void Load()
        {
            Main.RunOnMainThread(() =>
            {
                string prefix = "Images/Masterpieces/";
                var filenames = OdeMod.Instance.GetFileNames().FindAll(x => x.StartsWith(prefix));
                foreach (var filename in filenames)
                {
                    var l = filename[..prefix.Length].Split('/');
                    if (l.Length == 0)
                        continue;
                    string masterpieceName = l.First();
                    var masterpiece = masterpieces.Find(p => p.Name == masterpieceName);
                    if (masterpiece == null)
                    {
                        masterpiece = new Masterpiece();
                        masterpiece.Name = masterpieceName;
                        masterpieces.Add(masterpiece);
                    }
                    Texture2D texture = OdeMod.Instance.Assets.Request<Texture2D>(filename.Split('.')[0], AssetRequestMode.ImmediateLoad).Value;
                    Color[] colors = new Color[texture.Width * texture.Height];
                    texture.GetData(colors);
                    masterpiece.InitByColors(colors, texture.Width, texture.Height);
                }
            });
        }
    }
}