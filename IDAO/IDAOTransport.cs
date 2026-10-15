using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Projecte1.model;

namespace Projecte1.IDAO
{
    public interface IDAOTransport
    {
        public ObservableCollection<Transport> GetTransports(int offset = 0, int rows_per_page = -1);

        public Transport GetTransport(int id);

        public bool UpdateTransport(Transport t);

        public bool InsertTransport(Transport t);

        public bool DeleteTransport(int id);

    }
}