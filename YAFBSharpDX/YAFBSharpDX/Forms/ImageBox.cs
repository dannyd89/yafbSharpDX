using SharpDX;
using SharpDX.Direct2D1;

namespace YAFBSharpDX.Forms
{
    public class ImageBox : IFixedDrawable
    {
        public float X;
        public float Y;
        public readonly SharpDX.Direct2D1.Bitmap Bitmap;
        public RectangleF Position;

        public ImageBox(Bitmap bitmap, float x, float y)
        {
            Bitmap = bitmap;
            X = x;
            Y = y;

            Position = new RectangleF(x, y, bitmap.PixelSize.Width, bitmap.PixelSize.Height);
        }

        public void Draw(RenderTarget renderTarget)
        {
            renderTarget.DrawBitmap(Bitmap, Position, 1f, BitmapInterpolationMode.Linear);
        }
    }
}
