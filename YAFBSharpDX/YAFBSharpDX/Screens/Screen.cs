using SharpDX.Direct2D1;
using System;
using System.Windows.Forms;

namespace YAFBSharpDX.Screens
{
    /// <summary>
    /// A screen can be used to display information as a main layer
    /// or also as a popup being layered above the first screen
    /// </summary>
    public abstract class Screen : IDisposable, IEquatable<Screen>
    {
        /// <summary>
        /// Tracks the amount of screens created
        /// </summary>
        private static long idCounter = 0;

        /// <summary>
        /// Id of the screen
        /// </summary>
        public readonly long Id;

        /// <summary>
        /// 
        /// </summary>
        public readonly GameUI Parent;

        /// <summary>
        /// 
        /// </summary>
        public abstract ScreenType ScreenType { get; }

        /// <summary>
        /// Fires when screen is requesting its closure
        /// </summary>
        public event EventHandler Close;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Screen(GameUI parent)
        {
            Id = idCounter++;

            Parent = parent;
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
        /// <param name="renderTarget"></param>
        public abstract void Render(YAFBCore.Utils.Mathematics.Size2F windowBounds, WindowRenderTarget renderTarget);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public virtual void KeyDown(object sender, KeyEventArgs args) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public virtual void MouseDown(object sender, MouseEventArgs args) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public virtual void MouseMove(object sender, MouseEventArgs args) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public virtual void MouseWheel(object sender, MouseEventArgs args) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public virtual void MouseUp(object sender, MouseEventArgs args) { }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Dispose()
        {
            Close?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Screen other)
        {
            if (other == null)
                return false;

            return Id == other.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Screen);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ScreenType.ToString() + " (" + Id.ToString().PadLeft(3, '0') + ")";
        }
    }
}
