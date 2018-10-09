using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using YAFBCore.Controllables;
using YAFBCore.Mapping;
using YAFBCore.Mapping.Units;
using YAFBCore.Networking;
using YAFBCore.Utils;
using YAFBCore.Utils.Mathematics;

namespace YAFBSharpDX.Screens
{
    /// <summary>
    /// 
    /// </summary>
    public class GameScreen : Screen
    {
        private GameUI parent;
        private UniverseSession universeSession;
        private MapManager mapManager;
        private Ship ship;

        private Transformator X;
        private Transformator Y;
        
        private float scale = 2f;
        private float viewCenterX;
        private float viewCenterY;

        private float destinationViewCenterX;
        private float destinationViewCenterY;

        private float currentMouseX;
        private float currentMouseY;

        /// <summary>
        /// 
        /// </summary>
        public override ScreenType ScreenType => ScreenType.Game;

        /// <summary>
        /// 
        /// </summary>
        public GameScreen(GameUI parent) 
            : base(parent)
        {
            this.parent = parent;
            universeSession = parent.Session;
            mapManager = universeSession.MapManager;

            ship = universeSession.ControllablesManager.CreateShip("D1RP", "D1RP");

            ship.TryContinue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowBounds"></param>
        /// <param name="renderTarget"></param>
        public override void Render(Size2F windowBounds, WindowRenderTarget renderTarget)
        {
            viewCenterX += (destinationViewCenterX - viewCenterX) / 5f;
            viewCenterY += (destinationViewCenterY - viewCenterY) / 5f;

            RectangleF sourceRect;

            PlayerShipMapUnit shipUnit;
            if (ship != null && ship.IsAlive && mapManager.TryGetPlayerUnit(ship.Universe.Name, ship.Name, out shipUnit))
                sourceRect = getSourceRectangleF(shipUnit.Position.X, shipUnit.Position.Y, scale, windowBounds.Width, windowBounds.Height);
            else
                sourceRect = getSourceRectangleF(viewCenterX, viewCenterY, scale, windowBounds.Width, windowBounds.Height);

            X = new Transformator(sourceRect.Left, sourceRect.Right, 0, windowBounds.Width);
            Y = new Transformator(sourceRect.Top, sourceRect.Bottom, 0, windowBounds.Height);

            // TODO: Default Universe muss gesetzt werden falls ein Schiff nicht am Leben ist

            List<MapUnit> unitList;
            if (mapManager.TryGetUnits(ship.Universe.Name, sourceRect, out unitList) && unitList.Count > 0)
            {
                // TODO: Draw HUD

                drawUnits(renderTarget, unitList);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #region Private functions
        /// <summary>
        /// Calculates the source rectangle where we are currently looking at
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="targetScale"></param>
        /// <param name="targetWidth"></param>
        /// <param name="targetHeight"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RectangleF getSourceRectangleF(float dx, float dy, float targetScale, float targetWidth, float targetHeight)
        {
            return new RectangleF(dx - targetWidth / targetScale,
                                  dy - targetHeight / targetScale,
                                  targetWidth / targetScale * 2f,
                                  targetHeight / targetScale * 2f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="renderTarget"></param>
        /// <param name="unitList"></param>
        private void drawUnits(WindowRenderTarget renderTarget, List<MapUnit> unitList)
        {
            for (int i = 0; i < unitList.Count; i++)
            {
                MapUnit mapUnit = unitList[i];

                SharpDX.Vector2 position = new SharpDX.Vector2(X[mapUnit.Position.X], Y[mapUnit.Position.Y]);
                float radius = X.Prop(mapUnit.Radius);

                switch (mapUnit)
                {
                    case PlanetMapUnit planetMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.DarkGreen, position, radius);
                        break;
                    case PlayerShipMapUnit playerShipMapUnit:
                        if (playerShipMapUnit.IsOwnShip)
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.LightBlue, position, radius);
                        else
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.IndianRed, position, radius);
                        break;
                    case SunMapUnit sunMapUnit:
                        for (int c = 0; c < sunMapUnit.CoronaInfos.Length; c++)
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.LightYellow, position, X.Prop(sunMapUnit.CoronaInfos[c].Radius));

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.OrangeRed, position, radius);
                        break;
                    default:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, X.Prop(mapUnit.Radius));
                        break;
                }
            }
        }
        #endregion
    }
}
