using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class POINT
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public POINT(string Name, int X, int Y)
        {
            this.Name = Name;
            this.X = X;
            this.Y = Y;
        }
        public POINT(POINT a)
        {
            this.Name = a.Name;
            this.X = a.X;
            this.Y = a.Y;
        }
    }
}
