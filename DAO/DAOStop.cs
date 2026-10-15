using Projecte1.IDAO;
using Projecte1.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Projecte1.DAO
{
    internal class DAOStop : IDAOStop
    {
        public bool DeleteStop(int route, int order)
        {
            throw new NotImplementedException();
        }

        public Stop GetStop(int route, int order)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Stop> GetStops(int route, int time, int station, int offset = 0, int rows_per_page = -1)
        {
            throw new NotImplementedException();
        }

        public bool InsertStop(Stop s)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStop(Stop s)
        {
            throw new NotImplementedException();
        }
    }
}
