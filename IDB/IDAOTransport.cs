using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Projecte1.model;

namespace Projecte1.IDAO
{
    public interface IDAOTransport
    {
        public ObservableCollection<Transport> GetTransports();

        public Transport? GetTransport(int id);
    }
}
