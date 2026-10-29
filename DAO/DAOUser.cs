using Projecte1.IDAO;
using Projecte1.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Projecte1.DAO
{
    internal class DAOUser : IDAOUser
    {
        private readonly MyDBContext _context;

        public DAOUser(MyDBContext context)
        {
            _context = context;
        }

        public User? GetUser(int id)
        {
            User? resultat = null;

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"select idU, email, nomU, passwd, tipus, data_reg from USUARI
                                where idU = :ID_TEXT
                ";

                Utils.CreateParameter(consulta, id, "ID_TEXT", System.Data.DbType.Int32);

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                using (var reader = consulta.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int idU = reader.GetInt32(reader.GetOrdinal("idU"));
                        string email = reader.GetString(reader.GetOrdinal("email"));
                        string nomU = reader.GetString(reader.GetOrdinal("nomU"));
                        string passwd = reader.GetString(reader.GetOrdinal("passwd"));

                        // tipus és CHAR(1): llegim el primer caràcter del string
                        char tipus = ' ';
                        int ordTipus = reader.GetOrdinal("tipus");
                        if (!reader.IsDBNull(ordTipus))
                        {
                            string tipusStr = reader.GetString(ordTipus);
                            if (tipusStr.Length > 0) tipus = tipusStr[0];
                        }

                        DateTime dataReg = reader.GetDateTime(reader.GetOrdinal("data_reg"));

                        resultat = new User(idU, email, nomU, passwd, tipus, dataReg);
                    }
                }
            }

            return resultat;
        }

        public User? GetUserByMail(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail)) return null;

            User? resultat = null;

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"select idU, email, nomU, passwd, tipus, data_reg from USUARI
                                where LOWER(email) = LOWER(:MAIL_TEXT)
                ";

                Utils.CreateParameter(consulta, mail.Trim(), "MAIL_TEXT", System.Data.DbType.String);

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                using (var reader = consulta.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int idU = reader.GetInt32(reader.GetOrdinal("idU"));
                        string email = reader.GetString(reader.GetOrdinal("email"));
                        string nomU = reader.GetString(reader.GetOrdinal("nomU"));
                        string passwd = reader.GetString(reader.GetOrdinal("passwd"));

                        char tipus = ' ';
                        int ordTipus = reader.GetOrdinal("tipus");
                        if (!reader.IsDBNull(ordTipus))
                        {
                            string tipusStr = reader.GetString(ordTipus);
                            if (tipusStr.Length > 0) tipus = tipusStr[0];
                        }

                        DateTime dataReg = reader.GetDateTime(reader.GetOrdinal("data_reg"));

                        resultat = new User(idU, email, nomU, passwd, tipus, dataReg);
                    }
                }
            }

            return resultat;
        }

        public ObservableCollection<User> GetEditors(int offset = 0, int rows_per_page = -1)
        {
            ObservableCollection<User> resultat = new ObservableCollection<User>();

            var connexio = _context.Connection;

            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            using (var consulta = connexio.CreateCommand())
            {
                consulta.CommandText =
                    @"select idU, email, nomU, passwd, tipus, data_reg from USUARI
                                where tipus IN ('E','B')
                                order by idU
                ";
                if (rows_per_page > 0)
                {
                    consulta.CommandText += $" offset {offset} rows fetch next {rows_per_page} rows only";
                }

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                using (var reader = consulta.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idU = reader.GetInt32(reader.GetOrdinal("idU"));
                        string email = reader.GetString(reader.GetOrdinal("email"));
                        string nomU = reader.GetString(reader.GetOrdinal("nomU"));
                        string passwd = reader.GetString(reader.GetOrdinal("passwd"));

                        char tipus = ' ';
                        int ordTipus = reader.GetOrdinal("tipus");
                        if (!reader.IsDBNull(ordTipus))
                        {
                            string tipusStr = reader.GetString(ordTipus);
                            if (tipusStr.Length > 0) tipus = tipusStr[0];
                        }

                        DateTime dataReg = reader.GetDateTime(reader.GetOrdinal("data_reg"));

                        User u = new User(idU, email, nomU, passwd, tipus, dataReg);

                        resultat.Add(u);
                    }
                }
            }

            return resultat;
        }

        public bool InsertUser(User u)
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
                    @"insert into USUARI (email, nomU, passwd, tipus, data_reg)
                                values (:EMAIL_TEXT, :NAME_TEXT, :PASSWD_TEXT, :TIPUS_TEXT, :DATA_REG)
                                returning idU into :IDU_OUT
                ";

                Utils.CreateParameter(consulta, u.Mail, "EMAIL_TEXT", System.Data.DbType.String);
                Utils.CreateParameter(consulta, u.Name, "NAME_TEXT", System.Data.DbType.String);
                Utils.CreateParameter(consulta, u.Passwd, "PASSWD_TEXT", System.Data.DbType.String);
                Utils.CreateParameter(consulta, u.UType.ToString(), "TIPUS_TEXT", System.Data.DbType.String);
                Utils.CreateParameter(consulta, u.Data_reg, "DATA_REG", System.Data.DbType.DateTime);
                var idOut = Utils.CreateOutputParameter(consulta, "IDU_OUT", System.Data.DbType.Int32);

                // IMPORTANT !!! NO us ho deixeu <---!!!!!!!
                consulta.Transaction = _context.Transaction;

                int files = consulta.ExecuteNonQuery();

                if (files != 1)
                {
                    _context.Rollback();
                    return false;
                }

                u.IdU = int.Parse(idOut.Value!.ToString()!);

                return true;
            }
        }

        public bool UpdateUserType(int idU, char uType)
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
                    @"update USUARI set tipus = :TIPUS_TEXT
                                where idU = :ID_TEXT
                ";

                Utils.CreateParameter(consulta, uType.ToString(), "TIPUS_TEXT", System.Data.DbType.String);
                Utils.CreateParameter(consulta, idU, "ID_TEXT", System.Data.DbType.Int32);

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