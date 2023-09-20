using Domain.Helpers.Interfaces;
using Infra.Data.SqlServer.Helpers;

namespace Infra.CrossCutting.UoW
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly Session _session;
        public UnitOfWork(Session session)
        {
            _session = session;
        }

        public void Dispose() => _session.Transaction?.Dispose();

        public void Commit()
        {
            try
            {
                _session.Transaction.Commit();
            }
            catch
            {
                _session.Transaction.Rollback();
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public void Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }
    }
}
