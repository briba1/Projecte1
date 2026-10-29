using Projecte1.IDAO;
using System;

namespace Projecte1.DAO;

public sealed class UnitOfWork : IDisposable
{
    private readonly MyDBContext _context;
    public IDAOUser DAOUsers { get; }
    public IDAOCity DAOCities { get; }
    public IDAOStation DAOStations { get; }
    public IDAORoute DAORoutes { get; }
    public IDAOStop DAOStops { get; }
    public IDAOTransport DAOTransports { get; }

    public UnitOfWork(MyDBContext context)
    {
        _context = context;
        DAOUsers = new DAOUser(context);
        DAOCities = new DAOCity(context);
        DAOStations = new DAOStation(context);
        DAORoutes = new DAORoute(context);
        DAOStops = new DAOStop(context);
        DAOTransports = new DAOTransport(context);
    }

    public void BeginTransaction() => _context.BeginTransaction();
    public void Commit() => _context.Commit();
    public void Rollback() => _context.Rollback();
    public void Dispose() => _context.Dispose();
}