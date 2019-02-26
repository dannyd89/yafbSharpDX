using Flattiverse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeTextGenerator
{
    class Program
    {
        class Node : IComparable<Node>
        {
            public string BaseClass;
            public Node Parent;
            public List<Node> Nodes = new List<Node>();

            public string ClassName;

            public Node(string className, string baseClass)
            {
                ClassName = className;
                BaseClass = baseClass;
            }

            public int CompareTo(Node other)
            {
                return ClassName.CompareTo(other.ClassName);
            }

            public override string ToString()
            {
                return $"{ClassName} ({BaseClass ?? ""})";
            }
        }

        static void Main(string[] args)
        {
            //generateColorCodeText();

            //generateDebugDraw();

            generateSortedMessageListener();

            Console.ReadKey();
        }

        private static void generateSortedMessageListener()
        {
            string connectorPath = Path.Combine(Environment.CurrentDirectory, Path.Combine(@"..\..\..\..\..\..\yafbcore\YAFBCore", "FlattiverseConnector.dll"));
            Assembly flattiverseConnector = Assembly.LoadFrom(connectorPath);

            List<Node> nodes = new List<Node>();

            Node baseNode = null;
            foreach (Type type in flattiverseConnector.GetTypes())
                if (type.IsClass && type.IsPublic && type.Name.Contains("Message"))
                {
                    string className = type.Name;

                    Node current = new Node(className, type.BaseType.Name.Contains("Message") ? type.BaseType.Name : null);
                    nodes.Add(current);

                    if (current.BaseClass == null)
                        baseNode = current;
                }

            List<Node> checkedNodes = new List<Node>();
            Queue<Node> openNodes = new Queue<Node>();
            openNodes.Enqueue(baseNode);

            while (openNodes.Count > 0)
            {
                Node currentNode = openNodes.Dequeue();
                foreach (Node node in nodes)
                    if (node.BaseClass == currentNode.ClassName)
                    {
                        currentNode.Nodes.Add(node);

                        if (!checkedNodes.Contains(node))
                            openNodes.Enqueue(node);
                    }

                checkedNodes.Add(currentNode);
            }

            //sortNodes(baseNode.Nodes);

            StringBuilder stringBuilder = new StringBuilder();

            printNodes(stringBuilder, baseNode.Nodes);

            Console.WriteLine(stringBuilder.ToString());

            writeText("sortedMessages.txt", stringBuilder.ToString());

        }

        private static void printNodes(StringBuilder stringBuilder, List<Node> nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                Node node = nodes[i];


                if (i == 0)
                    stringBuilder.Append("if ");
                else
                    stringBuilder.Append("else if ");

                stringBuilder.Append("(message is ").Append(node.ClassName).AppendLine(")");

                if (node.Nodes.Count > 0)
                    stringBuilder.AppendLine("{");

                stringBuilder.Append("RaiseOnMessage((").Append(node.ClassName).AppendLine(")message);");

                if (node.Nodes.Count > 0)
                {
                    printNodes(stringBuilder, node.Nodes);
                    stringBuilder.AppendLine("}");
                }
            }
        }

        private static void sortNodes(List<Node> nodes)
        {
            nodes.Sort();

            foreach (Node node in nodes)
                if (node.Nodes.Count > 0)
                    sortNodes(node.Nodes);
        }

        private static void generateDebugDraw()
        {
            StringBuilder debugDrawBuilder = new StringBuilder();
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

            float x = 10f, y = 10f, width = 30f, height = 20f, padding = 5f;

            foreach (string rawName in unitNames)
            {
                if (!rawName.StartsWith("AI"))
                    rawNameVar = char.ToLowerInvariant(rawName[0]).ToString() + rawName.Substring(1);
                else
                    rawNameVar = char.ToLowerInvariant(rawName[0]).ToString() + char.ToLowerInvariant(rawName[1]).ToString() + rawName.Substring(2);

                debugDrawBuilder.Append("renderTarget.FillRectangle(new SharpDX.RectangleF(").Append($"{x}f , {y}f, {width}f, {height}f), ").Append("SolidColorBrushes.").Append(rawName).AppendLine(");");

                debugDrawBuilder.Append("renderTarget.DrawText(\"")
                                .Append(rawName)
                                .Append("\", new SharpDX.DirectWrite.TextFormat(Parent.DirectWriteFactory, \"Helvetica\", 12f), new SharpDX.RectangleF(")
                                .Append($"{x + width + padding}f , {y}f, 200f, {height}f), ")
                                .Append("SolidColorBrushes.").Append(rawName).AppendLine(", DrawTextOptions.NoSnap);");

                debugDrawBuilder.AppendLine();

                y += height + padding;
            }

            writeText("debugDraw.txt", debugDrawBuilder.ToString());
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
