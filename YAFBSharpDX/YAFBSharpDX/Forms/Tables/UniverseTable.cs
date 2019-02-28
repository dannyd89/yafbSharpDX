using YAFBSharpDX.Brushes;
using YAFBSharpDX.Helpers;
using YAFBSharpDX.Screens;
using Flattiverse;
using SharpDX;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAFBSharpDX.Forms.Tables
{
    public class UniverseTable : IDisposable
    {
        private RoundedRectangle roundedRectangle;
        private SharpDX.RectangleF rect;
        private Brush backColorBrush;

        private readonly UniverseGroup universeGroup;
        private readonly Screen screen;

        private Dictionary<string, UniverseTeamTable> teamTables;

        public UniverseTable(Screen screen, UniverseGroup universeGroup, Brush backColorBrush)
        {
            this.universeGroup = universeGroup;
            this.screen = screen;
            this.backColorBrush = backColorBrush;

            teamTables = new Dictionary<string, UniverseTeamTable>();

            UniverseTeamTable teamTable = null;
            foreach (Team team in universeGroup.Teams)
            {
                SolidColorBrush teamColor = SolidColorBrushes.TeamColors[team.Name];

                teamTable = new UniverseTeamTable(screen, team, teamColor);

                teamTables.Add(team.Name, teamTable);
            }
        }

        /// <summary>
        /// Adds a column to all team tables
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="fieldName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void AddColumn(string caption, string fieldName, float width, float height)
        {
            foreach (UniverseTeamTable teamTable in teamTables.Values)
                teamTable.Columns.Add(new Column(screen.Parent.DirectWriteFactory, caption, fieldName, width, height));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="renderTarget"></param>
        public void Draw(float x, float y, float width, float height, RenderTarget renderTarget)
        {
            Dictionary<string, List<Player>> playerList = new Dictionary<string, List<Player>>();
            Dictionary<string, SharpDX.Direct2D1.Bitmap> deletable = new Dictionary<string, Bitmap>(ImageCollections.PlayersSmallAvatars);

            foreach (Player player in universeGroup.Players)
                if (player.IsOnline)
                {
                    if (player.Team != null && !playerList.ContainsKey(player.Team.Name))
                        playerList.Add(player.Team.Name, new List<Player>());

                    if (deletable.ContainsKey(player.Name))
                        deletable.Remove(player.Name);
                    else
                    {
                        Bitmap avatar;

                        System.Drawing.Bitmap tempBitmap = null;

                        if (player.IsActive)
                            tempBitmap = new System.Drawing.Bitmap(new System.IO.MemoryStream(player.SmallAvatarRAW));

                        if (tempBitmap != null)
                        {
                            System.Drawing.Bitmap smallAvatar = new System.Drawing.Bitmap(tempBitmap, new System.Drawing.Size(32, 32));

                            avatar = BitmapConverter.ToSharpDXBitmap(renderTarget, smallAvatar);

                            tempBitmap.Dispose();
                        }
                        else
                            avatar = null;

                        if (player != null)
                            ImageCollections.PlayersSmallAvatars.Add(player.Name, avatar);
                    }

                    if (player != null && player.Team != null)
                        playerList[player.Team.Name].Add(player);
                }

            foreach (KeyValuePair<string, Bitmap> kvp in deletable)
            {
                if (kvp.Value != null)
                    kvp.Value.Dispose();

                ImageCollections.PlayersSmallAvatars.Remove(kvp.Key);
            }

            float currentHeight = height;
            float currentY = y;

            foreach (Team team in universeGroup.Teams)
            {
                List<Player> players;
                if (!playerList.TryGetValue(team.Name, out players))
                    players = new List<Player>();

                UniverseTeamTable teamTable = teamTables[team.Name];

                teamTable.Update(players, x, currentY, width, currentHeight);

                currentHeight -= teamTable.Height;
                currentY += teamTable.Height + 20f;
            }

            roundedRectangle = new RoundedRectangle();
            roundedRectangle.RadiusX = 8;
            roundedRectangle.RadiusY = 8;

            rect = new RectangleF(x, y, width, height);
            roundedRectangle.Rect = rect;

            renderTarget.FillRoundedRectangle(roundedRectangle, backColorBrush);
            renderTarget.DrawRoundedRectangle(roundedRectangle, SolidColorBrushes.White);

            foreach (UniverseTeamTable teamTable in teamTables.Values)
                teamTable.Draw(renderTarget);
        }

        public void Dispose()
        {
            try
            {
                if (teamTables != null)
                    foreach (UniverseTeamTable teamTable in teamTables.Values)
                        teamTable.Dispose();
            }
            catch { }
        }
    }
}
