using Projecte1.IDAO;
using Projecte1.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Projecte1.DAO
{
    internal class DAOStop : IDAOStop
    {
        private readonly MyDBContext _context;

        public DAOStop(MyDBContext context)
        {
            _context = context;
        }

        public ObservableCollection<Stop> GetStops(int route, int station,
                        int offset = 0, int rows_per_page = -1)
        {
            ObservableCollection<Stop> resultat = new ObservableCollection<Stop>();

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"select trajecte, ordre, hora_pas, estacio from PARADA
                                where
                                ( :ROUTE_TEXT=-1 OR trajecte = :ROUTE_TEXT ) AND
                                ( :STATION_TEXT=-1 OR estacio = :STATION_TEXT )
                                order by trajecte, ordre
                ";
                if (rows_per_page > 0)
                {
                    consulta.CommandText += $" offset {offset} rows fetch next {rows_per_page} rows only";
                }

                Utils.CreateParameter(consulta, route, "ROUTE_TEXT", System.Data.DbType.Int32);
                Utils.CreateParameter(consulta, station, "STATION_TEXT", System.Data.DbType.Int32);

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                using (var reader = consulta.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int trajecte = reader.GetInt32(reader.GetOrdinal("trajecte"));
                        int ordre = reader.GetInt32(reader.GetOrdinal("ordre"));
                        int horaPas = reader.GetInt32(reader.GetOrdinal("hora_pas"));
                        int estacio = reader.GetInt32(reader.GetOrdinal("estacio"));

                        Stop s = new Stop(trajecte, ordre, horaPas, estacio);

                        resultat.Add(s);
                    }
                }
            }

            return resultat;
        }

        public ObservableCollection<Stop> GetStopsOfValidatedRoutes()
        {
            ObservableCollection<Stop> resultat = new ObservableCollection<Stop>();

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"select p.trajecte, p.ordre, p.hora_pas, p.estacio
                                from PARADA p
                                join TRAJECTE t on t.idT = p.trajecte
                                where t.validat = 1
                                order by p.trajecte, p.ordre
                ";

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                using (var reader = consulta.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int trajecte = reader.GetInt32(reader.GetOrdinal("trajecte"));
                        int ordre = reader.GetInt32(reader.GetOrdinal("ordre"));
                        int horaPas = reader.GetInt32(reader.GetOrdinal("hora_pas"));
                        int estacio = reader.GetInt32(reader.GetOrdinal("estacio"));

                        Stop s = new Stop(trajecte, ordre, horaPas, estacio);

                        resultat.Add(s);
                    }
                }
            }

            return resultat;
        }

        public bool InsertStop(Stop s)
        {
            if (_context.Transaction == null)
            {
                throw new Exception("NO TRANSACTION");
            }

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"insert into PARADA (trajecte, ordre, hora_pas, estacio)
                                values (:TRAJECTE_TEXT, :ORDRE_TEXT, :HORA_PAS, :ESTACIO_TEXT)
                ";

                Utils.CreateParameter(consulta, s.Route, "TRAJECTE_TEXT", System.Data.DbType.Int32);
                Utils.CreateParameter(consulta, s.Order, "ORDRE_TEXT", System.Data.DbType.Int32);
                Utils.CreateParameter(consulta, s.Time, "HORA_PAS", System.Data.DbType.Int32);
                Utils.CreateParameter(consulta, s.Station, "ESTACIO_TEXT", System.Data.DbType.Int32);

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                int files = consulta.ExecuteNonQuery();

                if (files != 1)
                {
                    _context.Rollback();
                    return false;
                }

                return true;
            }
        }

        public bool UpdateStop(Stop s)
        {
            if (_context.Transaction == null)
            {
                throw new Exception("NO TRANSACTION");
            }

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"update PARADA
                                set hora_pas = :HORA_PAS,
                                    estacio = :ESTACIO_TEXT
                                where trajecte = :TRAJECTE_TEXT and ordre = :ORDRE_TEXT
                ";

                Utils.CreateParameter(consulta, s.Time, "HORA_PAS", System.Data.DbType.Int32);
                Utils.CreateParameter(consulta, s.Station, "ESTACIO_TEXT", System.Data.DbType.Int32);
                Utils.CreateParameter(consulta, s.Route, "TRAJECTE_TEXT", System.Data.DbType.Int32);
                Utils.CreateParameter(consulta, s.Order, "ORDRE_TEXT", System.Data.DbType.Int32);

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                int files = consulta.ExecuteNonQuery();

                if (files != 1)
                {
                    _context.Rollback();
                    return false;
                }

                return true;
            }
        }

        public bool DeleteStop(int route, int order)
        {
            if (_context.Transaction == null)
            {
                throw new Exception("NO TRANSACTION");
            }

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"delete from PARADA where trajecte = :TRAJECTE_TEXT and ordre = :ORDRE_TEXT
                ";

                Utils.CreateParameter(consulta, route, "TRAJECTE_TEXT", System.Data.DbType.Int32);
                Utils.CreateParameter(consulta, order, "ORDRE_TEXT", System.Data.DbType.Int32);

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                int files = consulta.ExecuteNonQuery();

                if (files != 1)
                {
                    _context.Rollback();
                    return false;
                }

                return true;
            }
        }

        public bool DeleteStopsByRoute(int route)
        {
            if (_context.Transaction == null)
            {
                throw new Exception("NO TRANSACTION");
            }

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"delete from PARADA where trajecte = :TRAJECTE_TEXT
                ";

                Utils.CreateParameter(consulta, route, "TRAJECTE_TEXT", System.Data.DbType.Int32);

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                // 0 files NO és error: pot ser que el trajecte no tingui parades
                consulta.ExecuteNonQuery();

                return true;
            }
        }
    }
}