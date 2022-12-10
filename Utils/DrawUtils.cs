using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

using Terraria;
using Terraria.GameContent;

namespace OdeMod.Utils
{
    internal static class DrawUtils
    {
        /// <summary>
        /// 绘制直线
        /// </summary>
        /// <param name="sb">画布</param>
        /// <param name="startPoint">起点</param>
        /// <param name="endPoint">终点</param>
        /// <param name="size">线条大小</param>
        /// <param name="color">线条颜色</param>
        public static void DrawLine(SpriteBatch sb, Vector2 startPoint, Vector2 endPoint, Vector2 size, Color color)
        {
            float length = (startPoint - endPoint).Length();
            Vector2 normalVec = endPoint - startPoint;
            normalVec.Normalize();
            Rectangle rectangle = new Rectangle();
            rectangle.Width = (int)size.X;
            rectangle.Height = (int)size.Y;
            for (int i = 0; i < length; i++)
            {
                rectangle.X = (int)(startPoint.X + i * normalVec.X);
                rectangle.Y = (int)(startPoint.Y + i * normalVec.Y);
                sb.Draw(TextureAssets.MagicPixel.Value, rectangle, color);
            }
        }

        /// <summary>
        /// 获取一块具有DrawLayer绘制内容的画布
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="drawLayer"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static RenderTarget2D GetDrawRenderTarget(SpriteBatch sb, Action<SpriteBatch> drawLayer, int width, int height)
        {
            sb.End();
            sb.GraphicsDevice.SetRenderTarget(Main.screenTargetSwap);
            sb.GraphicsDevice.Clear(Color.Black);
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp,
                DepthStencilState.Default, RasterizerState.CullNone);
            sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            sb.End();

            var render = OdeMod.RenderTarget2DPool.Pool(width, height);
            sb.GraphicsDevice.SetRenderTarget(render);
            sb.GraphicsDevice.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp,
                DepthStencilState.Default, RasterizerState.CullNone);
            drawLayer(sb);
            sb.End();

            sb.GraphicsDevice.SetRenderTarget(Main.screenTarget);
            sb.GraphicsDevice.Clear(Color.Black);
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp,
                DepthStencilState.Default, RasterizerState.CullNone);
            sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            sb.End();

            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
            return render;
        }

        public static RenderTarget2D SetDrawRenderTarget(SpriteBatch sb, Action<SpriteBatch> drawLayer, RenderTarget2D render,
            RenderTarget2D screenRender, RenderTarget2D screenRenderSwap)
        {
            sb.End();
            sb.GraphicsDevice.SetRenderTarget(screenRenderSwap);
            sb.GraphicsDevice.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone);
            sb.Draw(screenRender, Vector2.Zero, Color.White);
            sb.End();

            sb.GraphicsDevice.SetRenderTarget(render);
            sb.GraphicsDevice.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone);
            drawLayer(sb);
            sb.End();

            sb.GraphicsDevice.SetRenderTarget(screenRender);
            sb.GraphicsDevice.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone);
            sb.Draw(screenRenderSwap, Vector2.Zero, Color.White);
            sb.End();

            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
            return render;
        }

        /// <summary>
        /// 对画笔所在的桌子上的画布释放魔法
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="effect"></param>
        public static void SetEffectToScreen(SpriteBatch sb, Effect effect)
        {
            sb.End();
            sb.GraphicsDevice.SetRenderTarget(Main.screenTargetSwap);
            sb.GraphicsDevice.Clear(Color.Black);
            sb.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            sb.End();

            sb.GraphicsDevice.SetRenderTarget(Main.screenTarget);
            sb.GraphicsDevice.Clear(Color.Black);
            sb.Begin(SpriteSortMode.Immediate, null, null, null, null, effect);
            sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
        }

        public static void SetDrawToScreen(SpriteBatch sb, Action<SpriteBatch, RenderTarget2D> draw)
        {
            sb.End();
            sb.GraphicsDevice.SetRenderTarget(Main.screenTargetSwap);
            sb.GraphicsDevice.Clear(Color.Black);
            sb.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            sb.End();

            sb.GraphicsDevice.SetRenderTarget(Main.screenTarget);
            sb.GraphicsDevice.Clear(Color.Black);
            sb.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            draw(sb, Main.screenTargetSwap);
            sb.End();

            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
        }
    }
}