using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Projecte1.model;

namespace Projecte1.IDAO
{
    public interface IDAOCity
    {
        public ObservableCollection<City> GetCities(int id, string name, int offset = 0, int rows_per_page = -1);

        public City? GetCity(int id);

        public City? GetCityByName(string name);
    }
}
