using System.Configuration;
using System.Data.SqlClient;

namespace ReportPortal.Web.Repositories.Sql
{
    public class SqlConnectionFactory
    {
        public SqlConnection CreateConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ReportPortalConnection"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}