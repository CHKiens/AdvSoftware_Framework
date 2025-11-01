using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Mandatory2DGameFramework
{
    public class ConfigReader
    {
        public static World CreateWorld(string path)
        {
            int maxX = 0;
            int maxY = 0;
            string difficulty = "Normal";

            using (XmlReader reader = XmlReader.Create(path))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "MaxX":
                                maxX = reader.ReadElementContentAsInt();
                                break;
                            case "MaxY":
                                maxY = reader.ReadElementContentAsInt();
                                break;
                            case "Difficulty":
                                difficulty = reader.ReadElementContentAsString();
                                break;
                        }
                    }
                }
            }
            return new World(maxX, maxY, difficulty);
        }
    }
}
