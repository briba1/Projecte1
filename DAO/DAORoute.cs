using Projecte1.IDAO;
using Projecte1.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Projecte1.DAO
{
    internal class DAORoute : IDAORoute
    {
        private readonly MyDBContext _context;

        public DAORoute(MyDBContext context)
        {
            _context = context;
        }

        public Route? GetRoute(int id)
        {
            Route? resultat = null;

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"select idT, nomT, data_crea, validat, usuari, mitja_trans from TRAJECTE
                                where idT = :ID_TEXT
                ";

                Utils.CreateParameter(consulta, id, "ID_TEXT", System.Data.DbType.Int32);

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                using (var reader = consulta.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int idT = reader.GetInt32(reader.GetOrdinal("idT"));
                        string nomT = reader.GetString(reader.GetOrdinal("nomT"));
                        DateTime dataCrea = reader.GetDateTime(reader.GetOrdinal("data_crea"));

                        // validat és NUMBER(1) -> decimal
                        int validatInt = Convert.ToInt32(reader.GetDecimal(reader.GetOrdinal("validat")));
                        bool validat = validatInt == 1;

                        int usuari = reader.GetInt32(reader.GetOrdinal("usuari"));
                        int mitjaTrans = reader.GetInt32(reader.GetOrdinal("mitja_trans"));

                        resultat = new Route(idT, nomT, dataCrea, validat, usuari, mitjaTrans);
                    }
                }
            }

            return resultat;
        }

        public ObservableCollection<Route> GetRoutes(bool? validat, int user,
                        int offset = 0, int rows_per_page = -1)
        {
            ObservableCollection<Route> resultat = new ObservableCollection<Route>();

            // validat: null = tots; true/false = 1/0 (passat com a int, -1 = sense filtre)
            int validatInt = validat.HasValue ? (validat.Value ? 1 : 0) : -1;

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"select idT, nomT, data_crea, validat, usuari, mitja_trans from TRAJECTE
                                where
                                ( :VALIDAT_TEXT=-1 OR validat = :VALIDAT_TEXT ) AND
                                ( :USER_TEXT=-1 OR usuari = :USER_TEXT )
                                order by idT
                ";
                if (rows_per_page > 0)
                {
                    consulta.CommandText += $" offset {offset} rows fetch next {rows_per_page} rows only";
                }

                Utils.CreateParameter(consulta, validatInt, "VALIDAT_TEXT", System.Data.DbType.Int32);
                Utils.CreateParameter(consulta, user, "USER_TEXT", System.Data.DbType.Int32);

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                using (var reader = consulta.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idT = reader.GetInt32(reader.GetOrdinal("idT"));
                        string nomT = reader.GetString(reader.GetOrdinal("nomT"));
                        DateTime dataCrea = reader.GetDateTime(reader.GetOrdinal("data_crea"));

                        int validatIntCol = Convert.ToInt32(reader.GetDecimal(reader.GetOrdinal("validat")));
                        bool validatCol = validatIntCol == 1;

                        int usuari = reader.GetInt32(reader.GetOrdinal("usuari"));
                        int mitjaTrans = reader.GetInt32(reader.GetOrdinal("mitja_trans"));

                        Route r = new Route(idT, nomT, dataCrea, validatCol, usuari, mitjaTrans);

                        resultat.Add(r);
                    }
                }
            }

            return resultat;
        }

        public bool InsertRoute(Route r)
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
                    @"insert into TRAJECTE (nomT, data_crea, validat, mitja_trans, usuari)
                                values (:NAME_TEXT, :DATA_CREA, :VALIDAT_TEXT, :MITJA_TRANS, :USUARI)
                                returning idT into :IDT_OUT
                ";

                Utils.CreateParameter(consulta, r.NameT, "NAME_TEXT", System.Data.DbType.String);
                Utils.CreateParameter(consulta, r.CreateDate, "DATA_CREA", System.Data.DbType.DateTime);
                Utils.CreateParameter(consulta, r.Validat ? 1 : 0, "VALIDAT_TEXT", System.Data.DbType.Int32);
                Utils.CreateParameter(consulta, r.Transport, "MITJA_TRANS", System.Data.DbType.Int32);
                Utils.CreateParameter(consulta, r.User, "USUARI", System.Data.DbType.Int32);
                var idOut = Utils.CreateOutputParameter(consulta, "IDT_OUT", System.Data.DbType.Int32);

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                int files = consulta.ExecuteNonQuery();

                if (files != 1)
                {
                    _context.Rollback();
                    return false;
                }

                r.IdT = int.Parse(idOut.Value!.ToString()!);

                return true;
            }
        }

        public bool UpdateRoute(Route r)
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
                // NO canviem ni usuari ni data_crea
                consulta.CommandText =
                    @"update TRAJECTE
                                set nomT = :NAME_TEXT,
                                    validat = :VALIDAT_TEXT,
                                    mitja_trans = :MITJA_TRANS
                                where idT = :ID_TEXT
                ";

                Utils.CreateParameter(consulta, r.NameT, "NAME_TEXT", System.Data.DbType.String);
                Utils.CreateParameter(consulta, r.Validat ? 1 : 0, "VALIDAT_TEXT", System.Data.DbType.Int32);
                Utils.CreateParameter(consulta, r.Transport, "MITJA_TRANS", System.Data.DbType.Int32);
                Utils.CreateParameter(consulta, r.IdT, "ID_TEXT", System.Data.DbType.Int32);

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

        public bool DeleteRoute(int id)
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
                    @"delete from TRAJECTE where idT = :ID_TEXT
                ";

                Utils.CreateParameter(consulta, id, "ID_TEXT", System.Data.DbType.Int32);

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
    }
}