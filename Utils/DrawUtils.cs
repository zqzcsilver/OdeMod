using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OdeMod.Utils.Expands;
using OdeMod.Utils.Geometry;

using Terraria;
using Terraria.ModLoader;

namespace OdeMod.Utils
{
    internal static class DrawUtils
    {
        public struct DrawTriangleInfo
        {
            public Triangle PositionTriangle;
            public Triangle MappingTriangle;
            public Color Color;
            public bool Full;
            public float Thickness;
            public bool RotationCorrection;
        }

        public static void DrawInTriangle(SpriteBatch sb, Triangle triangle, Texture2D texture,
            Color color, Func<Vector2, Vector2, Vector2, Vector2> texcoord, Func<Vector2, Vector2, Vector2> scaleFactor,
            Func<Vector2, Vector2, Vector2> positionFactor, float thickness = 4f, bool full = false)
        {
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

            var originalState = sb.GraphicsDevice.RasterizerState;
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            rasterizerState.FillMode = FillMode.Solid;
            sb.GraphicsDevice.RasterizerState = rasterizerState;

            Vector2 max = new Vector2(Math.Max(Math.Max(triangle.VertexA.X, triangle.VertexB.X), triangle.VertexC.X),
                Math.Max(Math.Max(triangle.VertexA.Y, triangle.VertexB.Y), triangle.VertexC.Y)),
                min = new Vector2(Math.Min(Math.Min(triangle.VertexA.X, triangle.VertexB.X), triangle.VertexC.X),
                Math.Min(Math.Min(triangle.VertexA.Y, triangle.VertexB.Y), triangle.VertexC.Y));
            max -= min;
            Vector2 texSize = texture.Size();

            VertexPositionColorTexture[] vertexs = new VertexPositionColorTexture[]
            {
                new VertexPositionColorTexture(new Vector3(triangle.VertexA, 0f), color,
                texcoord(texSize,max,triangle.VertexA-min)),
                new VertexPositionColorTexture(new Vector3(triangle.VertexB, 0f), color,
                texcoord(texSize,max,triangle.VertexB-min)),
                new VertexPositionColorTexture(new Vector3(triangle.VertexC, 0f), color,
                texcoord(texSize,max,triangle.VertexC-min))
            };

            var x = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/DrawInTriangle", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            x.Parameters["uWorldViewProjection"].SetValue(Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1));
            var scale = scaleFactor(texSize, max);
            x.Parameters["uScaleFactor"].SetValue(scale);
            x.Parameters["uPositionFactor"].SetValue(positionFactor(texSize, max));
            x.Parameters["SpriteTexture"].SetValue(texture);
            x.CurrentTechnique.Passes[0].Apply();

            sb.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexs, 0, vertexs.Length / 3);

            sb.GraphicsDevice.RasterizerState = originalState;

            if (thickness > 0f)
            {
                List<Vector2> points = new List<Vector2>();
                List<float> tn = new List<float>();
                List<Color> cs = new List<Color>();

                points.Add(triangle.VertexA);
                points.Add(triangle.VertexB);
                tn.Add(thickness);
                cs.Add(color);

                points.Add(triangle.VertexB);
                points.Add(triangle.VertexC);
                tn.Add(thickness);
                cs.Add(color);

                points.Add(triangle.VertexC);
                points.Add(triangle.VertexA);
                tn.Add(thickness);
                cs.Add(color);
                DrawLines(sb, points, tn, cs);
            }

            sb.End();
            sb.Begin();
        }

        public static void DrawInTriangle(SpriteBatch sb, Triangle posTriangle, Triangle mappingTriangle, Texture2D texture,
            Color color, bool rotationCorrection = true, float thickness = 4f, bool full = false)
        {
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

            var originalState = sb.GraphicsDevice.RasterizerState;
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            rasterizerState.FillMode = FillMode.Solid;
            sb.GraphicsDevice.RasterizerState = rasterizerState;

            Vector2 texSize = texture.Size();
            Vector2[] mappingPoint = new Vector2[]
            {
                mappingTriangle.VertexA,
                mappingTriangle.VertexB,
                mappingTriangle.VertexC
            }, posPoint = new Vector2[]
            {
                posTriangle.VertexA,
                posTriangle.VertexB,
                posTriangle.VertexC
            };
            if (rotationCorrection)
            {
                Array.Sort(mappingPoint, (x1, x2) => (x1.X + x1.Y * texSize.X).CompareTo(x2.X + x2.Y * texSize.X));
                Array.Sort(posPoint, (x1, x2) => (x1.X + x1.Y * Main.screenWidth).CompareTo(x2.X + x2.Y * Main.screenWidth));
            }

            VertexPositionColorTexture[] vertexs = new VertexPositionColorTexture[]
            {
                new VertexPositionColorTexture(new Vector3(posPoint[0], 0f), color,
                new Vector2(mappingPoint[0].X/texSize.X,mappingPoint[0].Y/texSize.Y)),
                new VertexPositionColorTexture(new Vector3(posPoint[1], 0f), color,
                new Vector2(mappingPoint[1].X/texSize.X,mappingPoint[1].Y/texSize.Y)),
                new VertexPositionColorTexture(new Vector3(posPoint[2], 0f), color,
                new Vector2(mappingPoint[2].X/texSize.X,mappingPoint[2].Y/texSize.Y))
            };

            var x = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/DrawInTriangle", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            x.Parameters["uWorldViewProjection"].SetValue(Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1));
            x.Parameters["uScaleFactor"].SetValue(Vector2.One);
            x.Parameters["uPositionFactor"].SetValue(Vector2.Zero);
            x.Parameters["SpriteTexture"].SetValue(texture);
            x.CurrentTechnique.Passes[0].Apply();

            sb.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexs, 0, vertexs.Length / 3);

            sb.GraphicsDevice.RasterizerState = originalState;

            if (thickness > 0f)
            {
                List<Vector2> points = new List<Vector2>();
                List<float> tn = new List<float>();
                List<Color> cs = new List<Color>();

                points.Add(posTriangle.VertexA);
                points.Add(posTriangle.VertexB);
                tn.Add(thickness);
                cs.Add(color);

                points.Add(posTriangle.VertexB);
                points.Add(posTriangle.VertexC);
                tn.Add(thickness);
                cs.Add(color);

                points.Add(posTriangle.VertexC);
                points.Add(posTriangle.VertexA);
                tn.Add(thickness);
                cs.Add(color);
                DrawLines(sb, points, tn, cs);
            }

            sb.End();
            sb.Begin();
        }

        public static void DrawInTriangle(SpriteBatch sb, DrawTriangleInfo drawTriangleInfo, Texture2D texture) =>
            DrawInTriangle(sb, drawTriangleInfo.PositionTriangle, drawTriangleInfo.MappingTriangle, texture, drawTriangleInfo.Color,
                drawTriangleInfo.RotationCorrection, drawTriangleInfo.Thickness, drawTriangleInfo.Full);

        public static void DrawInTriangles(SpriteBatch sb, List<DrawTriangleInfo> drawTriangleInfos, Texture2D texture)
        {
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

            var originalState = sb.GraphicsDevice.RasterizerState;
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            rasterizerState.FillMode = FillMode.Solid;
            sb.GraphicsDevice.RasterizerState = rasterizerState;

            Vector2 texSize = texture.Size();
            List<VertexPositionColorTexture> vertexs = new List<VertexPositionColorTexture>();
            Vector2[] mappingPoint = new Vector2[3], posPoint = new Vector2[3];
            foreach (var info in drawTriangleInfos)
            {
                mappingPoint[0] = info.MappingTriangle.VertexA;
                mappingPoint[1] = info.MappingTriangle.VertexB;
                mappingPoint[2] = info.MappingTriangle.VertexC;
                posPoint[0] = info.PositionTriangle.VertexA;
                posPoint[1] = info.PositionTriangle.VertexB;
                posPoint[2] = info.PositionTriangle.VertexC;
                if (info.RotationCorrection)
                {
                    Array.Sort(mappingPoint, (x1, x2) => (x1.X + x1.Y * texSize.X).CompareTo(x2.X + x2.Y * texSize.X));
                    Array.Sort(posPoint, (x1, x2) => (x1.X + x1.Y * Main.screenWidth).CompareTo(x2.X + x2.Y * Main.screenWidth));
                }

                vertexs.Add(new VertexPositionColorTexture(new Vector3(posPoint[0], 0f), info.Color,
                    new Vector2(mappingPoint[0].X / texSize.X, mappingPoint[0].Y / texSize.Y)));
                vertexs.Add(new VertexPositionColorTexture(new Vector3(posPoint[1], 0f), info.Color,
                new Vector2(mappingPoint[1].X / texSize.X, mappingPoint[1].Y / texSize.Y)));
                vertexs.Add(new VertexPositionColorTexture(new Vector3(posPoint[2], 0f), info.Color,
                new Vector2(mappingPoint[2].X / texSize.X, mappingPoint[2].Y / texSize.Y)));
            }
            var x = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/DrawInTriangle", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            x.Parameters["uWorldViewProjection"].SetValue(Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1));
            x.Parameters["uScaleFactor"].SetValue(Vector2.One);
            x.Parameters["uPositionFactor"].SetValue(Vector2.Zero);
            x.Parameters["SpriteTexture"].SetValue(texture);
            x.CurrentTechnique.Passes[0].Apply();

            sb.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexs.ToArray(), 0, vertexs.Count / 3);

            sb.GraphicsDevice.RasterizerState = originalState;

            List<Vector2> points = new List<Vector2>();
            List<float> tn = new List<float>();
            List<Color> cs = new List<Color>();
            foreach (var info in drawTriangleInfos)
            {
                if (info.Thickness > 0f)
                {
                    points.Add(info.PositionTriangle.VertexA);
                    points.Add(info.PositionTriangle.VertexB);
                    tn.Add(info.Thickness);
                    cs.Add(info.Color);

                    points.Add(info.PositionTriangle.VertexB);
                    points.Add(info.PositionTriangle.VertexC);
                    tn.Add(info.Thickness);
                    cs.Add(info.Color);

                    points.Add(info.PositionTriangle.VertexC);
                    points.Add(info.PositionTriangle.VertexA);
                    tn.Add(info.Thickness);
                    cs.Add(info.Color);
                }
            }
            DrawLines(sb, points, tn, cs);

            sb.End();
            sb.Begin();
        }

        /// <summary>
        /// 绘制三角形
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="triangles"></param>
        /// <param name="colors"></param>
        public static void DrawTriangles(SpriteBatch sb, List<Triangle> triangles, List<Color> colors, List<float> thicknesses = null, bool full = false)
        {
            if (triangles == null || triangles.Count == 0 || colors == null || colors.Count != triangles.Count)
                return;

            Triangle triangle;
            if (full)
            {
                sb.End();
                sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

                var originalState = sb.GraphicsDevice.RasterizerState;
                RasterizerState rasterizerState = new RasterizerState();
                rasterizerState.CullMode = CullMode.None;
                rasterizerState.FillMode = FillMode.Solid;
                sb.GraphicsDevice.RasterizerState = rasterizerState;

                List<VertexPositionColorTexture> vertexs = new List<VertexPositionColorTexture>();
                for (int i = 0; i < triangles.Count; i++)
                {
                    triangle = triangles[i];
                    vertexs.Add(new VertexPositionColorTexture(new Vector3(triangle.VertexA, 0f), colors[i],
                        new Vector2(1f, 1f)));
                    vertexs.Add(new VertexPositionColorTexture(new Vector3(triangle.VertexB, 0f), colors[i],
                        new Vector2(1f, 1f)));
                    vertexs.Add(new VertexPositionColorTexture(new Vector3(triangle.VertexC, 0f), colors[i],
                        new Vector2(0f, 0f)));
                }
                var x = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/TriangleDraw", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                x.Parameters["uWorldViewProjection"].SetValue(Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1));
                x.CurrentTechnique.Passes[0].Apply();

                sb.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexs.ToArray(), 0, triangles.Count);

                sb.GraphicsDevice.RasterizerState = originalState;

                sb.End();
                sb.Begin();
            }
            List<Vector2> points = new List<Vector2>();
            List<float> tn = new List<float>();
            List<Color> cs = new List<Color>();
            if (thicknesses != null && thicknesses.Count > 0)
            {
                for (int i = 0; i < triangles.Count; i++)
                {
                    triangle = triangles[i];
                    points.Add(triangle.VertexA);
                    points.Add(triangle.VertexB);
                    tn.Add(thicknesses[i]);
                    cs.Add(colors[i]);

                    points.Add(triangle.VertexB);
                    points.Add(triangle.VertexC);
                    tn.Add(thicknesses[i]);
                    cs.Add(colors[i]);

                    points.Add(triangle.VertexC);
                    points.Add(triangle.VertexA);
                    tn.Add(thicknesses[i]);
                    cs.Add(colors[i]);
                }
            }
            else
            {
                for (int i = 0; i < triangles.Count; i++)
                {
                    triangle = triangles[i];
                    points.Add(triangle.VertexA);
                    points.Add(triangle.VertexB);
                    tn.Add(4f);
                    cs.Add(colors[i]);

                    points.Add(triangle.VertexB);
                    points.Add(triangle.VertexC);
                    tn.Add(4f);
                    cs.Add(colors[i]);

                    points.Add(triangle.VertexC);
                    points.Add(triangle.VertexA);
                    tn.Add(4f);
                    cs.Add(colors[i]);
                }
            }
            DrawLines(sb, points, tn, cs);
        }

        public static void DrawTriangle(SpriteBatch sb, Triangle triangle, Color color,
            float thicknesses = 4f, bool full = false) => DrawTriangles(sb, new List<Triangle>() { triangle },
                new List<Color>() { color }, new List<float>() { thicknesses }, full);

        /// <summary>
        /// 绘制直线
        /// </summary>
        /// <param name="sb">画布</param>
        /// <param name="startPoint">起点</param>
        /// <param name="endPoint">终点</param>
        /// <param name="thickness"></param>
        /// <param name="size">线条大小</param>
        /// <param name="color">线条颜色</param>
        public static void DrawLine(SpriteBatch sb, Vector2 startPoint, Vector2 endPoint, float thickness, Color color)
        {
            if (thickness == 0)
                return;

            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

            var originalState = sb.GraphicsDevice.RasterizerState;
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            rasterizerState.FillMode = FillMode.Solid;
            sb.GraphicsDevice.RasterizerState = rasterizerState;

            var normalDir = endPoint.Rotated(MathHelper.PiOver2, startPoint) - startPoint;
            normalDir.Normalize();

            VertexPositionColorTexture[] vertexs = new VertexPositionColorTexture[]
            {
                new VertexPositionColorTexture(new Vector3(startPoint,0f),color,new Vector2(0f,0f)),
                new VertexPositionColorTexture(new Vector3(startPoint+normalDir*thickness,0f),color,new Vector2(1f,1f)),
                new VertexPositionColorTexture(new Vector3(endPoint,0f),color,new Vector2(0f,0f)),
                new VertexPositionColorTexture(new Vector3(endPoint,0f),color,new Vector2(1f,1f)),
                new VertexPositionColorTexture(new Vector3(endPoint+normalDir*thickness,0f),color,new Vector2(0f,0f)),
                new VertexPositionColorTexture(new Vector3(startPoint+normalDir*thickness,0f),color,new Vector2(1f,1f)),
            };
            var x = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/TriangleDraw", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            x.Parameters["uWorldViewProjection"].SetValue(Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1));
            x.CurrentTechnique.Passes[0].Apply();

            sb.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexs, 0, vertexs.Length / 3);

            sb.GraphicsDevice.RasterizerState = originalState;

            sb.End();
            sb.Begin();
        }

        public static void DrawLines(SpriteBatch sb, List<Vector2> points, List<float> thicknesses,
            List<Color> colors, float startPos = 0f, float endPos = 1f, Vector2 screenSize = default)
        {
            if (endPos > 1f)
                endPos = 1f;
            if (startPos < 0f)
                startPos = 0f;
            if (points == null || points.Count == 0 || points.Count % 2 != 0 || thicknesses == null || thicknesses.Count != points.Count / 2 ||
                !(colors != null && colors.Count == points.Count / 2) || startPos < 0f || startPos >= endPos || endPos > 1f)
                return;

            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

            var originalState = sb.GraphicsDevice.RasterizerState;
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            rasterizerState.FillMode = FillMode.Solid;
            sb.GraphicsDevice.RasterizerState = rasterizerState;

            List<VertexPositionColorTexture> vertexs = new List<VertexPositionColorTexture>();
            Vector2 startPoint, endPoint, normalDir, normal;
            float thickness;
            Color color = Color.White;
            float maxLength = 0f, nowLength = 0f, lineLength, startLength, endLength, nowLineLength;

            for (int i = 0; i < points.Count; i += 2)
            {
                thickness = thicknesses[i / 2];
                if (thickness <= 0f)
                    continue;
                startPoint = points[i];
                endPoint = points[i + 1];
                maxLength += (endPoint - startPoint).Length();
            }
            startLength = maxLength * startPos;
            endLength = maxLength * endPos;
            for (int i = 0; i < points.Count; i += 2)
            {
                if (nowLength > endLength)
                    break;

                thickness = thicknesses[i / 2];
                if (thickness <= 0f)
                    continue;
                startPoint = points[i];
                endPoint = points[i + 1];
                if (colors != null)
                    color = colors[i / 2];

                normalDir = endPoint.Rotated(MathHelper.PiOver2, startPoint) - startPoint;
                normalDir.Normalize();
                normal = endPoint - startPoint;

                lineLength = normal.Length();
                normal.Normalize();

                nowLineLength = lineLength;
                if (nowLength < startLength)
                {
                    nowLength += lineLength;
                    if (nowLength > startLength)
                    {
                        nowLineLength = startLength - (nowLength - lineLength);
                        startPoint -= nowLineLength * normal;
                        if (nowLength > endLength)
                        {
                            nowLineLength = nowLength - endLength;
                            endPoint -= normal * nowLineLength;
                        }
                        vertexs.Add(new VertexPositionColorTexture(new Vector3(startPoint - normalDir * (thickness / 2f), 0f), color,
                    new Vector2(0f, 0f)));
                        vertexs.Add(new VertexPositionColorTexture(new Vector3(startPoint + normalDir * (thickness / 2f), 0f), color,
                            new Vector2(1f, 1f)));
                        vertexs.Add(new VertexPositionColorTexture(new Vector3(endPoint - normalDir * (thickness / 2f), 0f), color,
                            new Vector2(0f, 0f)));
                        vertexs.Add(new VertexPositionColorTexture(new Vector3(endPoint - normalDir * (thickness / 2f), 0f), color,
                            new Vector2(1f, 1f)));
                        vertexs.Add(new VertexPositionColorTexture(new Vector3(endPoint + normalDir * (thickness / 2f), 0f), color,
                            new Vector2(0f, 0f)));
                        vertexs.Add(new VertexPositionColorTexture(new Vector3(startPoint + normalDir * (thickness / 2f), 0f), color,
                            new Vector2(1f, 1f)));
                    }
                    continue;
                }
                if (nowLength + lineLength > endLength)
                {
                    endPoint -= (nowLength + lineLength - endLength) * normal;
                }

                vertexs.Add(new VertexPositionColorTexture(new Vector3(startPoint - normalDir * (thickness / 2f), 0f), color,
                    new Vector2(0f, 0f)));
                vertexs.Add(new VertexPositionColorTexture(new Vector3(startPoint + normalDir * (thickness / 2f), 0f), color,
                    new Vector2(1f, 1f)));
                vertexs.Add(new VertexPositionColorTexture(new Vector3(endPoint - normalDir * (thickness / 2f), 0f), color,
                    new Vector2(0f, 0f)));
                vertexs.Add(new VertexPositionColorTexture(new Vector3(endPoint - normalDir * (thickness / 2f), 0f), color,
                    new Vector2(1f, 1f)));
                vertexs.Add(new VertexPositionColorTexture(new Vector3(endPoint + normalDir * (thickness / 2f), 0f), color,
                    new Vector2(0f, 0f)));
                vertexs.Add(new VertexPositionColorTexture(new Vector3(startPoint + normalDir * (thickness / 2f), 0f), color,
                    new Vector2(1f, 1f)));

                nowLength += lineLength;
            }
            var x = ModContent.Request<Effect>("OdeMod/Effects/VertexShaders/TriangleDraw", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            if (screenSize == default)
                x.Parameters["uWorldViewProjection"].SetValue(Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1));
            else
                x.Parameters["uWorldViewProjection"].SetValue(Matrix.CreateOrthographicOffCenter(0, screenSize.X, screenSize.Y, 0, 0, 1));
            x.CurrentTechnique.Passes[0].Apply();

            if (vertexs.Count > 0)
                sb.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexs.ToArray(), 0, vertexs.Count / 3);

            sb.GraphicsDevice.RasterizerState = originalState;

            sb.End();
            sb.Begin();
        }

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
            DrawLine(sb, startPoint, endPoint, size.Length(), color);
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
            RenderTarget2D screenRender, RenderTarget2D screenRenderSwap, Color clearColor = default)
        {
            sb.End();
            sb.GraphicsDevice.SetRenderTarget(screenRenderSwap);
            sb.GraphicsDevice.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone);
            sb.Draw(screenRender, Vector2.Zero, Color.White);
            sb.End();

            sb.GraphicsDevice.SetRenderTarget(render);
            sb.GraphicsDevice.Clear(clearColor);
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
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            sb.End();

            sb.GraphicsDevice.SetRenderTarget(Main.screenTarget);
            sb.GraphicsDevice.Clear(Color.Black);
            sb.Begin(SpriteSortMode.Immediate, null, null, null, null, effect);
            sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            sb.End();
            sb.Begin();
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