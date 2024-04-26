using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;

namespace SCBXML2TXT
{
    internal class Program
    {
        static string startString = @"using System;
using System.Collections.Generic;

namespace Star_Shitizen_Master_Mapping;

public class BindingsList
{
	public static List<MyBinds> Bindings = new()
    {
";
        static string endString = @"    };
}";

        static Dictionary<string, List<MyBinds>> bindings = new();

        private static void AddBinding(string actionmap, string action)
        {
            if (!bindings.ContainsKey(actionmap))
                bindings[actionmap] = new();

            bindings[actionmap].Add(new MyBinds(actionmap, action, "PH_displayCategory", "PH_subcategory", "PH_displayName"));
        }

        private static void WriteBindings(string outFileName = "BindingsList.cs")
        {
            using (FileStream fs = new FileStream(outFileName, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(startString);

                    foreach(var region in bindings)
                    {
                        sw.Write("#region " + region.Key + "\n");
                        foreach (MyBinds bind in region.Value)
                        {
                            sw.Write(bind.ToString());
                        }
                        sw.Write("#endregion\n\n");
                    }

                    sw.Write(endString);
                }
            }
        }

        static void ParseXml(string filePath)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                XmlNodeList actionMaps = xmlDoc.DocumentElement.ChildNodes; // <ActionMaps version="1">
                foreach (XmlNode actionmap in actionMaps) // actionmap can be CustomisationUIHeader, modifiers or actionmap
                {
                    if (actionmap.Name == "actionmap")
                    {
                        string? actionmapName = actionmap.Attributes?["name"]?.Value?.ToString();
                        if (actionmapName == null)
                            continue;

                        foreach (XmlNode action in actionmap.ChildNodes) // action should only contain action
                        {
                            if (action.Name == "action")
                            {
                                string? actionName = action.Attributes?["name"]?.Value?.ToString();
                                if (actionName == null)
                                    continue;

                                AddBinding(actionmapName, actionName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing XML: {ex.Message}");
            }
        }

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("You didn't specify a path dumbass");
                return;
            }

            if (args != null)
            {
                Console.WriteLine("Reading XML from " + args[0]);
                ParseXml(args[0]);

                // when ready output to outputPath
                if (args.Length < 2)
                {
                    Console.WriteLine("Writing to BindingsList.cs");
                    WriteBindings();
                    Console.WriteLine("Done. File saved as BindingsList.cs");
                }
                else
                {
                    Console.WriteLine("Writing to " + args[1]);
                    WriteBindings(args[1]);
                    Console.WriteLine("Done. File saved as " + args[1]);
                }
            }
            else
            {
                Console.WriteLine("You didn't specify a path dumbass");
            }
            
        }
    }
}

