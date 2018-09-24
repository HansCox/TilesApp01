using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Drawing;
using TilesApp01.Models;

namespace TilesApp01.Controllers
{
    public class TilesController : ApiController
    {
        public Triangle Get(char row, int col)
        {
            Vertices vs = computeVertices(row, col);
            return new Triangle
            {
                Row = row,
                Column = col,
                Corners = vs
            };
        }

        public Triangle Get(Point v1, Point v2, Point v3)
        {
            Vertices vs = new Vertices();
            vs.v1 = v1;
            vs.v2 = v2;
            vs.v3 = v3;
            char row = computeRow(vs);
            int col = computeCol(vs);
            return new Triangle
            {
                Row = row,
                Column = col,
                Corners = vs
            };
        }

        private Vertices computeVertices(char row, int col)
        {
            Vertices vs = new Vertices();
            if (row >= Triangle.ROW_START && row <= Triangle.ROW_END 
                && col >= Triangle.COL_START && col <= Triangle.COL_END
                )
            {
                int sideLen = Triangle.SIDE_LENGTH;
                int rownum = row - Triangle.ROW_START;

                //When constructing a triangle, v1 is upper-left vertex
                if (col % 2 == 1) //Odd columns are lower-left of square
                {
                    vs.v1 = new Point(rownum * sideLen, (col - 1) * sideLen);
                    vs.v2 = new Point(vs.v1.X, vs.v1.Y + sideLen);
                    vs.v3 = new Point(vs.v1.X + sideLen, vs.v1.Y + sideLen);

                }
                else //Even columns are upper right of square
                {
                    vs.v1 = new Point(rownum * sideLen, (col - 2) * sideLen);
                    vs.v2 = new Point(vs.v1.X + sideLen, vs.v1.Y);
                    vs.v3 = new Point(vs.v1.X + sideLen, vs.v1.Y + sideLen);
                }
            }
            return vs;
        }

        private char computeRow(Vertices vs)
        {
            char row = '-';
            if (isTriangleOnGrid(vs))
            {
                //Find the avg y val and find the row.
                int centerY = vs.v1.Y + vs.v2.Y + vs.v3.Y;
                centerY = centerY / 3; //Truncation may occur.
                char rowtop = (char) (centerY / Triangle.SIDE_LENGTH);
                row = (char) (Triangle.ROW_START + rowtop);
            }
            return row;
        }

        private int computeCol(Vertices vs)
        {
            int col = -1;
            if (isTriangleOnGrid(vs))
            {
                //An even column number has two vertices on the right and one on the left.
                //An odd column number has one vertex on the right and two on the left.
                //Take the average of the X values. 
                //If it is in the right half of a grid square, it is an even column.
                //If it is in the left half of a grid square, it is an odd column.
                int centerX = vs.v1.X + vs.v2.X + vs.v3.X;
                centerX = centerX / 3; //Truncation may occur.
                int colSquare = centerX / Triangle.SIDE_LENGTH);
                int squareCenterX = (colSquare * Triangle.SIDE_LENGTH) + (Triangle.SIDE_LENGTH / 2);
                if (centerX < squareCenterX)
                {
                    //odd column
                    col = colSquare;
                }
                else
                {
                    //even column
                    col = colSquare + 1;
                }                
            }
            return col;
        }

        private Boolean isTriangleOnGrid(Vertices vs)
        {
            Boolean isOnGrid = false;
            int centerY = vs.v1.Y + vs.v2.Y + vs.v3.Y;
            centerY = centerY / 3; //Truncation may occur.
            int centerX = vs.v1.X + vs.v2.X + vs.v3.X;
            centerX = centerX / 3; //Truncation may occur.
            if(centerY > 0 && centerY < (Triangle.ROW_END - Triangle.ROW_START) * Triangle.SIDE_LENGTH
                && centerX > 0 && centerX < ((Triangle.COL_END / 2 ) * Triangle.SIDE_LENGTH)
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
