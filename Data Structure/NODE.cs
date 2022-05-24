using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class NODE
    {
        public string Name { get; set; } //
        public double Cost { get; set; }
        public double Heuristic { get; set; }

        public Queue<POINT> way = new Queue<POINT>();
        public NODE(string Name, double Cost, double Heuristic)
        {
            this.Name = Name;
            this.Cost = Math.Round(Cost, 3);
            this.Heuristic = Math.Round(Heuristic, 3);
        }
        public NODE(string Name, double Cost, double Heuristic, POINT pOINT, Queue<POINT> OldQueue)
        {
            this.Name = Name;
            this.Cost = Math.Round(Cost, 3);
            this.Heuristic = Math.Round(Heuristic, 3);
            way = OldQueue;
            way.Enqueue(pOINT);
        }
        public NODE(string Name, double Cost, double Heuristic, POINT pOINT)
        {
            this.Name = Name;
            this.Cost = Math.Round(Cost, 3); 
            this.Heuristic = Math.Round(Heuristic, 3);
            way.Enqueue(pOINT);
        }
    }
}
