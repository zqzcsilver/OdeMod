using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Utils;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace OdeMod.Globals.GlobalNPCs
{
    /// <summary>
    /// 用于绘制buff的特效
    /// </summary>
    internal class BuffNPC : GlobalNPC
    {
        private static Vector2[] pos = new Vector2[30];
        private static Vector2[] pos2 = new Vector2[30];
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (npc.HasBuff<Buffs.Locked>())
            {
                for (int i = 0; i < 30; i++)
                {
                    float amount = (float)-0.1f * i + (float)Main.time*0.1f;
                    pos[i] = npc.Center + 30 * new Vector2((float)Math.Cos(amount) * 2, (float)Math.Sin(amount)).RotatedBy(0.75f);
                    pos2[i] = npc.Center - 30 * new Vector2((float)Math.Cos(amount) * 2, (float)Math.Sin(amount)).RotatedBy(-0.75f);
                }
                List<CustomVertexInfo> bars = new();
                float width = 12;
                for (float i = 1; i < 30; ++i)
                {

                    var normalDir = pos[(int)i - 1] - pos[(int)i];
                    normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));

                    var factor = i / 30;
                    var color = Color.Lerp(Color.White, Color.Red, factor);
                    var w = MathHelper.Lerp(1f, 0f, (float)Math.Pow(factor, 2));
                    bars.Add(new CustomVertexInfo(pos[(int)i] + normalDir * width, color, new Vector3(factor, 1, w)));
                    bars.Add(new CustomVertexInfo(pos[(int)i] + normalDir * -width, color, new Vector3(factor, 0, w)));
                }
                for (float i = 1; i < 30; ++i)
                {

                    var normalDir = pos2[(int)i - 1] - pos2[(int)i];
                    normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));

                    var factor = i / 30;
                    var color = Color.Lerp(Color.White, Color.Red, factor);
                    var w = MathHelper.Lerp(1f, 0f, (float)Math.Pow(factor, 2));
                    bars.Add(new CustomVertexInfo(pos2[(int)i] + normalDir * width, color, new Vector3(factor, 1, w)));
                    bars.Add(new CustomVertexInfo(pos2[(int)i] + normalDir * -width, color, new Vector3(factor, 0, w)));
                }

                List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();

                if (bars.Count > 2)
                {

                    for (int i = 0; i < 56; i += 2)//58-2
                    {
                        triangleList.Add(bars[i]);
                        triangleList.Add(bars[i + 2]);
                        triangleList.Add(bars[i + 1]);

                        triangleList.Add(bars[i + 1]);
                        triangleList.Add(bars[i + 2]);
                        triangleList.Add(bars[i + 3]);
                    }
                    for (int i = 58; i < 114; i += 2)
                    {
                        triangleList.Add(bars[i]);
                        triangleList.Add(bars[i + 2]);
                        triangleList.Add(bars[i + 1]);

                        triangleList.Add(bars[i + 1]);
                        triangleList.Add(bars[i + 2]);
                        triangleList.Add(bars[i + 3]);
                    }
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                    RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;

                    var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                    var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0)) * Main.Transform;

                    var shader = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MaskColor2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Flame0", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MainShape2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Lock", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MainColor2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap3", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    shader.Parameters["uTransform"].SetValue(model * projection);
                    shader.Parameters["uTime"].SetValue(-(float)Main.time * 0.05f);
                    Main.graphics.GraphicsDevice.Textures[0] = MainColor2;
                    Main.graphics.GraphicsDevice.Textures[1] = MainShape2;
                    Main.graphics.GraphicsDevice.Textures[2] = MaskColor2;
                    Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                    Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                    Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;

                    shader.CurrentTechnique.Passes[0].Apply();

                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
                    Main.graphics.GraphicsDevice.RasterizerState = originalState;
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                }
            }
        }
    }
}
