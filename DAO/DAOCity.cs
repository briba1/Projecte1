using Projecte1.IDAO;
using Projecte1.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Projecte1.DAO
{
    internal class DAOCity : IDAOCity
    {
        public bool DeleteCity(int id)
        {
            throw new NotImplementedException();
        }

        public City GetCity(int id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<City> GetCities(int id, string name, int offset = 0, int rows_per_page = -1)
        {
            throw new NotImplementedException();
        }

        public bool InsertCity(City c)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCity(City c)
        {
            throw new NotImplementedException();
        }
    }
}
