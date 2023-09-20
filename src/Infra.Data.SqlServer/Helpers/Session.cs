using Domain.Helpers;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace Infra.Data.SqlServer.Helpers
{
    public sealed class Session : IDisposable
    {
        public IDbConnection Connection { get; private set; }
        public IDbTransaction Transaction { get; private set; }

        public Session(IOptions<ConnectionStrings> settings)
        {
            Connection = new SqlConnection(settings.Value.SqlServerConnection);
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
