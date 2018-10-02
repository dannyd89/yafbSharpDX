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
        /// <summary>
        /// 
        /// </summary>
        public WindowRenderTarget WindowRenderTarget { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SharpDX.Direct2D1.Factory Direct2DFactory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SharpDX.DirectWrite.Factory DirectWriteFactory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GameUI()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            MouseWheel += GameUI_MouseWheel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameUI_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Renderloop
        /// </summary>
        public void Render()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private void EnterFullScreenMode()
        {
            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LeaveFullScreenMode()
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

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameUI_KeyDown(object sender, KeyEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameUI_MouseDown(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameUI_MouseMove(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameUI_MouseWheel(object sender, MouseEventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameUI_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
