using Oracle.ManagedDataAccess.Client;
using System;

namespace Projecte1.DAO;

public sealed class MyDBContext : IDisposable
{
    // Canvia aquests valors si la IP, el port o el servei Oracle són diferents.
    private const string ConnectionString =
        "User Id=ALUMNE;Password=alumne;Data Source=10.2.78.110:1521/XEPDB1;";

    public OracleConnection Connection { get; }
    public OracleTransaction? Transaction { get; private set; }

    public MyDBContext()
    {
        Connection = new OracleConnection(ConnectionString);
        Connection.Open();
    }

    public void BeginTransaction()
    {
        Transaction ??= Connection.BeginTransaction();
    }

    public void Commit()
    {
        Transaction?.Commit();
        Transaction?.Dispose();
        Transaction = null;
    }

    public void Rollback()
    {
        if (Transaction is null) return;
        try { Transaction.Rollback(); }
        finally { Transaction.Dispose(); Transaction = null; }
    }

    public void Dispose()
    {
        Transaction?.Dispose();
        Connection.Dispose();
    }
}
