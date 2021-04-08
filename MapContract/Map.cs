using System;
using System.Collections.Generic;

namespace MapContract
{
    /*Specification:
     * - Coordinates always start from lower-left corner unless otherwise specified (e.g. for composites);
     * - Coordinates always follow +X to right, +Y to up convention.
     */


    #region Basic Types
    public enum Origin
    {
        LowerLeft,
        LowerRight,
        UpperLeft,
        UpperRight,
        Centered
    }
    public struct Margin
    {
        public int Top;
        public int Bottom;
        public int Left;
        public int Right;
    }
    public struct Color
    {
        public byte R;
        public byte G;
        public byte B;
        /// <summary>
        /// Opacity; Opacity 0 is usually not drawn (skip drawing)
        /// </summary>
        public byte O;
    }
    public struct Options
    {
        /// <summary>
        /// Whether points should be drawn
        /// </summary>
        public bool ShowVertex;
        /// <summary>
        /// Whether labels should be drawn
        /// </summary>
        public bool ShowLabel;
        /// <summary>
        /// Size or thickness of stroke
        /// </summary>
        public int Thickness;
        /// <summary>
        /// Color for strokes for paths and shapes; 
        /// Points will use FillColor instead
        /// </summary>
        public Color BorderColor;
        public Color FillColor;

        #region Font
        public string Font;
        public int FontSize;
        public Color FontColor;
        #endregion
    }
    #endregion

    #region Basic Data Constructs (Flat Hierachy)
    public class MapData
    {
        public string Name;
        public string[] Tags;
    }
    public class Point : MapData
    {
        public double X;
        public double Y;

        public Point() { }
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
    public class Path : MapData
    {
        public Point[] Points;
    }
    public class Shape : MapData
    {
        public Point[] Points;
    }
    public class Image: MapData
    {
        public double X;
        public double Y;
        public string FilePath;
        public Origin Origin;
    }
    #endregion

    #region Advanced Data Constructs
    public class Composite: MapData
    {
        public double X;
        public double Y;
        public string Path;
        public Origin Origin;
        public bool ImportStyles;
    }
    #endregion

    #region Map Containers
    public class DrawLayer
    {
        public List<Point> Points { get; set; }
        public List<Path> Paths { get; set; }
        public List<Shape> Shapes { get; set; }
        public List<Image> Images { get; set; }
    }
    public class MapVisual
    {
        public Margin Margin { get; set; }
        public Color Background { get; set; }
        /// <summary>
        /// Preset background texture effect, e.g. Paper, grid, Blueprint, etc. This is a short serializable string
        /// </summary>
        public string BackgroundEffect { get; set; }
    }
    public class Map
    {
        /// <summary>
        /// Provides overall visual options
        /// </summary>
        public MapVisual Visual { get; set; }
        /// <summary>
        /// Draw layer is only used to define drawing order; First layer is drawn first
        /// </summary>
        public List<DrawLayer> Layers { get; set; }
        /// <summary>
        /// Draw options is used to apply styles by filtering tags; Only root level options are used
        /// </summary>
        public Dictionary<string, Options> Styles { get; set; }
    }
    #endregion
}