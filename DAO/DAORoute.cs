using Projecte1.IDAO;
using Projecte1.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Projecte1.DAO
{
    internal class DAORoute : IDAORoute
    {
        public bool DeleteRoute(int id)
        {
            throw new NotImplementedException();
        }

        public Route GetRoute(int id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Route> GetRoutes(bool validat, int user, int offset = 0, int rows_per_page = -1)
        {
            throw new NotImplementedException();
        }

        public bool InsertRoute(Route r)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRoute(Route r)
        {
            throw new NotImplementedException();
        }
    }
}
