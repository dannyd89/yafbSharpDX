using SharpDX.Direct2D1;

namespace YAFBSharpDX.Forms
{
    /// <summary>
    /// Used for fixed non-scaled drawables 
    /// </summary>
    interface IFixedDrawable
    {
        void Draw(RenderTarget renderTarget);
    }
}
