using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using YAFBSharpDX.Screens;

namespace YAFBSharpDX
{
    class ScreenManager
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        public static ScreenManager Instance { get; private set; }

        /// <summary>
        /// Indicates if managers was already disposed
        /// </summary>
        private bool isDisposed;

        /// <summary>
        /// Current main render target
        /// </summary>
        private WindowRenderTarget renderTarget;

        /// <summary>
        /// 
        /// </summary>
        private SharpDX.Direct2D1.Factory direct2DFactory;

        /// <summary>
        /// 
        /// </summary>
        private List<Screen> screens;

        /// <summary>
        /// 
        /// </summary>
        public readonly YAFBCore.Networking.Connection Connection;

        /// <summary>
        /// 
        /// </summary>
        public YAFBCore.Networking.UniverseSession Session;
        #endregion

        /// <summary>
        /// Creates a ScreenManager
        /// </summary>
        /// <param name="coreWindow"></param>
        /// <returns></returns>
        public static ScreenManager Create(WindowRenderTarget renderTarget, SharpDX.Direct2D1.Factory direct2DFactory, SharpDX.DirectWrite.Factory directWriteFactory)
        {
            if (Instance != null)
            {
                Instance.Dispose();
                Instance = null;
            }

            Instance = new ScreenManager(renderTarget, direct2DFactory, directWriteFactory);
            return Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        private ScreenManager(WindowRenderTarget renderTarget, SharpDX.Direct2D1.Factory direct2DFactory, SharpDX.DirectWrite.Factory directWriteFactory)
        {
            this.window = window;
            window.PointerPressed += window_PointerPressed;
            window.PointerMoved += window_PointerMoved;
            window.PointerReleased += window_PointerReleased;

            device = new CanvasDevice();
            swapChainManager = new SwapChainManager(window, device);

            Connection = YAFBCore.Networking.ConnectionManager.Connect("ddraghici@gmx.de", "flattiverse=1337");

            Session = Connection.Join(Connection.UniverseGroups["Time Master"], "dannyd", Connection.UniverseGroups["Time Master"].Teams["None"]);
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Load()
        {
            screens = new List<Screen> { new GameScreen() };

            foreach (Screen screen in screens)
                screen.Load(device);

            //var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/spaceship.svg"));
            //using (var fileStream = await file.OpenReadAsync())
            //    spaceShipSvg = await CanvasSvgDocument.LoadAsync(device, fileStream);

            //bitmap = await CanvasBitmap.LoadAsync(device, "Assets/test.jpg");
            //imageBrush = new CanvasImageBrush(device, bitmap);
        }

        /// <summary>
        /// 
        /// </summary>
        private void Dispose()
        {
            if (isDisposed)
                throw new ObjectDisposedException("ScreenManager");

            isDisposed = true;

            device.Dispose();

            if (screens != null)
            {
                foreach (Screen screen in screens)
                    screen.Dispose();

                screens = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void MouseDown(object sender, MouseEventArgs args)
        {
            if (screens != null)
                for (int i = 0; i < screens.Count; i++)
                    screens[i].MouseDown(args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void window_PointerMoved(CoreWindow sender, PointerEventArgs args)
        {
            if (screens != null)
                for (int i = 0; i < screens.Count; i++)
                    screens[i].PointerMoved(sender, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void window_PointerReleased(CoreWindow sender, PointerEventArgs args)
        {
            if (screens != null)
                for (int i = 0; i < screens.Count; i++)
                    screens[i].PointerReleased(sender, args);
        }
        
    }
}
