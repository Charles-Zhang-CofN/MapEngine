using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapContract
{
    public enum Effect
    {
        Grid,
        Blueprint,
        Paper
    }
    public class BackgroundEffect
    {
        public Effect Effect;
    }
    public class Grid: BackgroundEffect
    {
        public int Thickness;
        public int Spacing;
        public Color Background;
        public Color Stroke;
    }
}
