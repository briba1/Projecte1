using System;
using System.Collections.Generic;
using System.Text;

namespace Projecte1.model
{
    public class Stop
    {
        private int route;
        private int order;
        private int time;
        private int station;

        public Stop(int route, int order, int time, int station)
        {
            this.Route = route;
            this.Order = order;
            this.Time = time;
            this.Station = station;
        }

        public int Route { get => route; set => route = value; }
        public int Order { get => order; set => order = value; }
        public int Time { get => time; set => time = value; }
        public int Station { get => station; set => station = value; }
    }
}