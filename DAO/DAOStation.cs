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
        private readonly MyDBContext _context;

        public DAOStation(MyDBContext context)
        {
            _context = context;
        }

        public Station? GetStation(int id)
        {
            Station? resultat = null;

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"select idE, nomE, latitud, longitud, ciutat from ESTACIO
                                where idE = :ID_TEXT
                ";

                Utils.CreateParameter(consulta, id, "ID_TEXT", System.Data.DbType.Int32);

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                using (var reader = consulta.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int idE = reader.GetInt32(reader.GetOrdinal("idE"));
                        string nomE = reader.GetString(reader.GetOrdinal("nomE"));

                        // latitud i longitud són anulables a la BD
                        double latitud = 0;
                        int ordLat = reader.GetOrdinal("latitud");
                        if (!reader.IsDBNull(ordLat)) latitud = reader.GetDouble(ordLat);

                        double longitud = 0;
                        int ordLon = reader.GetOrdinal("longitud");
                        if (!reader.IsDBNull(ordLon)) longitud = reader.GetDouble(ordLon);

                        int ciutat = reader.GetInt32(reader.GetOrdinal("ciutat"));

                        resultat = new Station(idE, nomE, latitud, longitud, ciutat);
                    }
                }
            }

            return resultat;
        }

        public ObservableCollection<Station> GetStations(int city, string name,
                        int offset = 0, int rows_per_page = -1)
        {
            ObservableCollection<Station> resultat = new ObservableCollection<Station>();

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"select idE, nomE, latitud, longitud, ciutat from ESTACIO
                                where
                                ( :CITY_TEXT=-1 OR ciutat = :CITY_TEXT ) AND
                                ( :NAME_TEXT='%%' OR LOWER(nomE) LIKE LOWER(:NAME_TEXT) )
                                order by idE
                ";
                if (rows_per_page > 0)
                {
                    consulta.CommandText += $" offset {offset} rows fetch next {rows_per_page} rows only";
                }

                Utils.CreateParameter(consulta, city, "CITY_TEXT", System.Data.DbType.Int32);
                Utils.CreateParameter(consulta, "%" + (name ?? "") + "%", "NAME_TEXT", System.Data.DbType.String);

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                using (var reader = consulta.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idE = reader.GetInt32(reader.GetOrdinal("idE"));
                        string nomE = reader.GetString(reader.GetOrdinal("nomE"));

                        double latitud = 0;
                        int ordLat = reader.GetOrdinal("latitud");
                        if (!reader.IsDBNull(ordLat)) latitud = reader.GetDouble(ordLat);

                        double longitud = 0;
                        int ordLon = reader.GetOrdinal("longitud");
                        if (!reader.IsDBNull(ordLon)) longitud = reader.GetDouble(ordLon);

                        int ciutat = reader.GetInt32(reader.GetOrdinal("ciutat"));

                        Station s = new Station(idE, nomE, latitud, longitud, ciutat);

                        resultat.Add(s);
                    }
                }
            }

            return resultat;
        }
    }
}