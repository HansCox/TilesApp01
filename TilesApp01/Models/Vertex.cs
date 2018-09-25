using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TilesApp01.Models
{
    public class Vertex
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vertex(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}