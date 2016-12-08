using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chess
{
    public class Coordinate
    {
        public int collumn;
        public int line;

        public Coordinate(int line, int collumn)
        {
            this.collumn = collumn;
            this.line = line;
        }

    }
}