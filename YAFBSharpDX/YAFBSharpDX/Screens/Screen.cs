using SharpDX.Direct2D1;
using System;

namespace YAFBSharpDX.Screens
{
    /// <summary>
    /// A screen can be used to display information as a main layer
    /// or also as a popup being layered above the first screen
    /// </summary>
    abstract class Screen : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Screen(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Used to load needed assets
        /// </summary>
        public virtual void Load()
        { }

        /// <summary>
        /// Renders the screen content
        /// </summary>
        /// <param name="windowBounds"></param>
        /// <param name="drawingSession"></param>
        public abstract void Render(YAFBCore.Utils.Mathematics.RectangleF windowBounds, WindowRenderTarget renderTarget);

        /// <summary>
        /// 
        /// </summary>
        public virtual void Dispose()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
