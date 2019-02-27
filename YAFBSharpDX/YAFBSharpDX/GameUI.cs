using SharpDX.Direct2D1;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YAFBSharpDX
{
    public partial class GameUI : RenderForm
    {
        #region Public Fields
        /// <summary>
        /// 
        /// </summary>
        public WindowRenderTarget WindowRenderTarget;

        /// <summary>
        /// 
        /// </summary>
        public SharpDX.Direct2D1.Factory Direct2DFactory;

        /// <summary>
        /// 
        /// </summary>
        public SharpDX.DirectWrite.Factory DirectWriteFactory;

        /// <summary>
        /// 
        /// </summary>
        public YAFBCore.Networking.Connection Connection;

        /// <summary>
        /// 
        /// </summary>
        public YAFBCore.Networking.UniverseSession Session;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private List<Screens.Screen> screens = new List<Screens.Screen>();

        private YAFBCore.Utils.Mathematics.Size2F windowBounds;

        /// <summary>
        /// 
        /// </summary>
        public GameUI()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            windowBounds = new YAFBCore.Utils.Mathematics.Size2F(ClientSize.Width, ClientSize.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameUI_Load(object sender, EventArgs e)
        {
            Connection = YAFBCore.Networking.ConnectionManager.Connect("ddraghici@gmx.de", "flattiverse=1337");

            //Session = Connection.Join(Connection.UniverseGroups["Time Master"], "dannyd", Connection.UniverseGroups["Time Master"].Teams["None"]);
            //Session = Connection.Join(Connection.UniverseGroups["Mission I"], "dannyd", Connection.UniverseGroups["Mission I"].Teams["None"]);
            Session = Connection.Join(Connection.UniverseGroups["Mission II"], "dannyd", Connection.UniverseGroups["Mission II"].Teams["None"]);
            //Session = Connection.Join(Connection.UniverseGroups["Labyrinth I"], "dannyd", Connection.UniverseGroups["Labyrinth I"].Teams["None"]);

            Brushes.SolidColorBrushes.Init(WindowRenderTarget);

            CreateScreen(Screens.ScreenType.Game);
            //CreateScreen(Screens.ScreenType.Debug);
        }

        /// <summary>
        /// Creates a specific type of screen and adds it to the list
        /// </summary>
        /// <returns></returns>
        public Screens.Screen CreateScreen(Screens.ScreenType screenType)
        {
            Screens.Screen screen;

            switch (screenType)
            {
                case Screens.ScreenType.Debug:
                    screen = new Screens.DebugScreen(this);
                    break;
                case Screens.ScreenType.Game:
                    screen = new Screens.GameScreen(this);
                    break;
                default:
                    return null;
            }

            KeyDown += screen.KeyDown;
            MouseDown += screen.MouseDown;
            MouseMove += screen.MouseMove;
            MouseUp += screen.MouseUp;
            MouseWheel += screen.MouseWheel;

            screen.Close += screen_Close;

            screens.Add(screen);

            return screen;
        }

        /// <summary>
        /// Event whena screen is requesting its closure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void screen_Close(object sender, EventArgs e)
        {
            screens.Remove((Screens.Screen)sender);
        }

        /// <summary>
        /// Renderloop
        /// </summary>
        public void Render()
        {
            WindowRenderTarget.BeginDraw();

            WindowRenderTarget.Clear(Colors.AdvancedColors.DarkGray);

            for (int i = 0; i < screens.Count; i++)
                screens[i].Render(windowBounds, WindowRenderTarget);

            WindowRenderTarget.EndDraw();
        }

        /// <summary>
        /// 
        /// </summary>
        private void enterFullScreenMode()
        {
            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// 
        /// </summary>
        private void leaveFullScreenMode()
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameUI_Resize(object sender, EventArgs e)
        {
            windowBounds = new YAFBCore.Utils.Mathematics.Size2F(ClientSize.Width, ClientSize.Height);

            WindowRenderTarget.Resize(new SharpDX.Size2(ClientSize.Width, ClientSize.Height));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                for (int i = 0; i < screens.Count; i++)
                    if (screens[i] != null)
                        screens[i].Dispose();

                YAFBCore.Networking.ConnectionManager.Close(Connection.Email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
