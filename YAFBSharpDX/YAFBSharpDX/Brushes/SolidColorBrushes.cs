using YAFBSharpDX.Colors;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAFBSharpDX.Brushes
{
    public static class SolidColorBrushes
    {
        #region No Transparency

        #region White
        private static SolidColorBrush white;

        public static SolidColorBrush White => white;
        #endregion

        #region Black
        private static SolidColorBrush black;

        public static SolidColorBrush Black => black;
        #endregion

        #region Red
        private static SolidColorBrush red;

        public static SolidColorBrush Red => red;
        #endregion

        #region Green
        private static SolidColorBrush green;

        public static SolidColorBrush Green => green;
        #endregion

        #region Blue
        private static SolidColorBrush blue;

        public static SolidColorBrush Blue => blue;
        #endregion

        #region Yellow
        private static SolidColorBrush yellow;

        public static SolidColorBrush Yellow
        {
            get
            {
                return SolidColorBrushes.yellow;
            }
        }
        #endregion

        #region Lightgray
        private static SolidColorBrush lightGray;

        public static SolidColorBrush LightGray
        {
            get
            {
                return SolidColorBrushes.lightGray;
            }
        }
        #endregion

        #region BlueViolet
        private static SolidColorBrush blueViolet;

        public static SolidColorBrush BlueViolet
        {
            get
            {
                return blueViolet;
            }
        }
        #endregion

        #region Violet
        private static SolidColorBrush violet;

        public static SolidColorBrush Violet
        {
            get
            {
                return violet;
            }
        }
        #endregion

        #region LightSeaGreen
        private static SolidColorBrush lightSeaGreen;

        public static SolidColorBrush LightSeaGreen
        {
            get
            {
                return SolidColorBrushes.lightSeaGreen;
            }
        }
        #endregion

        #region DarkGreen
        private static SolidColorBrush darkGreen;

        public static SolidColorBrush DarkGreen
        {
            get
            {
                return SolidColorBrushes.darkGreen;
            }
        }
        #endregion

        #region OrangeRed
        private static SolidColorBrush orangeRed;

        public static SolidColorBrush OrangeRed
        {
            get
            {
                return SolidColorBrushes.orangeRed;
            }
        }
        #endregion

        #region LightYellow
        private static SolidColorBrush lightYellow;

        public static SolidColorBrush LightYellow
        {
            get
            {
                return SolidColorBrushes.lightYellow;
            }
        }
        #endregion

        #region DarkRed
        private static SolidColorBrush darkRed;

        public static SolidColorBrush DarkRed
        {
            get
            {
                return SolidColorBrushes.darkRed;
            }
        }
        #endregion

        #region LightBlue
        private static SolidColorBrush lightBlue;

        public static SolidColorBrush LightBlue
        {
            get
            {
                return SolidColorBrushes.lightBlue;
            }
        }
        #endregion

        #region CadetBlue
        private static SolidColorBrush cadetBlue;

        public static SolidColorBrush CadetBlue
        {
            get
            {
                return SolidColorBrushes.cadetBlue;
            }
        }
        #endregion

        #region LightGoldenrodYellow
        private static SolidColorBrush lightGoldenrodYellow;

        public static SolidColorBrush LightGoldenrodYellow
        {
            get
            {
                return SolidColorBrushes.lightGoldenrodYellow;
            }
        }
        #endregion

        #region IndianRed
        private static SolidColorBrush indianRed;

        public static SolidColorBrush IndianRed
        {
            get
            {
                return SolidColorBrushes.indianRed;
            }
        }
        #endregion

        #region DarkOliveGreen
        private static SolidColorBrush darkOliveGreen;

        public static SolidColorBrush DarkOliveGreen
        {
            get
            {
                return SolidColorBrushes.darkOliveGreen;
            }
        }
        #endregion

        #region GreenYellow
        private static SolidColorBrush greenYellow;

        public static SolidColorBrush GreenYellow
        {
            get
            {
                return SolidColorBrushes.greenYellow;
            }
        }
        #endregion

        #region RosyBrown
        private static SolidColorBrush rosyBrown;

        public static SolidColorBrush RosyBrown
        {
            get
            {
                return SolidColorBrushes.rosyBrown;
            }
        }
        #endregion

        #region LimeGreen
        private static SolidColorBrush limeGreen;

        public static SolidColorBrush LimeGreen
        {
            get
            {
                return SolidColorBrushes.limeGreen;
            }
        }
        #endregion

        #region LimeGreen
        private static SolidColorBrush lightPink;

        public static SolidColorBrush LightPink
        {
            get
            {
                return SolidColorBrushes.lightPink;
            }
        }
        #endregion

        #endregion

        #region Halftransparency

        #region DarkGrayHalfTransparent
        private static SolidColorBrush blackHalfTransparent;

        public static SolidColorBrush BlackHalfTransparent
        {
            get
            {
                return SolidColorBrushes.blackHalfTransparent;
            }
        }
        #endregion

        #region DarkGrayHalfTransparent
        private static SolidColorBrush darkGrayHalfTransparent;

        public static SolidColorBrush DarkGrayHalfTransparent
        {
            get
            {
                return SolidColorBrushes.darkGrayHalfTransparent;
            }
        }
        #endregion

        #endregion

        #region Unit specific brushes
        #region AIBase
        private static SolidColorBrush aiBase;

        public static SolidColorBrush AIBase => aiBase;
        #endregion

        #region AIDrone
        private static SolidColorBrush aiDrone;

        public static SolidColorBrush AIDrone => aiDrone;
        #endregion

        #region AIPlatform
        private static SolidColorBrush aiPlatform;

        public static SolidColorBrush AIPlatform => aiPlatform;
        #endregion

        #region AIProbe
        private static SolidColorBrush aiProbe;

        public static SolidColorBrush AIProbe => aiProbe;
        #endregion

        #region AIShip
        private static SolidColorBrush aiShip;

        public static SolidColorBrush AIShip => aiShip;
        #endregion

        #region Asteroid
        private static SolidColorBrush asteroid;

        public static SolidColorBrush Asteroid => asteroid;
        #endregion

        #region BlackHole
        private static SolidColorBrush blackHole;

        public static SolidColorBrush BlackHole => blackHole;
        #endregion

        #region Buoy
        private static SolidColorBrush buoy;

        public static SolidColorBrush Buoy => buoy;
        #endregion

        #region CloakPowerUp
        private static SolidColorBrush cloakPowerUp;

        public static SolidColorBrush CloakPowerUp => cloakPowerUp;
        #endregion

        #region DoubleDamagePowerUp
        private static SolidColorBrush doubleDamagePowerUp;

        public static SolidColorBrush DoubleDamagePowerUp => doubleDamagePowerUp;
        #endregion

        #region EnergyPowerUp
        private static SolidColorBrush energyPowerUp;

        public static SolidColorBrush EnergyPowerUp => energyPowerUp;
        #endregion

        #region Explosion
        private static SolidColorBrush explosion;

        public static SolidColorBrush Explosion => explosion;
        #endregion

        #region Gate
        private static SolidColorBrush gate;

        public static SolidColorBrush Gate => gate;
        #endregion

        #region HastePowerUp
        private static SolidColorBrush hastePowerUp;

        public static SolidColorBrush HastePowerUp => hastePowerUp;
        #endregion

        #region HullPowerUp
        private static SolidColorBrush hullPowerUp;

        public static SolidColorBrush HullPowerUp => hullPowerUp;
        #endregion

        #region IonsPowerUp
        private static SolidColorBrush ionsPowerUp;

        public static SolidColorBrush IonsPowerUp => ionsPowerUp;
        #endregion

        #region Meteoroid
        private static SolidColorBrush meteoroid;

        public static SolidColorBrush Meteoroid => meteoroid;
        #endregion

        #region MissionTarget
        private static SolidColorBrush missionTarget;

        public static SolidColorBrush MissionTarget => missionTarget;
        #endregion

        #region Moon
        private static SolidColorBrush moon;

        public static SolidColorBrush Moon => moon;
        #endregion

        #region ParticlesPowerUp
        private static SolidColorBrush particlesPowerUp;

        public static SolidColorBrush ParticlesPowerUp => particlesPowerUp;
        #endregion

        #region Planet
        private static SolidColorBrush planet;

        public static SolidColorBrush Planet => planet;
        #endregion

        #region QuadDamagePowerUp
        private static SolidColorBrush quadDamagePowerUp;

        public static SolidColorBrush QuadDamagePowerUp => quadDamagePowerUp;
        #endregion

        #region ShieldPowerUp
        private static SolidColorBrush shieldPowerUp;

        public static SolidColorBrush ShieldPowerUp => shieldPowerUp;
        #endregion

        #region Shot
        private static SolidColorBrush shot;

        public static SolidColorBrush Shot => shot;
        #endregion

        #region ShotProductionPowerUp
        private static SolidColorBrush shotProductionPowerUp;

        public static SolidColorBrush ShotProductionPowerUp => shotProductionPowerUp;
        #endregion

        #region SpaceJellyFish
        private static SolidColorBrush spaceJellyFish;

        public static SolidColorBrush SpaceJellyFish => spaceJellyFish;
        #endregion

        #region SpaceJellyFishSlime
        private static SolidColorBrush spaceJellyFishSlime;

        public static SolidColorBrush SpaceJellyFishSlime => spaceJellyFishSlime;
        #endregion

        #region Storm
        private static SolidColorBrush storm;

        public static SolidColorBrush Storm => storm;
        #endregion

        #region StormCommencingWhirl
        private static SolidColorBrush stormCommencingWhirl;

        public static SolidColorBrush StormCommencingWhirl => stormCommencingWhirl;
        #endregion

        #region StormWhirl
        private static SolidColorBrush stormWhirl;

        public static SolidColorBrush StormWhirl => stormWhirl;
        #endregion

        #region Sun
        private static SolidColorBrush sun;

        public static SolidColorBrush Sun => sun;
        #endregion

        #region Switch
        private static SolidColorBrush @switch;

        public static SolidColorBrush Switch => @switch;
        #endregion

        #region TotalRefreshPowerUp
        private static SolidColorBrush totalRefreshPowerUp;

        public static SolidColorBrush TotalRefreshPowerUp => totalRefreshPowerUp;
        #endregion

        #region Unknown
        private static SolidColorBrush unknown;

        public static SolidColorBrush Unknown => unknown;
        #endregion

        #region WormHole
        private static SolidColorBrush wormHole;

        public static SolidColorBrush WormHole => wormHole;
        #endregion
        #endregion

        /// <summary>
        /// Dynamic nebula brushes
        /// </summary>
        public static Dictionary<string, SolidColorBrush> NebulaColors;
        /// <summary>
        /// Dynamic team brushes
        /// </summary>
        public static Dictionary<string, SolidColorBrush> TeamColors;

        /// <summary>
        /// Inits all colors
        /// </summary>
        /// <param name="renderTarget"></param>
        public static void Init(WindowRenderTarget renderTarget)
        {
            // Non transparent brushes
            white = new SolidColorBrush(renderTarget, AdvancedColors.White);
            black = new SolidColorBrush(renderTarget, AdvancedColors.Black);

            red = new SolidColorBrush(renderTarget, AdvancedColors.Red);
            green = new SolidColorBrush(renderTarget, AdvancedColors.Green);
            blue = new SolidColorBrush(renderTarget, AdvancedColors.Blue);

            yellow = new SolidColorBrush(renderTarget, AdvancedColors.Yellow);
            lightGray = new SolidColorBrush(renderTarget, AdvancedColors.LightGray);
            blueViolet = new SolidColorBrush(renderTarget, AdvancedColors.BlueViolet);
            violet = new SolidColorBrush(renderTarget, AdvancedColors.Violet);
            lightSeaGreen = new SolidColorBrush(renderTarget, AdvancedColors.LightSeaGreen);
            darkGreen = new SolidColorBrush(renderTarget, AdvancedColors.DarkGreen);
            orangeRed = new SolidColorBrush(renderTarget, AdvancedColors.OrangeRed);
            lightYellow = new SolidColorBrush(renderTarget, AdvancedColors.LightYellow);
            darkRed = new SolidColorBrush(renderTarget, AdvancedColors.DarkRed);
            lightBlue = new SolidColorBrush(renderTarget, AdvancedColors.LightBlue);
            cadetBlue = new SolidColorBrush(renderTarget, AdvancedColors.CadetBlue);
            lightGoldenrodYellow = new SolidColorBrush(renderTarget, AdvancedColors.LightGoldenrodYellow);
            indianRed = new SolidColorBrush(renderTarget, AdvancedColors.IndianRed);
            darkOliveGreen = new SolidColorBrush(renderTarget, AdvancedColors.DarkOliveGreen);
            greenYellow = new SolidColorBrush(renderTarget, AdvancedColors.GreenYellow);
            rosyBrown = new SolidColorBrush(renderTarget, AdvancedColors.RosyBrown);
            limeGreen = new SolidColorBrush(renderTarget, AdvancedColors.LimeGreen);
            lightPink = new SolidColorBrush(renderTarget, AdvancedColors.LightPink);

            // Half transparent brushes
            blackHalfTransparent = new SolidColorBrush(renderTarget, AdvancedColors.BlackHalfTransparent);
            darkGrayHalfTransparent = new SolidColorBrush(renderTarget, AdvancedColors.DarkGrayHalfTransparent);

            aiBase = new SolidColorBrush(renderTarget, AdvancedColors.AIBase);
            aiDrone = new SolidColorBrush(renderTarget, AdvancedColors.AIDrone);
            aiPlatform = new SolidColorBrush(renderTarget, AdvancedColors.AIPlatform);
            aiProbe = new SolidColorBrush(renderTarget, AdvancedColors.AIProbe);
            aiShip = new SolidColorBrush(renderTarget, AdvancedColors.AIShip);
            asteroid = new SolidColorBrush(renderTarget, AdvancedColors.Asteroid);
            blackHole = new SolidColorBrush(renderTarget, AdvancedColors.BlackHole);
            buoy = new SolidColorBrush(renderTarget, AdvancedColors.Buoy);
            cloakPowerUp = new SolidColorBrush(renderTarget, AdvancedColors.CloakPowerUp);
            doubleDamagePowerUp = new SolidColorBrush(renderTarget, AdvancedColors.DoubleDamagePowerUp);
            energyPowerUp = new SolidColorBrush(renderTarget, AdvancedColors.EnergyPowerUp);
            explosion = new SolidColorBrush(renderTarget, AdvancedColors.Explosion);
            gate = new SolidColorBrush(renderTarget, AdvancedColors.Gate);
            hastePowerUp = new SolidColorBrush(renderTarget, AdvancedColors.HastePowerUp);
            hullPowerUp = new SolidColorBrush(renderTarget, AdvancedColors.HullPowerUp);
            ionsPowerUp = new SolidColorBrush(renderTarget, AdvancedColors.IonsPowerUp);
            meteoroid = new SolidColorBrush(renderTarget, AdvancedColors.Meteoroid);
            missionTarget = new SolidColorBrush(renderTarget, AdvancedColors.MissionTarget);
            moon = new SolidColorBrush(renderTarget, AdvancedColors.Moon);
            particlesPowerUp = new SolidColorBrush(renderTarget, AdvancedColors.ParticlesPowerUp);

            planet = new SolidColorBrush(renderTarget, AdvancedColors.Planet);
            quadDamagePowerUp = new SolidColorBrush(renderTarget, AdvancedColors.QuadDamagePowerUp);
            shieldPowerUp = new SolidColorBrush(renderTarget, AdvancedColors.ShieldPowerUp);
            shot = new SolidColorBrush(renderTarget, AdvancedColors.Shot);
            shotProductionPowerUp = new SolidColorBrush(renderTarget, AdvancedColors.ShotProductionPowerUp);
            spaceJellyFish = new SolidColorBrush(renderTarget, AdvancedColors.SpaceJellyFish);
            spaceJellyFishSlime = new SolidColorBrush(renderTarget, AdvancedColors.SpaceJellyFishSlime);
            storm = new SolidColorBrush(renderTarget, AdvancedColors.Storm);
            stormCommencingWhirl = new SolidColorBrush(renderTarget, AdvancedColors.StormCommencingWhirl);
            stormWhirl = new SolidColorBrush(renderTarget, AdvancedColors.StormWhirl);
            sun = new SolidColorBrush(renderTarget, AdvancedColors.Sun);
            @switch = new SolidColorBrush(renderTarget, AdvancedColors.Switch);
            totalRefreshPowerUp = new SolidColorBrush(renderTarget, AdvancedColors.TotalRefreshPowerUp);
            unknown = new SolidColorBrush(renderTarget, AdvancedColors.Unknown);
            wormHole = new SolidColorBrush(renderTarget, AdvancedColors.WormHole);

            NebulaColors = new Dictionary<string, SolidColorBrush>();
            TeamColors = new Dictionary<string, SolidColorBrush>();
        }
    }
}
