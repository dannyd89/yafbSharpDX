using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YAFBCore.Controllables;
using YAFBCore.Mapping;
using YAFBCore.Mapping.Units;
using YAFBCore.Networking;
using YAFBCore.Pathfinding.Pathfinders;
using YAFBCore.Utils;
using YAFBCore.Utils.Mathematics;
using YAFBSharpDX.Forms.Tables;

namespace YAFBSharpDX.Screens
{
    /// <summary>
    /// 
    /// </summary>
    public class GameScreen : Screen
    {
        private GameUI parent;
        private UniverseSession universeSession;
        private Flattiverse.UniverseGroup universeGroup;
        private MapManager mapManager;
        private ControllablesManager controllablesManager;
        private Ship ship;

        #region Disposable graphic elements
        private StrokeStyle dashedStrokeStyle;

        private SharpDX.DirectWrite.TextFormat missionTargetTextFormat;
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

        private const int SCOREBOARD_PADDING = 30;
        private UniverseTable scoreBoard;
        private bool showScoreBoard;

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
            universeGroup = universeSession.UniverseGroup;
            mapManager = universeSession.MapManager;
            controllablesManager = universeSession.ControllablesManager;

            foreach (Flattiverse.Team team in universeGroup.Teams)
            {
                SolidColorBrush teamColor = new SolidColorBrush(parent.WindowRenderTarget, new SharpDX.Color4(team.Red, team.Green, team.Blue, 1f));

                if (!Brushes.SolidColorBrushes.TeamColors.ContainsKey(team.Name))
                    Brushes.SolidColorBrushes.TeamColors.Add(team.Name, teamColor);
            }

            dashedStrokeStyle = new StrokeStyle(parent.Direct2DFactory, new StrokeStyleProperties() { DashStyle = DashStyle.Dash, DashCap = CapStyle.Flat });
            missionTargetTextFormat = new SharpDX.DirectWrite.TextFormat(parent.DirectWriteFactory, "Arial", SharpDX.DirectWrite.FontWeight.Normal, SharpDX.DirectWrite.FontStyle.Normal, 12f);

#if DEBUG
            universeGroup.Chat("Hello");

            foreach (Flattiverse.Player p in universeGroup.Players)
                p.Chat("Hello?");
#endif

            scoreBoard = new UniverseTable(this, universeGroup, Brushes.SolidColorBrushes.BlackHalfTransparent);

            scoreBoard.AddColumn(" ", "SmallAvatar", 40f, UniverseTeamTable.MAX_COLUMN_HEIGHT);
            scoreBoard.AddColumn("Name", "Name", UniverseTeamTable.MAX_COLUMN_WIDTH, UniverseTeamTable.MAX_COLUMN_HEIGHT);
            scoreBoard.AddColumn("Score", "Score", UniverseTeamTable.MAX_COLUMN_WIDTH, UniverseTeamTable.MAX_COLUMN_HEIGHT);
            scoreBoard.AddColumn("Kills", "Kills", UniverseTeamTable.MAX_COLUMN_WIDTH, UniverseTeamTable.MAX_COLUMN_HEIGHT);
            scoreBoard.AddColumn("Deaths", "Deaths", UniverseTeamTable.MAX_COLUMN_WIDTH, UniverseTeamTable.MAX_COLUMN_HEIGHT);
            scoreBoard.AddColumn("Avg. Commit Time", "AverageCommitTime", 250f, UniverseTeamTable.MAX_COLUMN_HEIGHT);

            ship = controllablesManager.CreateShip("D1RP", "R1P");

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

            Map map = null;
            PlayerShipMapUnit shipUnit = null;
            if (ship != null && ship.IsAlive && mapManager.TryGetPlayerUnit(ship.Universe.Name, ship.Name, out map, out shipUnit))
                sourceRect = getSourceRectangleF(shipUnit.Position.X, shipUnit.Position.Y, scale, windowBounds.Width, windowBounds.Height);
            else
                sourceRect = getSourceRectangleF(viewCenterX, viewCenterY, scale, windowBounds.Width, windowBounds.Height);

            X = new Transformator(sourceRect.Left, sourceRect.Right, 0, windowBounds.Width);
            Y = new Transformator(sourceRect.Top, sourceRect.Bottom, 0, windowBounds.Height);

            // TODO: Default Universe muss gesetzt werden falls ein Schiff nicht am Leben ist
            
            List<MapUnit> unitList;
            if (((map != null && mapManager.TryGetUnits(map, sourceRect, out unitList)) 
                    || mapManager.TryGetUnits(ship.Universe.Name, sourceRect, out unitList)) 
                && unitList.Count > 0)
            {
                drawUnits(renderTarget, unitList);

                // TODO: Draw HUD
                //if (shipUnit != null)
                //{
                //    renderTarget.DrawRectangle(
                //        new SharpDX.Mathematics.Interop.RawRectangleF(
                //            windowBounds.Width / 2f - 50f, 
                //            windowBounds.Height - 20f, 
                //            windowBounds.Width / 2f + 50f, 
                //            windowBounds.Height), 
                //        Brushes.SolidColorBrushes.White);

                //    renderTarget.FillRectangle(
                //        new SharpDX.Mathematics.Interop.RawRectangleF(
                //            windowBounds.Width / 2f - 49f,
                //            windowBounds.Height - 19f,
                //            (windowBounds.Width / 2f - 49f) + 98f * shipUnit.Energy,
                //            windowBounds.Height - 1f),
                //        Brushes.SolidColorBrushes.Violet);
                //}

                // TODO: Temp Klick Position
                if (ship != null && ship.IsAlive)
                {
                    Flattiverse.Vector pos = ship.DesiredPosition;

                    if (pos != null)
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.White, new SharpDX.Vector2(X[pos.X], Y[pos.Y]), 2f);
                }

                if (showScoreBoard)
                    scoreBoard.Draw(SCOREBOARD_PADDING, SCOREBOARD_PADDING, windowBounds.Width - SCOREBOARD_PADDING * 2f, windowBounds.Height - SCOREBOARD_PADDING * 2f, renderTarget);

                //MapPathfinder mapPathfinder = ship.MapPathfinder;

                //if (mapPathfinder != null)
                //{
                //    MapSectionRaster[] rasters = ship.MapPathfinder.Rasters;

                //    for (int i = 0; i < rasters.Length; i++)
                //    {
                //        MapSectionRasterTile[] tiles = rasters[i].Tiles;

                //        for (int t = 0; t < tiles.Length; t++)
                //        {
                //            var tile = tiles[t];

                //            if ((tile.Status & MapSectionRasterTileStatus.Blocked) == MapSectionRasterTileStatus.Blocked)
                //            {
                //                SharpDX.Mathematics.Interop.RawRectangleF rectangleF = new SharpDX.Mathematics.Interop.RawRectangleF(
                //                X[tile.X - rasters[i].TileSize / 2f],
                //                Y[tile.Y - rasters[i].TileSize / 2f],
                //                X[tile.X + rasters[i].TileSize / 2f],
                //                Y[tile.Y + rasters[i].TileSize / 2f]);

                //                renderTarget.DrawRectangle(rectangleF, Brushes.SolidColorBrushes.White);

                //                renderTarget.DrawRectangle(rectangleF, Brushes.SolidColorBrushes.RedHalfTransparent);
                //            }
                //        }
                //    }
                //}
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    showScoreBoard = !showScoreBoard;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (ship != null /*&& X != null && Y != null*/)
                    ship.Queue(new YAFBCore.Controllables.Commands.MoveCommand(X.Rev(e.X), Y.Rev(e.Y)));
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (ship != null /*&& X != null && Y != null*/)
                    ship.Queue(new YAFBCore.Controllables.Commands.ShootCommand(X.Rev(e.X), Y.Rev(e.Y)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void MouseMove(object sender, MouseEventArgs e)
        {
            currentMouseX = e.X;
            currentMouseY = e.Y;

            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                destinationViewCenterX += (e.X - currentMouseX) / scale;
                destinationViewCenterY += (e.Y - currentMouseY) / scale;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void MouseUp(object sender, MouseEventArgs e)
        {
            base.MouseUp(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta == 0)
                return;

            if (e.Delta > 0)
                scale *= 1.2f;
            else
                scale /= 1.2f;

            if (scale < 0.10f)
                scale = 0.10f;
            else if (scale > 15f)
                scale = 15f;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
            dashedStrokeStyle.Dispose();
            missionTargetTextFormat.Dispose();
            scoreBoard.Dispose();

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
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.AIBase, position, radius);

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, X.Prop(mapUnit.Radius - 3f));
                        break;
                    case AIDroneMapUnit aiDroneMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.AIDrone, position, radius);

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, X.Prop(mapUnit.Radius - 3f));
                        break;
                    case AIPlatformMapUnit aiPlatformMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.AIPlatform, position, radius);

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, X.Prop(mapUnit.Radius - 2f));
                        break;
                    case AIProbeMapUnit aiProbeMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.AIProbe, position, radius);

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, X.Prop(mapUnit.Radius - 2f));
                        break;
                    case AIShipMapUnit aiShipMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.AIShip, position, radius);

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, X.Prop(mapUnit.Radius - 2f));
                        break;
                    case AsteroidMapUnit asteroidMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Asteroid, position, radius);
                        break;
                    case BlackHoleMapUnit blackHoleMapUnit:
                        for (int c = 0; c < blackHoleMapUnit.GravityWellInfos.Length; c++)
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.BlueViolet, position, X.Prop(blackHoleMapUnit.GravityWellInfos[c].Radius), 1f, dashedStrokeStyle);

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.BlackHole, position, radius);
                        break;
                    case BuoyMapUnit buoyMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Buoy, position, radius);
                        break;
                    case CloakPowerUpMapUnit cloakPowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.CloakPowerUp, position, radius);
                        break;
                    case DoubleDamagePowerUpMapUnit doubleDamagePowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.DoubleDamagePowerUp, position, radius);
                        break;
                    case EnergyPowerUpMapUnit energyPowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.EnergyPowerUp, position, radius);
                        break;
                    case ExplosionMapUnit explosionMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Explosion, position, radius);
                        break;
                    case GateMapUnit gateMapUnit:
                        if (gateMapUnit.Switched)
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Red, position, radius);
                        else
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Green, position, radius, 1f, dashedStrokeStyle);
                        break;
                    case HastePowerUpMapUnit hastePowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.HastePowerUp, position, radius);
                        break;
                    case HullPowerUpMapUnit hullPowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.HullPowerUp, position, radius);
                        break;
                    case IonsPowerUpMapUnit ionsPowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.IonsPowerUp, position, radius);
                        break;
                    case MeteoroidMapUnit meteoroidMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Meteoroid, position, radius);
                        break;
                    case MissionTargetMapUnit missionTargetMapUnit:

                        SolidColorBrush solidColorBrush;
                        if (missionTargetMapUnit.Team == null || missionTargetMapUnit.Team.Name == "None")
                            solidColorBrush = missionTargetMapUnit.IsHit ? Brushes.SolidColorBrushes.LimeGreen : Brushes.SolidColorBrushes.Red;
                        else
                            solidColorBrush = Brushes.SolidColorBrushes.TeamColors[missionTargetMapUnit.Team.Name];

                        Primitives.Circle.Draw(renderTarget, solidColorBrush, position, radius);
                        Primitives.Circle.Draw(renderTarget, solidColorBrush, position, X.Prop(mapUnit.Radius + 3f));

                        if (missionTargetMapUnit.DominationRadius > 0f)
                        {
                            if (missionTargetMapUnit.Team == null)
                                Primitives.Circle.Draw(renderTarget,
                                            Brushes.SolidColorBrushes.GreenYellow,
                                            position,
                                            X.Prop(missionTargetMapUnit.DominationRadius));
                            else
                                Primitives.Circle.Draw(renderTarget,
                                            Brushes.SolidColorBrushes.TeamColors[missionTargetMapUnit.Team.Name],
                                            position,
                                            X.Prop(missionTargetMapUnit.DominationRadius), X.Prop(4f));

                            if (missionTargetMapUnit.DominatingTeam != null)
                                Primitives.Circle.Draw(renderTarget,
                                            Brushes.SolidColorBrushes.TeamColors[missionTargetMapUnit.DominatingTeam.Name],
                                            position,
                                            X.Prop(missionTargetMapUnit.DominationRadius - 10f),
                                            X.Prop(8f),
                                            dashedStrokeStyle);
                        }
                        else
                        {
                            renderTarget.DrawText(
                                missionTargetMapUnit.Name + " (#" + missionTargetMapUnit.SequenceNumber + ")",
                                missionTargetTextFormat,
                                new SharpDX.Mathematics.Interop.RawRectangleF(position.X, position.Y - X.Prop(20f), position.X + 100f, position.Y + 40f),
                                Brushes.SolidColorBrushes.MissionTarget,
                                DrawTextOptions.NoSnap);

                            foreach (Flattiverse.Vector tempHint in missionTargetMapUnit.Hints)
                            {
                                Flattiverse.Vector hint = tempHint * 20f;

                                renderTarget.DrawLine(
                                    new SharpDX.Mathematics.Interop.RawVector2(position.X, position.Y),
                                    new SharpDX.Mathematics.Interop.RawVector2(position.X + hint.X, position.Y + hint.Y),
                                    Brushes.SolidColorBrushes.White, 2f, dashedStrokeStyle);
                            }
                        }
                        break;
                    case MoonMapUnit moonMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Moon, position, radius);
                        break;
                    case NebulaMapUnit nebulaMapUnit:
                        // TODO: Nebula color handling
                        break;
                    case ParticlesPowerUpMapUnit particlesPowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.ParticlesPowerUp, position, radius);
                        break;
                    case PixelMapUnit pixelMapUnit:
                        // Skip this for now
                        // Unused anyway
                        break;
                    case PlanetMapUnit planetMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Planet, position, radius);
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
                        float penThicknessBars = 2f;

                        using (SharpDX.DirectWrite.TextLayout textLayout = new SharpDX.DirectWrite.TextLayout(parent.DirectWriteFactory, playerShipMapUnit.Name, missionTargetTextFormat, 100f, 20f))
                        {
                            float halfWidth = textLayout.Metrics.Width / 2f;
                            renderTarget.DrawTextLayout(new SharpDX.Mathematics.Interop.RawVector2(position.X - halfWidth, position.Y + radius + Y.Prop(penThicknessBars + 2f)),
                                                        textLayout,
                                                        Brushes.SolidColorBrushes.TeamColors[playerShipMapUnit.TeamName], SharpDX.Direct2D1.DrawTextOptions.NoSnap);
                        }

                        if (playerShipMapUnit.Hull > 0f)
                            Primitives.Arc.Draw(renderTarget,
                                     Brushes.SolidColorBrushes.OrangeRed,
                                     position,
                                     radius - X.Prop(penThicknessBars),
                                     270f,
                                     360f * playerShipMapUnit.Hull,
                                     X.Prop(penThicknessBars));

                        if (playerShipMapUnit.Shield > 0.01f)
                            Primitives.Arc.Draw(renderTarget,
                                     Brushes.SolidColorBrushes.CadetBlue,
                                     position,
                                     radius - X.Prop(penThicknessBars * 3f),
                                     270f,
                                     360f * playerShipMapUnit.Shield,
                                     X.Prop(penThicknessBars));

                        if (playerShipMapUnit.IsOwnShip)
                        {
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.LightBlue, position, radius);

                            #region Health, Energy, Shield

                            if (playerShipMapUnit.Energy > 0f)
                                Primitives.Arc.Draw(renderTarget,
                                         Brushes.SolidColorBrushes.BlueViolet,
                                         position,
                                         radius - X.Prop(penThicknessBars * 2f),
                                         270f,
                                         360f * playerShipMapUnit.Energy,
                                         X.Prop(penThicknessBars));

                            float weaponLoadPercentage = playerShipMapUnit.CurrentShots / playerShipMapUnit.MaxShots;

                            weaponLoadPercentage = weaponLoadPercentage < 0.01f ? 0.0f : weaponLoadPercentage;

                            if (weaponLoadPercentage > 0f)
                                Primitives.Arc.Draw(renderTarget,
                                         Brushes.SolidColorBrushes.LightGoldenrodYellow,
                                         position,
                                         radius + X.Prop(penThicknessBars),
                                         270f,
                                         360f * weaponLoadPercentage,
                                         X.Prop(penThicknessBars));
                            #endregion
                        }
                        else
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.IndianRed, position, radius);
                        break;
                    case QuadDamagePowerUpMapUnit quadDamagePowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.QuadDamagePowerUp, position, radius);
                        break;
                    case ShieldPowerUpMapUnit shieldPowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.ShieldPowerUp, position, radius);
                        break;
                    case ShotMapUnit shotMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Shot, position, radius);
                        break;
                    case ShotProductionPowerUpMapUnit shotProductionPowerUpMapUnit:
                        Primitives.Circle.Fill(renderTarget, Brushes.SolidColorBrushes.ShotProductionPowerUp, position, radius);
                        break;
                    case SpaceJellyFishMapUnit spaceJellyFishMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.SpaceJellyFish, position, radius);
                        break;
                    case SpaceJellyFishSlimeMapUnit spaceJellyFishSlimeMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.SpaceJellyFishSlime, position, radius, 1f, dashedStrokeStyle);
                        break;

                    case StormCommencingWhirlMapUnit stormCommencingWhirlMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.StormCommencingWhirl, position, radius, 1f, dashedStrokeStyle);
                        break;
                    case StormMapUnit stormMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Storm, position, radius);
                        break;
                    case StormWhirlMapUnit stormWhirlMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.StormWhirl, position, radius);
                        break;

                    case SunMapUnit sunMapUnit:
                        for (int c = 0; c < sunMapUnit.CoronaInfos.Length; c++)
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.LightYellow, position, X.Prop(sunMapUnit.CoronaInfos[c].Radius), 1f, dashedStrokeStyle);

                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Sun, position, radius);
                        break;

                    case SwitchMapUnit switchMapUnit:
                        if (switchMapUnit.Switched)
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Red, position, radius);
                        else
                            Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Green, position, radius, 1f, dashedStrokeStyle);
                        break;

                    case TotalRefreshPowerUpMapUnit totalRefreshPowerUpMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.TotalRefreshPowerUp, position, radius);
                        break;
                    case UnknownMapUnit unknownMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.Unknown, position, radius);
                        break;
                    case WormHoleMapUnit wormHoleMapUnit:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.WormHole, position, radius);
                        break;

                    default:
                        Primitives.Circle.Draw(renderTarget, Brushes.SolidColorBrushes.White, position, radius);
                        break;
                }
            }
        }
        #endregion
    }
}
