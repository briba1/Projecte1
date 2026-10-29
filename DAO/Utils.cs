using System;
using System.Data;
using System.Data.Common;

namespace Projecte1.DAO
{
    internal class Utils
    {
        public static void CreateParameter(DbCommand query, object? value, string name, DbType type)
        {
            if (query is Oracle.ManagedDataAccess.Client.OracleCommand oracleCommand)
            {
                oracleCommand.BindByName = true;
            }

            DbParameter p = query.CreateParameter();
            p.Value = value ?? DBNull.Value;
            p.ParameterName = name;
            p.DbType = type;
            query.Parameters.Add(p);
        }

        // Paràmetre de SORTIDA (per recuperar l'ID generat amb "RETURNING ... INTO :nom").
        // Substitueix el "select last_insert_id()" de MySQL.
        public static DbParameter CreateOutputParameter(DbCommand query, string name, DbType type)
        {
            if (query is Oracle.ManagedDataAccess.Client.OracleCommand oracleCommand)
            {
                oracleCommand.BindByName = true;
            }

            DbParameter p = query.CreateParameter();
            p.ParameterName = name;
            p.DbType = type;
            p.Direction = ParameterDirection.Output;
            query.Parameters.Add(p);
            return p;
        }
    }
}
