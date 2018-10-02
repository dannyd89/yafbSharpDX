using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YAFBSharpDX
{
    static class Program
    {
        private static WindowRenderTarget renderTarget = null;
        private static SharpDX.Direct2D1.Factory direct2DFactory = new SharpDX.Direct2D1.Factory(FactoryType.SingleThreaded);
        private static SharpDX.DirectWrite.Factory directWriteFactory = new SharpDX.DirectWrite.Factory(SharpDX.DirectWrite.FactoryType.Isolated);
        private static RenderTargetProperties renderTargetProperties;
        private static HwndRenderTargetProperties hwndProperties;
        private static GameUI gameUI;

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            gameUI = new GameUI();

            renderTargetProperties = new RenderTargetProperties(new PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied));

            hwndProperties = new HwndRenderTargetProperties();
            hwndProperties.Hwnd = gameUI.Handle;
            hwndProperties.PixelSize = new SharpDX.Size2(gameUI.ClientSize.Width, gameUI.ClientSize.Height);
            hwndProperties.PresentOptions = PresentOptions.None;
            renderTarget = new WindowRenderTarget(direct2DFactory, renderTargetProperties, hwndProperties);

            gameUI.WindowRenderTarget  = renderTarget;
            gameUI.Direct2DFactory = direct2DFactory;
            gameUI.DirectWriteFactory = directWriteFactory;

            RenderLoop.Run(gameUI, gameUI.Render);
        }
    }
}
