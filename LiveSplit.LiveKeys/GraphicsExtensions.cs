using System.Drawing;

namespace GraphicsExtensions
{
    public static class GraphicsExtensions
    {
        public static Font AdjustFontSize(this Graphics g, Font font, float minSize, float maxSize, string text, SizeF bounds)
        {
            for (float AdjustedSize = maxSize; AdjustedSize >= minSize; AdjustedSize--)
            {
                var newFont = new Font(font.Name, AdjustedSize, font.Style);
                SizeF newBounds = g.MeasureString(text, newFont);
                if (bounds.Width > newBounds.Width && bounds.Height > newBounds.Height)
                {
                    return newFont;
                }
            }
            return font;
        }

        public static Font AdjustFontSize(this Graphics g, Font font, float minSize, float maxSize, string text, float width, float height)
        {
            return g.AdjustFontSize(font, minSize, maxSize, text, new SizeF(width, height));
        }
    }
}
