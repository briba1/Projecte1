using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Projecte1.model;

namespace Projecte1.IDAO
{
    public interface IDAOStation
    {
        public ObservableCollection<Station> GetStations(int city, int offset = 0, int rows_per_page = -1);

        public Station GetStation(int id);

        public bool UpdateStation(Station s);

        public bool InsertStation(Station s);

        public bool DeleteStation(int id);
    }
}