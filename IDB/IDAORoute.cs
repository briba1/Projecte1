using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Projecte1.model;

namespace Projecte1.IDAO
{
    public interface IDAORoute
    {
        public ObservableCollection<Route> GetRoutes(bool? validat, int user, int offset = 0, int rows_per_page = -1);

        public Route? GetRoute(int id);

        public bool InsertRoute(Route r);

        public bool UpdateRoute(Route r);

        public bool DeleteRoute(int id);
    }
}
