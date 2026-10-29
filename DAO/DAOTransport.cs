using Projecte1.IDAO;
using Projecte1.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Projecte1.DAO
{
    internal class DAOTransport : IDAOTransport
    {
        private readonly MyDBContext _context;

        public DAOTransport(MyDBContext context)
        {
            _context = context;
        }

        public Transport? GetTransport(int id)
        {
            Transport? resultat = null;

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"select idM, nomMT from MITJA_TRANSPORT
                                where idM = :ID_TEXT
                ";

                Utils.CreateParameter(consulta, id, "ID_TEXT", System.Data.DbType.Int32);

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                using (var reader = consulta.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int idM = reader.GetInt32(reader.GetOrdinal("idM"));
                        string nomMT = reader.GetString(reader.GetOrdinal("nomMT"));
                        resultat = new Transport(idM, nomMT);
                    }
                }
            }

            return resultat;
        }

        public ObservableCollection<Transport> GetTransports()
        {
            ObservableCollection<Transport> resultat = new ObservableCollection<Transport>();

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"select idM, nomMT from MITJA_TRANSPORT
                                order by idM
                ";

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                using (var reader = consulta.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idM = reader.GetInt32(reader.GetOrdinal("idM"));
                        string nomMT = reader.GetString(reader.GetOrdinal("nomMT"));

                        Transport t = new Transport(idM, nomMT);

                        resultat.Add(t);
                    }
                }
            }

            return resultat;
        }
    }
}