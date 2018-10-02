using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
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
        /// Current main window
        /// </summary>
        private WindowRenderTarget renderTarget;

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

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        private CanvasRenderTarget FrontBuffer => accumulationBuffers[currentBuffer];

        /// <summary>
        /// 
        /// </summary>
        private CanvasRenderTarget BackBuffer => accumulationBuffers[(currentBuffer + 1) % 2];
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
        private void window_PointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            if (screens != null)
                for (int i = 0; i < screens.Count; i++)
                    screens[i].PointerPressed(sender, args);
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

        /// <summary>
        /// 
        /// </summary>
        public void Render()
        {
            swapChainManager.EnsureMatchesWindow(window);

            // Swap the accumulation buffer
            //currentBuffer = (currentBuffer + 1) % 2;

            //ensureCurrentBufferMatchesWindow();

            //using (var ds = FrontBuffer.CreateDrawingSession())
            //{
            //    ds.Clear(Colors.Transparent);

            //    accumulateBackBufferOntoFrontBuffer(ds);
            //    //DrawContactGeometry(ds, geom);
            //}

            var swapChain = swapChainManager.SwapChain;

            Rect bounds = window.Bounds;

            using (var ds = swapChain.CreateDrawingSession(Colors.White))
            {
                foreach (Screen screen in screens)
                    screen.Render(new YAFBCore.Utils.Mathematics.RectangleF((float)bounds.X, (float)bounds.Y, (float)bounds.Width, (float)bounds.Height), ds);

                //for (int i = 0; i < circles.Count; i++)
                //    ds.DrawGeometry(circles[i], Colors.Blue);

                //for (int i = 0; i < 3000; i++)
                //    ds.DrawImage(bitmap, (float)(random.NextDouble() * bounds.Width), (float)(random.NextDouble() * bounds.Height));

                //for (int i = 0; i < 40000; i++)
                //    ds.DrawCircle((float)(random.NextDouble() * bounds.Width), (float)(random.NextDouble() * bounds.Height), (float)(random.NextDouble() * 10f + 5f), Colors.Blue);

                //for (int i = 0; i < 80000; i++)
                //    ds.DrawRectangle((float)(random.NextDouble() * bounds.Width), (float)(random.NextDouble() * bounds.Height), (float)(random.NextDouble() * 20f + 5f), (float)(random.NextDouble() * 20f + 5f), imageBrush);

                //Stopwatch sw = Stopwatch.StartNew();

                //CanvasSvgNamedElement element = spaceShipSvg.FindElementById("test");

                //element.SetColorAttribute("fill", Color.FromArgb(255, (byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)));
                //element.SetTransformAttribute("transform", Matrix3x2.CreateRotation(toRadiant((float)random.NextDouble() * 359f), new Vector2(109.8f, 109.8f)));
                //ds.DrawSvg(spaceShipSvg, new Size(100, 100), new Vector2(100f, 100f));

                //element.SetColorAttribute("fill", Colors.IndianRed);
                //element.SetTransformAttribute("transform", Matrix3x2.CreateRotation(toRadiant((float)random.NextDouble() * 359f), new Vector2(109.8f, 109.8f)));
                //ds.DrawSvg(spaceShipSvg, new Size(100, 100), new Vector2(200f, 200f));

                //Debug.WriteLine("Time: " + sw.Elapsed);

                //ds.DrawRectangle(new Rect(100, 100, 100, 100), Colors.Black);

                //ds.DrawSvg(spaceShipSvg, new Size(100, 100), new Vector2(200f, 200f));

                //for (int i = 0; i < 2; i++)
                //{
                //    double size = random.NextDouble() * 100 + 5;

                //    ds.DrawSvg(spaceShipSvg, new Size(size, size + 5), new Vector2(Math.Max(5f, (float)(random.NextDouble() * bounds.Width - 105)), Math.Max(5f, (float)(random.NextDouble() * bounds.Height - 105))));


                //}

                //ds.DrawImage(FrontBuffer);

                //DrawFaceInCenterOfGeometry(ds, geom);
                //DrawContactPoints(ds);
                //DrawInfo(ds);
                //DrawTitle(ds, swapChain.Size);
            }

            swapChain.Present();
        }

        private float toRadiant(float degree)
        {
            return degree * ((float)Math.PI / 180f);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ensureCurrentBufferMatchesWindow()
        {
            var bounds = window.Bounds;
            Size windowSize = new Size(bounds.Width, bounds.Height);
            float dpi = DisplayInformation.GetForCurrentView().LogicalDpi;

            var buffer = accumulationBuffers[currentBuffer];

            if (buffer == null || !(SwapChainManager.SizeEqualsWithTolerance(buffer.Size, windowSize)) || buffer.Dpi != dpi)
            {
                if (buffer != null)
                    buffer.Dispose();

                buffer = new CanvasRenderTarget(device, (float)windowSize.Width, (float)windowSize.Height, dpi);
                accumulationBuffers[currentBuffer] = buffer;
            }
        }

        /// <summary>
        /// Hier werden die Buffer geswapt und der Background gezeichnet
        /// </summary>
        /// <param name="ds"></param>
        private void accumulateBackBufferOntoFrontBuffer(CanvasDrawingSession ds)
        {
            // If this is the first frame then there's no back buffer
            if (BackBuffer == null)
                return;

            //inputEffect.Source = BackBuffer;

            //// Adjust the scale, so that if the front and back buffer are different sizes (eg the window was resized) 
            //// then the contents is scaled up as appropriate.
            //var scaleX = FrontBuffer.Size.Width / BackBuffer.Size.Width;
            //var scaleY = FrontBuffer.Size.Height / BackBuffer.Size.Height;

            //var transform = Matrix3x2.CreateScale((float)scaleX, (float)scaleY);

            //// we do a bit of extra scale for effect
            //transform *= Matrix3x2.CreateScale(1.01f, 1.01f, FrontBuffer.Size.ToVector2() / 2);

            //outputEffect.TransformMatrix = transform;

            //imageBrush.Image = outputEffect;
            //imageBrush.SourceRectangle = new Rect(0, 0, FrontBuffer.Size.Width, FrontBuffer.Size.Height);
            //ds.FillRectangle(imageBrush.SourceRectangle.Value, imageBrush);
        }
    }
}
