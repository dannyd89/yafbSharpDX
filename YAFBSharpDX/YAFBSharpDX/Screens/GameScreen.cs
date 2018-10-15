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

        #region Disposable graphic elements
        private StrokeStyle dashedStrokeStyle;
        #endregion

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

            dashedStrokeStyle = new StrokeStyle(parent.Direct2DFactory, new StrokeStyleProperties() { DashStyle = DashStyle.Dash, DashCap = CapStyle.Flat });

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
            dashedStrokeStyle.Dispose();

            base.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
                float radius = Math.Max(X.Prop(mapUnit.Radius), 0.01f);
                
                switch (mapUnit)
                {
                    case AIBaseMapUnit aiBaseMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.IndianRed, position, radius);

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, X.Prop(mapUnit.Radius - 3f));
                        break;
                    case AIDroneMapUnit aiDroneMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.IndianRed, position, radius);

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, X.Prop(mapUnit.Radius - 3f));
                        break;
                    case AIPlatformMapUnit aiPlatformMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.IndianRed, position, radius);

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, X.Prop(mapUnit.Radius - 2f));
                        break;
                    case AIProbeMapUnit aiProbeMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.IndianRed, position, radius);

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, X.Prop(mapUnit.Radius - 2f));
                        break;
                    case AIShipMapUnit aiShipMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.IndianRed, position, radius);

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, X.Prop(mapUnit.Radius - 2f));
                        break;
                    case AsteroidMapUnit asteroidMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.LightPink, position, radius);
                        break;
                    case BlackHoleMapUnit blackHoleMapUnit:
                        for (int c = 0; c < blackHoleMapUnit.GravityWellInfos.Length; c++)
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.BlueViolet, position, X.Prop(blackHoleMapUnit.GravityWellInfos[c].Radius), 1f, dashedStrokeStyle);

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Violet, position, radius);
                        break;
                    case BuoyMapUnit buoyMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, radius);
                        break;
                    case CloakPowerUpMapUnit cloakPowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.LightGray, position, radius);
                        break;
                    case DoubleDamagePowerUpMapUnit doubleDamagePowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.LightBlue, position, radius);
                        break;
                    case EnergyPowerUpMapUnit energyPowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.Yellow, position, radius);
                        break;
                    case ExplosionMapUnit explosionMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Red, position, X.Prop(radius * explosionMapUnit.CurrentAge / explosionMapUnit.AgeMax));
                        break;
                    case GateMapUnit gateMapUnit:
                        if (gateMapUnit.Switched)
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Red, position, radius);
                        else
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Green, position, radius, 1f, dashedStrokeStyle);
                        break;
                    case HastePowerUpMapUnit hastePowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.Red, position, radius);
                        break;
                    case HullPowerUpMapUnit hullPowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.RosyBrown, position, radius);
                        break;
                    case IonsPowerUpMapUnit ionsPowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.GreenYellow, position, radius);
                        break;
                    case MeteoroidMapUnit meteoroidMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.RosyBrown, position, radius);
                        break;
                    case MissionTargetMapUnit missionTargetMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, radius);

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, X.Prop(mapUnit.Radius - 2f));
                        break;
                    case MoonMapUnit moonMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.LightGray, position, radius);
                        break;
                    case NebulaMapUnit nebulaMapUnit:
                        // TODO: Nebula color handling
                        break;
                    case ParticlesPowerUpMapUnit particlesPowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.LightSeaGreen, position, radius);
                        break;
                    case PixelMapUnit pixelMapUnit:
                        // Skip this for now
                        // Unused anyway
                        break;
                    case PlanetMapUnit planetMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.DarkGreen, position, radius);
                        break;

                    // TODO: Same handling as playership needed
                    case PlayerBaseMapUnit playerBaseMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.DarkGreen, position, radius);
                        break;
                    case PlayerDroneMapUnit playerDroneMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.DarkGreen, position, radius);
                        break;
                    case PlayerPlatformMapUnit playerPlatformMapUnit:
                        break;
                    case PlayerProbeMapUnit playerProbeMapUnit:
                        break;

                    case PlayerShipMapUnit playerShipMapUnit:
                        if (playerShipMapUnit.IsOwnShip)
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.LightBlue, position, radius);
                        else
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.IndianRed, position, radius);
                        break;
                    case QuadDamagePowerUpMapUnit quadDamagePowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.CadetBlue, position, radius);
                        break;
                    case ShieldPowerUpMapUnit shieldPowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.Violet, position, radius);
                        break;
                    case ShotMapUnit shotMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Red, position, radius);
                        break;
                    case ShotProductionPowerUpMapUnit shotProductionPowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.LightGoldenrodYellow, position, radius);
                        break;
                    case SpaceJellyFishMapUnit spaceJellyFishMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.LightSeaGreen, position, radius);
                        break;
                    case SpaceJellyFishSlimeMapUnit spaceJellyFishSlimeMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.LimeGreen, position, radius, 1f, dashedStrokeStyle);
                        break;

                    case StormCommencingWhirlMapUnit stormCommencingWhirlMapUnit:
                        break;
                    case StormMapUnit stormMapUnit:
                        break;
                    case StormWhirlMapUnit stormWhirlMapUnit:
                        break;

                    case SunMapUnit sunMapUnit:
                        for (int c = 0; c < sunMapUnit.CoronaInfos.Length; c++)
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.LightYellow, position, X.Prop(sunMapUnit.CoronaInfos[c].Radius), 1f, dashedStrokeStyle);

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.OrangeRed, position, radius);
                        break;
                    default:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, radius);
                        break;

                    case SwitchMapUnit switchMapUnit:
                        break;
                    case TotalRefreshPowerUpMapUnit totalRefreshPowerUpMapUnit:
                        break;
                    case UnknownMapUnit unknownMapUnit:
                        break;
                    case WormHoleMapUnit wormHoleMapUnit:
                        break;
                }
            }
        }
        #endregion
    }
}
