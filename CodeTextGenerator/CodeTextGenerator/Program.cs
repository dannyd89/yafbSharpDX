using Flattiverse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeTextGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            generateColorCodeText();
        }

        private static void generateColorCodeText()
        {
            StringBuilder colorsBuilder = new StringBuilder();
            StringBuilder brushesBuilder = new StringBuilder();
            string rawNameVar;

            List<string> unitNames = new List<string>();
            foreach (UnitKind unitKind in Enum.GetValues(typeof(UnitKind)))
                if (unitKind != UnitKind.Nebula
                    && unitKind != UnitKind.PixelCluster 
                    && unitKind != UnitKind.PlayerBase
                    && unitKind != UnitKind.PlayerDrone
                    && unitKind != UnitKind.PlayerPlatform
                    && unitKind != UnitKind.PlayerProbe
                    && unitKind != UnitKind.PlayerShip
                    && unitKind != UnitKind.Spawner)
                    unitNames.Add(unitKind.ToString());

            unitNames.Sort();

            foreach (string rawName in unitNames)
            {
                if (!rawName.StartsWith("AI"))
                    rawNameVar = char.ToLowerInvariant(rawName[0]).ToString() + rawName.Substring(1);
                else
                    rawNameVar = char.ToLowerInvariant(rawName[0]).ToString() + char.ToLowerInvariant(rawName[1]).ToString() + rawName.Substring(2);

                colorsBuilder.Append("public static Color4 ").Append(rawName).AppendLine(" = new Color4(1f, 1f, 1f, 1f);");

                brushesBuilder.Append("#region ").AppendLine(rawName);
                brushesBuilder.Append("private static SolidColorBrush ").Append(rawNameVar).AppendLine(";");
                brushesBuilder.AppendLine();
                brushesBuilder.Append("public static SolidColorBrush ").Append(rawName).Append(" => ").Append(rawNameVar).AppendLine(";");
                brushesBuilder.AppendLine("#endregion");
                brushesBuilder.AppendLine();
            }

            brushesBuilder.AppendLine();

            foreach (string rawName in unitNames)
            {
                if (!rawName.StartsWith("AI"))
                    rawNameVar = char.ToLowerInvariant(rawName[0]).ToString() + rawName.Substring(1);
                else
                    rawNameVar = char.ToLowerInvariant(rawName[0]).ToString() + char.ToLowerInvariant(rawName[1]).ToString() + rawName.Substring(2);

                if (rawNameVar != "switch")
                    brushesBuilder.Append(rawNameVar).Append(" = new SolidColorBrush(renderTarget, AdvancedColors.").Append(rawName).AppendLine(");");
                else
                    brushesBuilder.Append("@").Append(rawNameVar).Append(" = new SolidColorBrush(renderTarget, AdvancedColors.").Append(rawName).AppendLine(");");
            }

            writeText("colors.txt", colorsBuilder.ToString());
            writeText("brushes.txt", brushesBuilder.ToString());
        }

        private static void writeText(string fileName, string content)
        {
            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, fileName), content);
        }
    }
}
