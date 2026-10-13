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

        public ObservableCollection<Stop> GetRoutes(int route)
        {
            throw new NotImplementedException();
        }

        public Stop GetStop(int route, int order)
        {
            throw new NotImplementedException();
        }

        public int GetStopAmount(string id_route)
        {
            throw new NotImplementedException();
        }

        public bool InsertStop(Route r)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStop(Route r)
        {
            throw new NotImplementedException();
        }
    }
}
