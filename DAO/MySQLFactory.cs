namespace Projecte1.DAO;

public static class MySQLFactory
{
    public static void getUOW(System.Action<UnitOfWork> action)
    {
        using var context = new MyDBContext();
        using var uow = new UnitOfWork(context);
        uow.BeginTransaction();
        try
        {
            action(uow);
            uow.Commit();
        }
        catch
        {
            uow.Rollback();
            throw;
        }
    }
}
