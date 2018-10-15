using SharpDX;

namespace YAFBSharpDX.Colors
{
    public static class AdvancedColors
    {
        #region no transparency
        /// <summary>
        /// 
        /// </summary>
        public static Color4 DarkGray = new Color4(SimpleColors.DarkGray, 1.0f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 Red = new Color4(SimpleColors.Red, 1.0f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 Green = new Color4(SimpleColors.Green, 1.0f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 Blue = new Color4(SimpleColors.Blue, 1.0f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 Black = new Color4(SimpleColors.Black, 1.0f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 White = new Color4(SimpleColors.White, 1.0f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 Yellow = new Color4(SimpleColors.Yellow, 1.0f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 LightGray = new Color4(SimpleColors.LightGray, 1.0f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 Violet = new Color4(SimpleColors.Violet, 1f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 BlueViolet = new Color4(SimpleColors.BlueViolet, 1f);
        
        /// <summary>
        /// 
        /// </summary>
        public static Color4 LightSeaGreen = new Color4(SimpleColors.LightSeaGreen, 1f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 DarkGreen = new Color4(SimpleColors.DarkGreen, 1f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 OrangeRed = new Color4(SimpleColors.OrangeRed, 1f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 LightYellow = new Color4(SimpleColors.LightYellow, 1f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 DarkRed = new Color4(SimpleColors.DarkRed, 1f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 LightBlue = new Color4(SimpleColors.LightBlue, 1f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 CadetBlue = new Color4(SimpleColors.CadetBlue, 1f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 LightGoldenrodYellow = new Color4(SimpleColors.LightGoldenrodYellow, 1f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 IndianRed = new Color4(SimpleColors.IndianRed, 1f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 DarkOliveGreen = new Color4(SimpleColors.DarkOliveGreen, 1f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 GreenYellow = new Color4(SimpleColors.GreenYellow, 1f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 RosyBrown = new Color4(SimpleColors.RosyBrown, 1f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 LimeGreen = new Color4(SimpleColors.LimeGreen, 1f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 LightPink = new Color4(SimpleColors.LightPink, 1f);
        #endregion

        #region Halftransparency

        /// <summary>
        /// 
        /// </summary>
        public static Color4 BlackHalfTransparent = new Color4(SimpleColors.Black, 0.5f);

        /// <summary>
        /// 
        /// </summary>
        public static Color4 DarkGrayHalfTransparent = new Color4(SimpleColors.DarkGray, 0.5f);

        #endregion

        #region Unit specific colors
        public static Color4 AIBase = new Color4(1f, 1f, 1f, 1f);
        public static Color4 AIDrone = new Color4(1f, 1f, 1f, 1f);
        public static Color4 AIPlatform = new Color4(1f, 1f, 1f, 1f);
        public static Color4 AIProbe = new Color4(1f, 1f, 1f, 1f);
        public static Color4 AIShip = new Color4(1f, 1f, 1f, 1f);
        public static Color4 Asteroid = new Color4(1f, 1f, 1f, 1f);
        public static Color4 BlackHole = new Color4(1f, 1f, 1f, 1f);
        public static Color4 Buoy = new Color4(1f, 1f, 1f, 1f);
        public static Color4 CloakPowerUp = new Color4(1f, 1f, 1f, 1f);
        public static Color4 DoubleDamagePowerUp = new Color4(1f, 1f, 1f, 1f);
        public static Color4 EnergyPowerUp = new Color4(1f, 1f, 1f, 1f);
        public static Color4 Explosion = new Color4(1f, 1f, 1f, 1f);
        public static Color4 Gate = new Color4(1f, 1f, 1f, 1f);
        public static Color4 HastePowerUp = new Color4(1f, 1f, 1f, 1f);
        public static Color4 HullPowerUp = new Color4(1f, 1f, 1f, 1f);
        public static Color4 IonsPowerUp = new Color4(1f, 1f, 1f, 1f);
        public static Color4 Meteoroid = new Color4(1f, 1f, 1f, 1f);
        public static Color4 MissionTarget = new Color4(1f, 1f, 1f, 1f);
        public static Color4 Moon = new Color4(1f, 1f, 1f, 1f);
        public static Color4 ParticlesPowerUp = new Color4(1f, 1f, 1f, 1f);
        public static Color4 Pixel = new Color4(1f, 1f, 1f, 1f);
        public static Color4 Planet = new Color4(1f, 1f, 1f, 1f);
        public static Color4 QuadDamagePowerUp = new Color4(1f, 1f, 1f, 1f);
        public static Color4 ShieldPowerUp = new Color4(1f, 1f, 1f, 1f);
        public static Color4 Shot = new Color4(1f, 1f, 1f, 1f);
        public static Color4 ShotProductionPowerUp = new Color4(1f, 1f, 1f, 1f);
        public static Color4 SpaceJellyFish = new Color4(1f, 1f, 1f, 1f);
        public static Color4 SpaceJellyFishSlime = new Color4(1f, 1f, 1f, 1f);
        public static Color4 Storm = new Color4(1f, 1f, 1f, 1f);
        public static Color4 StormCommencingWhirl = new Color4(1f, 1f, 1f, 1f);
        public static Color4 StormWhirl = new Color4(1f, 1f, 1f, 1f);
        public static Color4 Sun = new Color4(1f, 1f, 1f, 1f);
        public static Color4 Switch = new Color4(1f, 1f, 1f, 1f);
        public static Color4 TotalRefreshPowerUp = new Color4(1f, 1f, 1f, 1f);
        public static Color4 Unknown = new Color4(1f, 1f, 1f, 1f);
        public static Color4 WormHole = new Color4(1f, 1f, 1f, 1f);
        #endregion
    }
}
