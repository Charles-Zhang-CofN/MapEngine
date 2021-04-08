using MapContract;
using System;
using System.Collections.Generic;
using System.IO;

namespace MapEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                var template = new Map() 
                {
                    Visual = new MapVisual()
                    { 
                        Background = new Color() { R = byte.MaxValue, G = byte.MaxValue, B = byte.MaxValue, O = byte.MaxValue }
                    },
                    Layers = new List<DrawLayer>()
                    {
                        new DrawLayer() 
                        {
                            Points = new List<Point>() { new Point() { Name = "Center Building", Tags = new string[] { "Test" }, X = 0, Y = 0} },
                            Shapes = new List<Shape>() { new Shape() { Name = "Wall", Tags = new string[] { "Test" }, Points = new Point[] { new Point(1,1), new Point(2,1), new Point(2,2), new Point(1,2) } } },
                            Paths = new List<MapContract.Path>() { new MapContract.Path() { Name = "Wall Street", Tags = new string[] { "Test" }, Points = new Point[] { new Point(-1, -1), new Point(2, -1), new Point(2, -2), new Point(3, -2) } } }
                        }
                    }
                };
                string yaml = new YamlDotNet.Serialization.Serializer().Serialize(template);
                File.WriteAllText("template.map", yaml);
                return;
            }
            else if (args.Length < 2)
                return;

            string inputPath = args[0];
            string outputPath = args[1];
            if(File.Exists(inputPath))
            {
                string yaml = File.ReadAllText(inputPath);
                var map = new YamlDotNet.Serialization.Deserializer().Deserialize<Map>(yaml);
                DrawMap(map);
            }
        }

        private static void DrawMap(Map map)
        {
            
        }
    }
}
