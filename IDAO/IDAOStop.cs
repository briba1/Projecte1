using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Projecte1.model;

namespace Projecte1.IDAO
{
    public interface IDAOStop
    {
        public ObservableCollection<Stop> GetStops(int route, int time, int station, int offset = 0, int rows_per_page = -1);

        public Stop GetStop(int route, int order);

        public bool UpdateStop(Stop s);

        public bool InsertStop(Stop s);

        public bool DeleteStop(int route, int order);
    }
}