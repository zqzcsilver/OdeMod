﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.UI;

namespace OdeMod.UI.OriginalUISystem.UIElements
{
    /// <summary>
    /// 拿取或放置物品的委托类别
    /// </summary>
    /// <param name="mouseItem">鼠标上的物品</param>
    /// <returns></returns>
    public delegate bool CheckPutSlotCondition(Item mouseItem);

    /// <summary>
    /// 更改物品的委托类别
    /// </summary>
    /// <param name="target">物品框</param>
    public delegate void ExchangeItemHandler(UIElement target);

    public class MyUIItemSlot : UIElement, IOriginalUIElement
    {
        /// <summary>
        /// 框贴图
        /// </summary>
        public Texture2D SlotBackTexture { get; set; }

        /// <summary>
        /// 是否可以放置物品
        /// </summary>
        public CheckPutSlotCondition CanPutInSlot { get; set; }

        /// <summary>
        /// 是否可以拿去物品
        /// </summary>
        public CheckPutSlotCondition CanTakeOutSlot { get; set; }

        /// <summary>
        /// 框内物品
        /// </summary>
        public Item ContainedItem { get; set; }

        /// <summary>
        /// 框的绘制的拐角尺寸
        /// </summary>
        public Vector2 CornerSize { get; set; }

        /// <summary>
        /// 绘制颜色
        /// </summary>
        public Color DrawColor { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// 更改物品时调用
        /// </summary>
        public event ExchangeItemHandler PostExchangeItem;

        /// <summary>
        /// 玩家拿取物品时调用
        /// </summary>
        public event ExchangeItemHandler OnPickItem;

        /// <summary>
        /// 玩家放入物品时调用
        /// </summary>
        public event ExchangeItemHandler OnPutItem;

        /// <summary>
        /// 在部件更新时调用
        /// </summary>
        public event ExchangeItemHandler PostUpdate;

        /// <summary>
        /// 透明度
        /// </summary>
        public float Opacity { get; set; }

        public MyUIItemSlot(Texture2D texture = default(Texture2D)) : base()
        {
            Opacity = 1f;
            ContainedItem = new Item();
            CanPutInSlot = null;
            SlotBackTexture = texture == default(Texture2D) ? TextureAssets.InventoryBack.Value : texture;
            DrawColor = new Color(0x3f, 0x41, 0x97) * 0.75f;
            CornerSize = new Vector2(10, 10);
            Tooltip = "";
            Width.Set(50, 0);
            Height.Set(50, 0);
        }

        public override void Update(GameTime gameTime)
        {
            PostUpdate?.Invoke(this);
            base.Update(gameTime);
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            //开启背包
            //Main.playerInventory = true;

            //当鼠标没物品，框里有物品的时候
            if (Main.mouseItem.type == 0 && ContainedItem.type != 0)
            {
                //如果可以拿起物品
                if (CanTakeOutSlot == null || CanTakeOutSlot(ContainedItem))
                {
                    //拿出物品
                    Main.mouseItem = ContainedItem.Clone();
                    ContainedItem = new Item();
                    ContainedItem.SetDefaults(0, true);

                    //调用委托
                    OnPickItem?.Invoke(this);

                    //触发放物品声音
                    //SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0.0f);
                }
            }
            //当鼠标有物品，框里没物品的时候
            else if (Main.mouseItem.type != 0 && ContainedItem.type == 0)
            {
                //如果可以放入物品
                if (CanPutInSlot == null || CanPutInSlot(Main.mouseItem))
                {
                    //放入物品
                    ContainedItem = Main.mouseItem.Clone();
                    Main.mouseItem = new Item();
                    Main.mouseItem.SetDefaults(0, true);

                    //调用委托
                    OnPutItem?.Invoke(this);

                    //触发放物品声音
                    //SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0.0f);
                }
            }
            //当鼠标和框都有物品时
            else if (Main.mouseItem.type != 0 && ContainedItem.type != 0)
            {
                //如果不能放入物品
                if (!(CanPutInSlot == null || CanPutInSlot(Main.mouseItem)))
                {
                    //中断函数
                    return;
                }

                //如果框里的物品和鼠标的相同
                if (Main.mouseItem.type == ContainedItem.type)
                {
                    //框里的物品数量加上鼠标物品数量
                    ContainedItem.stack += Main.mouseItem.stack;
                    //如果框里物品数量大于数量上限
                    if (ContainedItem.stack > ContainedItem.maxStack)
                    {
                        //计算鼠标物品数量，并将框内物品数量修改为数量上限
                        var exceed = ContainedItem.stack - ContainedItem.maxStack;
                        ContainedItem.stack = ContainedItem.maxStack;
                        Main.mouseItem.stack = exceed;
                    }
                    //反之
                    else
                    {
                        //清空鼠标物品
                        Main.mouseItem = new Item();
                    }
                }
                //如果可以放入物品也能拿出物品
                else if ((CanPutInSlot == null || CanPutInSlot(Main.mouseItem))
                    && (CanTakeOutSlot == null || CanTakeOutSlot(ContainedItem)))
                {
                    //交换框内物品和鼠标物品
                    var tmp = Main.mouseItem.Clone();
                    Main.mouseItem = ContainedItem;
                    ContainedItem = tmp;
                }

                //触发放物品声音
                //SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0.0f);
            }
            //反之
            else
            {
                //中断函数
                return;
            }

            //调用委托
            PostExchangeItem?.Invoke(this);
            base.LeftClick(evt);
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            base.DrawSelf(sb);

            float scale = Width.Pixels / 50f;
            //调用原版的介绍绘制
            if (ContainsPoint(Main.MouseScreen) && ContainedItem.type != 0)
            {
                Main.hoverItemName = ContainedItem.Name;
                Main.HoverItem = ContainedItem.Clone();
            }
            //获取当前UI部件的信息
            var DrawRectangle = GetDimensions();
            //绘制物品框
            DrawAdvBox(sb, (int)DrawRectangle.X, (int)DrawRectangle.Y,
                (int)DrawRectangle.Width, (int)DrawRectangle.Height,
                DrawColor * Opacity, SlotBackTexture, CornerSize, 1f);
            if (ContainedItem.type != 0)
            {
                var frame = Main.itemAnimations[ContainedItem.type] != null ? Main.itemAnimations[ContainedItem.type].GetFrame(TextureAssets.Item[ContainedItem.type].Value) : Item.GetDrawHitbox(ContainedItem.type, null);
                var size = frame.Size();
                var texScale = 1f;
                if (size.X > DrawRectangle.Width || size.Y > DrawRectangle.Height)
                {
                    texScale = size.X > size.Y ? size.X / DrawRectangle.Width : size.Y / DrawRectangle.Height;
                    texScale = 0.7f / texScale;
                    size *= texScale;
                }
                //绘制物品贴图
                sb.Draw(TextureAssets.Item[ContainedItem.type].Value, new Vector2(DrawRectangle.X + DrawRectangle.Width / 2,
                    DrawRectangle.Y + DrawRectangle.Height / 2) - (/*TextureAssets.Item[ContainedItem.type].Value.Size() / 2f * scale */ new Vector2(frame.Width, frame.Height) / 2f * scale),
                    new Rectangle?(frame), Color.White * Opacity, 0f, Vector2.Zero, scale, 0, 0);

                //绘制物品左下角那个代表数量的数字
                if (ContainedItem.stack > 1)
                {
                    sb.DrawString(FontAssets.MouseText.Value, ContainedItem.stack.ToString(), new Vector2(DrawRectangle.X + 10, DrawRectangle.Y + DrawRectangle.Height - 20), Color.White * Opacity, 0f, Vector2.Zero, scale * 0.8f, SpriteEffects.None, 0f);
                }
            }
        }

        /// <summary>
        /// 绘制物品框
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="c"></param>
        /// <param name="img"></param>
        /// <param name="size4"></param>
        /// <param name="scale"></param>
        public void DrawAdvBox(SpriteBatch sp, int x, int y, int w, int h, Color c, Texture2D img, Vector2 size4, float scale = 1f)
        {
            var box = img;
            var nw = (int)(w * scale);
            var nh = (int)(h * scale);
            x += (w - nw) / 2;
            y += (h - nh) / 2;
            w = nw;
            h = nh;
            var width = (int)size4.X;
            var height = (int)size4.Y;
            if (w < size4.X)
            {
                w = width;
            }
            if (h < size4.Y)
            {
                h = width;
            }
            sp.Draw(box, new Rectangle(x, y, width, height), new Rectangle(0, 0, width, height), c);
            sp.Draw(box, new Rectangle(x + width, y, w - width * 2, height), new Rectangle(width, 0, box.Width - width * 2, height), c);
            sp.Draw(box, new Rectangle((x + w) - width, y, width, height), new Rectangle(box.Width - width, 0, width, height), c);
            sp.Draw(box, new Rectangle(x, y + height, width, h - height * 2), new Rectangle(0, height, width, box.Height - height * 2), c);
            sp.Draw(box, new Rectangle(x + width, y + height, w - width * 2, h - height * 2), new Rectangle(width, height, box.Width - width * 2, box.Height - height * 2), c);
            sp.Draw(box, new Rectangle((x + w) - width, y + height, width, h - height * 2), new Rectangle(box.Width - width, height, width, box.Height - height * 2), c);
            sp.Draw(box, new Rectangle(x, (y + h) - height, width, height), new Rectangle(0, box.Height - height, width, height), c);
            sp.Draw(box, new Rectangle(x + width, (y + h) - height, w - width * 2, height), new Rectangle(width, box.Height - height, box.Width - width * 2, height), c);
            sp.Draw(box, new Rectangle((x + w) - width, (y + h) - height, width, height), new Rectangle(box.Width - width, box.Height - height, width, height), c);
        }
    }
}