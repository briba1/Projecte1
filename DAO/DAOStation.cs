using Projecte1.IDAO;
using Projecte1.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Projecte1.DAO
{
    internal class DAOStation : IDAOStation
    {
        public bool DeleteStation(int id)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Station> GetStations(int city, int offset = 0, int rows_per_page = -1)
        {
            throw new NotImplementedException();
        }

        public bool InsertStation(Station s)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStation(Station s)
        {
            throw new NotImplementedException();
        }
    }
}
