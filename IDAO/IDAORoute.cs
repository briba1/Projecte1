using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Projecte1.model;

namespace Projecte1.IDAO
{
    public interface IDAORoute
    {
        public ObservableCollection<Route> GetRoutes(bool valid, int user);

        public Route GetRoute(int id);

        public bool UpdateRoute(Route r);

        public bool InsertRoute(Route r);

        public bool DeleteRoute(int id);

    }
}