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
        private readonly MyDBContext _context;

        public DAOCity(MyDBContext context)
        {
            _context = context;
        }

        public City? GetCity(int id)
        {
            ObservableCollection<City> cities = GetCities(id, "");
            if (cities.Count == 0) return null;
            else return cities[0];
        }

        public City? GetCityByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            City? resultat = null;

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"select idC, nom from CIUTAT
                                where LOWER(nom) = LOWER(:NAME_TEXT)
                ";

                Utils.CreateParameter(consulta, name.Trim(), "NAME_TEXT", System.Data.DbType.String);

                consulta.Transaction = _context.Transaction;

                using (var reader = consulta.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int idC = reader.GetInt32(reader.GetOrdinal("idC"));
                        string nom = reader.GetString(reader.GetOrdinal("nom"));
                        resultat = new City(idC, nom);
                    }
                }
            }

            return resultat;
        }

        public ObservableCollection<City> GetCities(int id, string name,
                        int offset = 0, int rows_per_page = -1)
        {
            ObservableCollection<City> resultat = new ObservableCollection<City>();

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"select idC, nom from CIUTAT
                                where
                                ( :ID_TEXT=-1 OR idC = :ID_TEXT ) AND
                                ( :NAME_TEXT='%%' OR LOWER(nom) LIKE LOWER(:NAME_TEXT) )
                                order by idC
                ";
                if (rows_per_page > 0)
                {
                    consulta.CommandText += $" offset {offset} rows fetch next {rows_per_page} rows only";
                }

                Utils.CreateParameter(consulta, id, "ID_TEXT", System.Data.DbType.Int32);
                Utils.CreateParameter(consulta, "%" + (name ?? "") + "%", "NAME_TEXT", System.Data.DbType.String);

                consulta.Transaction = _context.Transaction;

                using (var reader = consulta.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idC = reader.GetInt32(reader.GetOrdinal("idC"));
                        string nom = reader.GetString(reader.GetOrdinal("nom"));

                        City c = new City(idC, nom);

                        resultat.Add(c);
                    }
                }
            }

            return resultat;
        }
    }
}