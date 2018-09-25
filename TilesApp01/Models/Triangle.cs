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

        public String Row { get; set; }

        public int Column { get; set; }

        public Vertices Corners { get; set; } //When constructing a triangle, v1 is upper-left vertex

        public static Triangle GetTriangle(Triangle t)
        {
            Triangle triangle = null;
           
            if (t != null)
            {
                char[] rowArray = t.Row.ToCharArray();
                char row = '-';
                if (rowArray.Length > 0)
                {
                    row = rowArray[0];
                }
                if (t.Corners == null || t.Corners.V1 == null)
                {                    
                    Vertices vs = ComputeVertices(t.Row, t.Column);
                    triangle = new Triangle
                    {
                        Row = t.Row,
                        Column = t.Column,
                        Corners = vs
                    };
                }
                else if (t.Column < Triangle.COL_START || t.Column > Triangle.COL_END
                    || row > Triangle.ROW_END || row < Triangle.ROW_START)
                {                    
                    Vertices vs = new Vertices
                    {
                        V1 = t.Corners.V1,
                        V2 = t.Corners.V2,
                        V3 = t.Corners.V3
                    };
                    char rowReturn = ComputeRow(vs);
                    char[] rowReturnArray = new char[1];
                    rowReturnArray[0] = rowReturn;
                    int col = ComputeCol(vs);
                    triangle = new Triangle
                    {
                        Row = new string(rowReturnArray),
                        Column = col,
                        Corners = vs
                    };
                }
                else
                {                    
                    triangle = new Triangle
                    {

                    };
                }
            }
            else
            {
                System.Console.Out.Write("Request object was null!");
                triangle = new Triangle { Row = "-", Column = -1, Corners = null };
            }
            return triangle;
        }

        private static Vertices ComputeVertices(string r, int col)
        {
            Vertices vs = new Vertices();
            char [] rowArray = r.ToCharArray();
            char row = '-';
            if(rowArray.Length > 0)
            {
                row = rowArray[0];
            }
            if (row >= Triangle.ROW_START && row <= Triangle.ROW_END
                && col >= Triangle.COL_START && col <= Triangle.COL_END
                )
            {
                int sideLen = Triangle.SIDE_LENGTH;
                int rownum = row - Triangle.ROW_START + 1;

                //When constructing a triangle, v1 is upper-left vertex
                if (col % 2 == 1) //Odd columns are lower-left of square
                {
                    vs.V1 = new Vertex((col - 1) / 2 * sideLen, (rownum - 1) * sideLen);
                    vs.V2 = new Vertex(vs.V1.X, vs.V1.Y + sideLen);
                    vs.V3 = new Vertex(vs.V1.X + sideLen, vs.V1.Y + sideLen);

                }
                else //Even columns are upper right of square
                {
                    vs.V1 = new Vertex((col - 1) / 2 * sideLen, (rownum - 1) * sideLen);
                    vs.V2 = new Vertex(vs.V1.X + sideLen, vs.V1.Y);
                    vs.V3 = new Vertex(vs.V1.X + sideLen, vs.V1.Y + sideLen);
                }
            }
            return vs;
        }

        private static char ComputeRow(Vertices vs)
        {
            char row = '-';
            if (IsTriangleOnGrid(vs))
            {
                //Find the avg y val and find the row.
                int centerY = vs.V1.Y + vs.V2.Y + vs.V3.Y;
                centerY = centerY / 3; //Truncation may occur.
                char rowtop = (char)(centerY / Triangle.SIDE_LENGTH);
                row = (char)(Triangle.ROW_START + rowtop);
            }
            return row;
        }

        private static int ComputeCol(Vertices vs)
        {
            int col = -1;
            if (IsTriangleOnGrid(vs))
            {
                //An even column number has two vertices on the right and one on the left.
                //An odd column number has one vertex on the right and two on the left.
                //Take the average of the X values. 
                //If it is in the right half of a grid square, it is an even column.
                //If it is in the left half of a grid square, it is an odd column.
                int centerX = vs.V1.X + vs.V2.X + vs.V3.X;
                centerX = centerX / 3; //Truncation may occur.
                int colSquare = centerX / Triangle.SIDE_LENGTH + 1;
                int squareCenterX = ((colSquare - 1) * Triangle.SIDE_LENGTH) + (Triangle.SIDE_LENGTH / 2);
                if (centerX < squareCenterX)
                {
                    //odd column
                    col = colSquare * 2 - 1;
                }
                else
                {
                    //even column
                    col = colSquare * 2;
                }
            }
            return col;
        }

        private static Boolean IsTriangleOnGrid(Vertices vs)
        {
            Boolean isOnGrid = false;
            int centerY = vs.V1.Y + vs.V2.Y + vs.V3.Y;
            centerY = centerY / 3; //Truncation may occur.
            int centerX = vs.V1.X + vs.V2.X + vs.V3.X;
            centerX = centerX / 3; //Truncation may occur.
            if (centerY > 0 && centerY < (Triangle.ROW_END - Triangle.ROW_START + 1) * Triangle.SIDE_LENGTH
                && centerX > 0 && centerX < ((Triangle.COL_END / 2) * Triangle.SIDE_LENGTH)
                )
            {
                isOnGrid = true;
            }
            else
            {
                // already false
            }
            return isOnGrid;
        }
    }
}