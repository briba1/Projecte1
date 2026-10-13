using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Projecte1.model;

namespace Projecte1.IDAO
{
    public interface IDAOStop
    {
        public int GetStopAmount(string id_route);

        public ObservableCollection<Stop> GetRoutes(int route);

        public Stop GetStop(int route, int order);

        public bool UpdateStop(Route r);

        public bool InsertStop(Route r);

        public bool DeleteStop(int route, int order);
    }
}