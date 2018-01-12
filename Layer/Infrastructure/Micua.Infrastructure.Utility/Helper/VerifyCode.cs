namespace Micua.Infrastructure.Utility
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    /// <summary> 
    /// 验证码类
    /// </summary> 
    public class VerifyCode
    {
        #region 字段
        private static readonly string[] FontItems =
            { 
                "Arial", 
                "Helvetica", 
                "Geneva", 
                "sans-serif", 
                "Verdana" 
            };
        private static readonly Brush[] BrushItems =
            { 
                Brushes.OliveDrab, 
                Brushes.ForestGreen, 
                Brushes.DarkCyan, 
                Brushes.LightSlateGray, 
                Brushes.RoyalBlue, 
                Brushes.SlateBlue, 
                Brushes.DarkViolet, 
                Brushes.MediumVioletRed, 
                Brushes.IndianRed, 
                Brushes.Firebrick, 
                Brushes.Chocolate, 
                Brushes.Peru, 
                Brushes.Goldenrod 
            };
        private static readonly string[] BrushName =
            { 
                "OliveDrab",
                "ForestGreen", 
                "DarkCyan", 
                "LightSlateGray", 
                "RoyalBlue", 
                "SlateBlue",
                "DarkViolet", 
                "MediumVioletRed", 
                "IndianRed", 
                "Firebrick", 
                "Chocolate", 
                "Peru", 
                "Goldenrod" 
            };
        private readonly Color _background;
        private readonly Pen _borderColor;
        private readonly Font _font;
        private readonly int _width;
        private readonly int _height;
        private readonly Random _random;
        private int _brushNameIndex;
        #endregion

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; private set; }

        #region 构造函数

        /// <summary>
        /// 构造一个验证码对象
        /// </summary>
        /// <param name="length">验证码长度</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="background">背景色</param>
        /// <param name="border">边框颜色</param>
        public VerifyCode(byte length = 4, short width = 50, short height = 20, float fontSize = 12, Color background = default(Color), Pen border = default(Pen))
        {
            this._random = new Random();
            this._background = background;
            this._borderColor = border ?? Pens.White;
            this._font = this.GetFont(fontSize);
            this._width = width;
            this._height = height;
            this.Code = GetRandomCode(length);
        }
        #endregion

        #region 取得一个指定位数的随机码 _static string GetRandomCode(int length)
        /// <summary> 
        /// 取得一个指定位数的随机码 
        /// </summary> 
        /// <returns></returns> 
        private static string GetRandomCode(int length)
        {
            return Guid.NewGuid().ToString("N").Substring(0, length);
        }
        #endregion

        #region 随机取一个字体 _Font GetFont()
        /// <summary> 
        /// 随机取一个字体 
        /// </summary> 
        /// <returns></returns> 
        private Font GetFont(float fontSize)
        {
            int fontIndex = this._random.Next(0, FontItems.Length);
            FontStyle fontStyle = GetFontStyle(this._random.Next(0, 2));
            return new Font(FontItems[fontIndex], fontSize, fontStyle);
        }
        #endregion

        #region 取一个字体的样式 _static FontStyle GetFontStyle(int index)
        /// <summary> 
        /// 取一个字体的样式 
        /// </summary> 
        /// <param name="index"></param> 
        /// <returns></returns> 
        private static FontStyle GetFontStyle(int index)
        {
            switch (index)
            {
                case 0:
                    return FontStyle.Bold;
                case 1:
                    return FontStyle.Italic;
                default:
                    return FontStyle.Regular;
            }
        }
        #endregion

        #region 随机取一个笔刷 _Brush GetBrush()
        /// <summary> 
        /// 随机取一个笔刷 
        /// </summary> 
        /// <returns></returns> 
        private Brush GetBrush()
        {
            int brushIndex = this._random.Next(0, BrushItems.Length);
            this._brushNameIndex = brushIndex;
            return BrushItems[brushIndex];
        }
        #endregion

        #region 画出位图 +Bitmap DrawBitmap()
        /// <summary> 
        /// 画出位图，注意对返回值使用过后的资源释放 
        /// </summary> 
        public byte[] DrawBitmap()
        {
            using (var image = new Bitmap(this._width, this._height))
            {
                using (var g = Graphics.FromImage(image))
                {
                    this.PaintBackground(g);
                    this.PaintText(g);
                    this.PaintTextStain(image);
                    this.PaintBorder(g);
                    //保存图片数据
                    var stream = new MemoryStream();
                    image.Save(stream, ImageFormat.Gif);
                    //输出图片流
                    return stream.ToArray();
                }
            }
        }
        /// <summary> 
        /// 绘画背景颜色 
        /// </summary> 
        /// <param name="g"></param> 
        private void PaintBackground(Graphics g)
        {
            g.Clear(this._background);
        }
        /// <summary> 
        /// 绘画边框 
        /// </summary> 
        /// <param name="g"></param> 
        private void PaintBorder(Graphics g)
        {
            g.DrawRectangle(this._borderColor, 0, 0, this._width - 1, this._height - 1);
        }
        /// <summary> 
        /// 绘画文字 
        /// </summary> 
        /// <param name="g"></param> 
        private void PaintText(Graphics g)
        {
            g.DrawString(this.Code, this._font, this.GetBrush(), 3, 1);
        }
        /// <summary> 
        /// 绘画文字噪音点和干扰线
        /// </summary> 
        /// <param name="b"></param> 
        private void PaintTextStain(Bitmap b)
        {
            for (int n = 0; n < 50; n++)
            {
                int x = this._random.Next(this._width);
                int y = this._random.Next(this._height);
                b.SetPixel(x, y, Color.FromName(BrushName[this._brushNameIndex]));
            }
        }
        #endregion
    }
}