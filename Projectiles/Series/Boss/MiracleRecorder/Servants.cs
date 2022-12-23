using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Players;
using OdeMod.ShaderDatas.ScreenShaderDatas;
using OdeMod.Utils;

using System;
using System.Collections.Generic;

using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

using static Terraria.Utils;
namespace OdeMod.Projectiles.Series.Boss.MiracleRecorder
{
    internal class Servants : ModProjectile, IMiracleRecorderProj
    {
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.aiStyle = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 114514000;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        private float timer = 0;
        private int servantCount = 2;
        private Vector2 bossPos = Vector2.Zero;
        private float oldrotate = 0;
        private float newrotate = 0;
        private int line = 0;
        private bool shadow = false;
        float rad2 = 0;
        Vector2 cent = Vector2.Zero;

        private int oldlogic = -1;
        private int newlogic = -1;
        public override void AI()
        {
            Player player = Main.LocalPlayer;
            servantCount = player.GetModPlayer<OdePlayer>().ServantCount;
            bossPos = player.GetModPlayer<OdePlayer>().MiraclePosFounded;
            //for (int i = 1; i <= servantCount; i++)
            //{
            //    float rad2 = (6.2832f / servantCount) * i;
            //    Projectile.NewProjectile(NPC.GetSource_FromAI(),
            //        NPC.Center + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (120f + servantCount * 20f), Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Servants>(), NPC.damage, 0, player.whoAmI, i);
            //}

            ///初始化
            ///
            newlogic = player.GetModPlayer<OdePlayer>().MiracleLogic;
            if (oldlogic != newlogic)
            {
                timer = 0;
            }
            oldlogic = player.GetModPlayer<OdePlayer>().MiracleLogic;



            timer++;
            if (timer % 8 == 0)
            {
                if (Projectile.frame < 3)
                    Projectile.frame++;
                else
                    Projectile.frame = 0;
            }
            Projectile.velocity *= 0f;
            ///

            if (player.GetModPlayer<OdePlayer>().MiracleLogic == 0)
            {
                if (timer == 1)
                {
                    shadow = true;
                    rad2 = (6.2832f / servantCount) * Projectile.ai[0];
                }

                if (Projectile.alpha > 0) Projectile.alpha -= 10;
                if (timer > 1 && timer < 190)
                {
                    rad2 += (float)Math.Sin(timer / 55f) * 0.08f;
                    Projectile.Center = player.GetModPlayer<OdePlayer>().MiraclePosFounded + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (120f + servantCount * 15f);
                    Projectile.rotation = rad2;
                }
                if (timer == 190)
                {
                    oldrotate = Projectile.rotation;
                    newrotate = (Projectile.Center - bossPos).ToRotation() - 1.57f;
                }
                if (timer >= 190 && timer < 210)
                {
                    float minus = newrotate - oldrotate;
                    while (minus > 3.14159f)
                        minus -= 6.28318f;

                    while (minus < -3.14159f)
                        minus += 6.28318f;
                    Projectile.rotation += minus * 0.05f;
                    line = 1;
                }
                if (timer == 215)
                {
                    line = 0;
                    cent = player.GetModPlayer<OdePlayer>().MiraclePosFounded;
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), new Vector2((float)Math.Cos(Projectile.rotation + 1.57f), (float)Math.Sin(Projectile.rotation + 1.57f)) * 16 + Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Series.Boss.MiracleRecorder.Laser02>(), 0, 0, player.whoAmI, Projectile.rotation);
                }
            }
            if (player.GetModPlayer<OdePlayer>().MiracleLogic == 1)
            {

                if (player.GetModPlayer<OdePlayer>().MiracleX == 1)
                {
                    timer = 1;
                }
                if (timer == 1)
                {
                    shadow = false;
                }
                if (timer > 1 && timer < 30)
                {
                    rad2 += 0.08f;
                    var lerPos = Vector2.Lerp(cent, player.GetModPlayer<OdePlayer>().MiraclePosFounded, (float)(0.5f * Math.Tanh((double)(timer - 15) / 7.5f) + 0.5f));
                    Projectile.Center = lerPos + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (120f + servantCount * 15f);
                    Projectile.rotation = rad2;
                }
                if (timer > 30f && timer < 110)
                {
                    shadow = true;
                    rad2 += 0.08f;
                    Projectile.Center = player.GetModPlayer<OdePlayer>().MiraclePosFounded + new Vector2((float)Math.Cos(rad2), (float)Math.Sin(rad2)) * (120f + servantCount * 15f);
                    Projectile.rotation = rad2;
                }
                if (timer == 90)
                {
                    cent = player.GetModPlayer<OdePlayer>().MiraclePosFounded;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, damage);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }

        public override bool PreDraw(ref Color lightColor)
        {

            if (line == 1)
            {
                Player player = Main.LocalPlayer;
                Vector2 tor = new Vector2((float)Math.Cos(Projectile.rotation + 1.57f), (float)Math.Sin(Projectile.rotation + 1.57f));
                Color color1 = new Color(255, 0, 241, 0f);
                Color color2 = new Color(255, 0, 241, 0.4f);
                Color color4;
                if (timer < 30f)
                    color4 = Color.Lerp(color1, color2, timer / 30f);
                else
                    color4 = Color.Lerp(color1, color2, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
                DrawLine(Main.spriteBatch, Projectile.Center, new Vector2((float)Math.Cos(Projectile.rotation + 1.57f), (float)Math.Sin(Projectile.rotation + 1.57f)) * 5000 + Projectile.Center, color4, color4, 3f);
            }


            if(shadow)
            {
                List<CustomVertexInfo> bars = new();

                int width = 18;
                var normalDir2 = Projectile.position - Projectile.oldPos[0];

                normalDir2 = Vector2.Normalize(new Vector2(-normalDir2.Y, normalDir2.X));
                bars.Add(new CustomVertexInfo(Projectile.Center + normalDir2 * width, Color.Red, new Vector3(0, 1, 1)));
                bars.Add(new CustomVertexInfo(Projectile.Center + normalDir2 * -width, Color.Red, new Vector3(0, 0, 1)));
                //这两个顶点加之前和加之后的效果完全相同，这一切都指向一个事实：程序是不存在的。
                //Main.spriteBatch.End();
                //Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                //Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, Projectile.Center + normalDir2 * width - Main.screenPosition, new Rectangle(0, 0, 10, 10), Color.White, 0f, new Vector2(0.5f, 0.5f), 5f, SpriteEffects.None, 0f);
                //Main.NewText(1);
                //Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, Projectile.Center + normalDir2 * -width - Main.screenPosition, new Rectangle(0, 0, 10, 10), Color.White, 0f, new Vector2(0.5f, 0.5f), 5f, SpriteEffects.None, 0f);
                //Main.spriteBatch.End();
                //我试图在那两个顶点的位置draw两个白色的方块，可是不显示任何东西

                //Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                for (int i = 1; i < Projectile.oldPos.Length; ++i)
                {
                    width -= 3;
                    if (Projectile.oldPos[i] == Vector2.Zero) break;
                    var normalDir = Projectile.oldPos[i - 1] - Projectile.oldPos[i];
                    normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));
                    var factor = i / (float)Projectile.oldPos.Length;
                    var color = Color.Lerp(Color.White, Color.Red, factor);
                    var w = MathHelper.Lerp(1f, 0f, factor);
                    bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * width, color, new Vector3(factor, 1, w * (255 - Projectile.alpha) / 255f)));
                    bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * -width, color, new Vector3(factor, 0, w * (255 - Projectile.alpha) / 255f)));
                }

                List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
                //count用于返回bars里面的元素数量（即顶点数量）
                if (bars.Count > 2)
                {
                    triangleList.Add(bars[0]);
                    var vertex = new CustomVertexInfo((bars[0].Position + bars[1].Position) * 0.5f + Vector2.Normalize(Projectile.velocity) * 30, Color.White,
                        new Vector3(0, 0.5f, 1));
                    triangleList.Add(bars[1]);
                    triangleList.Add(vertex);//用于绘制最前面的三角形，是个等腰三角形

                    for (int i = 0; i < bars.Count - 2; i += 2)
                    {
                        triangleList.Add(bars[i]);
                        triangleList.Add(bars[i + 2]);
                        triangleList.Add(bars[i + 1]);

                        triangleList.Add(bars[i + 1]);
                        triangleList.Add(bars[i + 2]);
                        triangleList.Add(bars[i + 3]);
                    }
                    // 按照顺序连接三角形，连接顺序请看裙子视频

                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                    RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;

                    var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                    var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0)) * Main.Transform;

                    //启用即时加载加载Shader
                    var shader = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/Trail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MainColor = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MaskColor2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_189", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    var MainShape2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Extra_200", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    // 把变换和所需信息丢给shader
                    shader.Parameters["uTransform"].SetValue(model * projection);//坐标变换，详见小裙子视频
                    shader.Parameters["uTime"].SetValue(-(float)Main.time * 0.05f);//使纹理随时间变化

                    Main.graphics.GraphicsDevice.Textures[0] = MainColor;
                    Main.graphics.GraphicsDevice.Textures[1] = MainShape2;
                    Main.graphics.GraphicsDevice.Textures[2] = MaskColor2;
                    Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                    Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                    Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;

                    shader.CurrentTechnique.Passes[0].Apply();

                    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);
                    //连三角形，其中那个0是偏移量
                    Main.graphics.GraphicsDevice.RasterizerState = originalState;
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                }
            }
          



            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 drawOrigin = new Vector2(42 * 0.5f, 56 * 0.7f);
            Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, -16f);
            //绘制本体
            Main.spriteBatch.Draw(texture, drawPos, new Rectangle(0, Projectile.frame * 56, 42, 56), lightColor * ((255f - (float)Projectile.alpha) / 255f), Projectile.rotation, drawOrigin, 1f, SpriteEffects.None, 0f);
            return false;

        }

    }
}