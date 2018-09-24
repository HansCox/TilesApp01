using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace TilesApp01.Models
{
    public class Triangle
    {
        public const char ROW_START = 'A';
        public const char ROW_END = 'F';
        public const int COL_START = 1;
        public const int COL_END = 12;
        public const int SIDE_LENGTH = 10; //pixels, non-hypotenuse sides

        public char Row { get; set; }

        public int Column { get; set; }

        public Vertices Corners { get; set; } //When constructing a triangle, v1 is upper-left vertex
    }
}