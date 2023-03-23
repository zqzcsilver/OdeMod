using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OdeMod.Utils;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OdeMod.Projectiles.Misc
{
    internal class StarLock : ModProjectile, IMiscProjectile
    {
        //我直接抄银烛码
        //吵吵你码
        public override string Texture => "OdeMod/Projectiles/Misc/Yao";
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.aiStyle = -1;

            Projectile.alpha = 0;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 24;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
        }
        private float m = 0f;
        public Vector2 TVEC = Vector2.Zero;
        public bool Locked = false;
        private float num = 0;
        private float alphaEffect = 0;
        NPC npc = null;

        Vector2 orig = Vector2.Zero;
        public override void AI()
        {
            if (TVEC == Vector2.Zero)
            {
                TVEC = Projectile.velocity;
            }

            if (m <= (float)(Math.PI * 5 / 11))
                m += (float)(Math.PI / 100);
            Vector2 shoot;
            int num = Dust.NewDust(Projectile.position, 6, 6, 223, 0f, 0f, 0, Color.White, 0.8f);
            Main.dust[num].velocity = Projectile.velocity;
            Main.dust[num].noGravity = true;
            if (Projectile.ai[0] == 1)
            {
                shoot = new Vector2(25 / (float)Math.Sqrt(Math.Cos(m)), 3 * (float)Math.Cos(3 * m)).RotatedBy(TVEC.ToRotation());
            }
            else if (Projectile.ai[0] == 2)
            {
                shoot = new Vector2(25 / (float)Math.Sqrt(Math.Cos(m)), -3 * (float)Math.Cos(3 * m)).RotatedBy(TVEC.ToRotation());
            }
            else
            {
                shoot = TVEC;
            }
            Projectile.velocity = Vector2.Normalize(shoot) * 12f;

            if (Projectile.timeLeft >= 100)
            {
                alphaEffect += 0.05f;
            }
            if (Projectile.timeLeft <= 20)
            {
                alphaEffect -= 0.05f;
            }
        }
        public override void PostDraw(Color lightColor)
        {
            List<CustomVertexInfo> bars = new();
            //顶点离弹幕坐标的距离，也是顶点三角形宽度的一半
            // 把所有的点都生成出来，按照顺序
            float width = 12;
            for (int i = 1; i < Projectile.oldPos.Length; ++i)
            {

                if (Projectile.oldPos[i] == Vector2.Zero) break;

                var normalDir = Projectile.oldPos[i - 1] - Projectile.oldPos[i];
                normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));

                var factor = i / (float)Projectile.oldPos.Length;
                var color = Color.Lerp(Color.White, Color.Red, factor);
                var w = MathHelper.Lerp(1f, 0f, (float)Math.Pow(factor, 2)) * alphaEffect;
                bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * width, color, new Vector3(factor, 1, w)));
                bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + 0.5f * new Vector2(Projectile.width, Projectile.height) + normalDir * -width, color, new Vector3(factor, 0, w)));
            }

            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();

            if (bars.Count > 2)
            {

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
                var MaskColor2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Flame0", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainShape2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/Lock", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                var MainColor2 = ModContent.Request<Texture2D>("OdeMod/Images/Effects/heatmap3", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                // 把变换和所需信息丢给shader
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
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                int num = Dust.NewDust(Projectile.position, 6, 6, 71, 0f, 0f, 0, Color.White, 2f);
                Main.dust[num].velocity = Projectile.velocity * Main.rand.Next(0, 101) * 0.01f;
                Main.dust[num].noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.boss && !Locked)

                npc = target;
            orig = Projectile.Center;
            target.AddBuff(ModContent.BuffType<Buffs.Locked>(), 300);

        }

    }
}

