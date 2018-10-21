using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;
using YAFBCore.Utils.Mathematics;
using YAFBSharpDX.Brushes;

namespace YAFBSharpDX.Screens
{
    class DebugScreen : Screen
    {
        public DebugScreen(GameUI parent) 
            : base(parent)
        { }

        public override ScreenType ScreenType => ScreenType.Debug;

        public override void Render(Size2F windowBounds, WindowRenderTarget renderTarget)
        {
            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 10f, 30f, 20f), SolidColorBrushes.AIBase);
            renderTarget.DrawText("AIBase", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 10f, 200f, 20f), SolidColorBrushes.AIBase, DrawTextOptions.NoSnap);
            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 35f, 30f, 20f), SolidColorBrushes.AIDrone);
            renderTarget.DrawText("AIDrone", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 35f, 200f, 20f), SolidColorBrushes.AIDrone, DrawTextOptions.NoSnap);
            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 60f, 30f, 20f), SolidColorBrushes.AIPlatform);
            renderTarget.DrawText("AIPlatform", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 60f, 200f, 20f), SolidColorBrushes.AIPlatform, DrawTextOptions.NoSnap);
            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 85f, 30f, 20f), SolidColorBrushes.AIProbe);
            renderTarget.DrawText("AIProbe", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 85f, 200f, 20f), SolidColorBrushes.AIProbe, DrawTextOptions.NoSnap);
            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 110f, 30f, 20f), SolidColorBrushes.AIShip);
            renderTarget.DrawText("AIShip", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 110f, 200f, 20f), SolidColorBrushes.AIShip, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 135f, 30f, 20f), SolidColorBrushes.Asteroid);
            renderTarget.DrawText("Asteroid", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 135f, 200f, 20f), SolidColorBrushes.Asteroid, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 160f, 30f, 20f), SolidColorBrushes.BlackHole);
            renderTarget.DrawText("BlackHole", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 160f, 200f, 20f), SolidColorBrushes.BlackHole, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 185f, 30f, 20f), SolidColorBrushes.Buoy);
            renderTarget.DrawText("Buoy", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 185f, 200f, 20f), SolidColorBrushes.Buoy, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 210f, 30f, 20f), SolidColorBrushes.CloakPowerUp);
            renderTarget.DrawText("CloakPowerUp", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 210f, 200f, 20f), SolidColorBrushes.CloakPowerUp, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 235f, 30f, 20f), SolidColorBrushes.DoubleDamagePowerUp);
            renderTarget.DrawText("DoubleDamagePowerUp", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 235f, 200f, 20f), SolidColorBrushes.DoubleDamagePowerUp, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 260f, 30f, 20f), SolidColorBrushes.EnergyPowerUp);
            renderTarget.DrawText("EnergyPowerUp", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 260f, 200f, 20f), SolidColorBrushes.EnergyPowerUp, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 285f, 30f, 20f), SolidColorBrushes.Explosion);
            renderTarget.DrawText("Explosion", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 285f, 200f, 20f), SolidColorBrushes.Explosion, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 310f, 30f, 20f), SolidColorBrushes.Gate);
            renderTarget.DrawText("Gate", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 310f, 200f, 20f), SolidColorBrushes.Gate, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 335f, 30f, 20f), SolidColorBrushes.HastePowerUp);
            renderTarget.DrawText("HastePowerUp", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 335f, 200f, 20f), SolidColorBrushes.HastePowerUp, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 360f, 30f, 20f), SolidColorBrushes.HullPowerUp);
            renderTarget.DrawText("HullPowerUp", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 360f, 200f, 20f), SolidColorBrushes.HullPowerUp, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 385f, 30f, 20f), SolidColorBrushes.IonsPowerUp);
            renderTarget.DrawText("IonsPowerUp", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 385f, 200f, 20f), SolidColorBrushes.IonsPowerUp, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 410f, 30f, 20f), SolidColorBrushes.Meteoroid);
            renderTarget.DrawText("Meteoroid", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 410f, 200f, 20f), SolidColorBrushes.Meteoroid, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 435f, 30f, 20f), SolidColorBrushes.MissionTarget);
            renderTarget.DrawText("MissionTarget", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 435f, 200f, 20f), SolidColorBrushes.MissionTarget, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 460f, 30f, 20f), SolidColorBrushes.Moon);
            renderTarget.DrawText("Moon", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 460f, 200f, 20f), SolidColorBrushes.Moon, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 485f, 30f, 20f), SolidColorBrushes.ParticlesPowerUp);
            renderTarget.DrawText("ParticlesPowerUp", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 485f, 200f, 20f), SolidColorBrushes.ParticlesPowerUp, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 535f, 30f, 20f), SolidColorBrushes.Planet);
            renderTarget.DrawText("Planet", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 535f, 200f, 20f), SolidColorBrushes.Planet, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 560f, 30f, 20f), SolidColorBrushes.QuadDamagePowerUp);
            renderTarget.DrawText("QuadDamagePowerUp", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 560f, 200f, 20f), SolidColorBrushes.QuadDamagePowerUp, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 585f, 30f, 20f), SolidColorBrushes.ShieldPowerUp);
            renderTarget.DrawText("ShieldPowerUp", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 585f, 200f, 20f), SolidColorBrushes.ShieldPowerUp, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 610f, 30f, 20f), SolidColorBrushes.Shot);
            renderTarget.DrawText("Shot", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 610f, 200f, 20f), SolidColorBrushes.Shot, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 635f, 30f, 20f), SolidColorBrushes.ShotProductionPowerUp);
            renderTarget.DrawText("ShotProductionPowerUp", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 635f, 200f, 20f), SolidColorBrushes.ShotProductionPowerUp, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 660f, 30f, 20f), SolidColorBrushes.SpaceJellyFish);
            renderTarget.DrawText("SpaceJellyFish", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 660f, 200f, 20f), SolidColorBrushes.SpaceJellyFish, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 685f, 30f, 20f), SolidColorBrushes.SpaceJellyFishSlime);
            renderTarget.DrawText("SpaceJellyFishSlime", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 685f, 200f, 20f), SolidColorBrushes.SpaceJellyFishSlime, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 710f, 30f, 20f), SolidColorBrushes.Storm);
            renderTarget.DrawText("Storm", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 710f, 200f, 20f), SolidColorBrushes.Storm, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 735f, 30f, 20f), SolidColorBrushes.StormCommencingWhirl);
            renderTarget.DrawText("StormCommencingWhirl", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 735f, 200f, 20f), SolidColorBrushes.StormCommencingWhirl, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 760f, 30f, 20f), SolidColorBrushes.StormWhirl);
            renderTarget.DrawText("StormWhirl", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 760f, 200f, 20f), SolidColorBrushes.StormWhirl, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 785f, 30f, 20f), SolidColorBrushes.Sun);
            renderTarget.DrawText("Sun", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 785f, 200f, 20f), SolidColorBrushes.Sun, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 810f, 30f, 20f), SolidColorBrushes.Switch);
            renderTarget.DrawText("Switch", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 810f, 200f, 20f), SolidColorBrushes.Switch, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 835f, 30f, 20f), SolidColorBrushes.TotalRefreshPowerUp);
            renderTarget.DrawText("TotalRefreshPowerUp", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 835f, 200f, 20f), SolidColorBrushes.TotalRefreshPowerUp, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 860f, 30f, 20f), SolidColorBrushes.Unknown);
            renderTarget.DrawText("Unknown", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 860f, 200f, 20f), SolidColorBrushes.Unknown, DrawTextOptions.NoSnap);

            renderTarget.FillRectangle(new SharpDX.RectangleF(10f, 885f, 30f, 20f), SolidColorBrushes.WormHole);
            renderTarget.DrawText("WormHole", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, "Helvetica", 12f), new SharpDX.RectangleF(45f, 885f, 200f, 20f), SolidColorBrushes.WormHole, DrawTextOptions.NoSnap);

            // Unit specific drawings
            // Sun - Corona different energies -> different colors?
            // BlackHole - GravityWell
        }
    }
}
