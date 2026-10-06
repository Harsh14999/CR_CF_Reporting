using System.Data.SqlClient;

namespace ReportPortal.Web.Repositories.Sql
{
    public class SqlAccessRepository : IAccessRepository
    {
        private readonly SqlConnectionFactory connectionFactory;

        public SqlAccessRepository(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public bool UserHasApplicationAccess(string userName, string applicationCode)
        {
            const string sql = @"
SELECT COUNT(1)
FROM UserApplicationAccess access
INNER JOIN Applications a ON a.ApplicationId = access.ApplicationId
WHERE access.UserName = @UserName AND a.ApplicationCode = @ApplicationCode AND access.IsActive = 1 AND a.IsActive = 1";

            using (var connection = connectionFactory.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@ApplicationCode", applicationCode);
                connection.Open();
                return (int)command.ExecuteScalar() > 0;
            }
        }

        public bool UserHasReportAccess(string userName, int reportId)
        {
            const string sql = @"
SELECT COUNT(1)
FROM Reports r
INNER JOIN Applications a ON a.ApplicationId = r.ApplicationId
INNER JOIN UserApplicationAccess appAccess ON appAccess.ApplicationId = a.ApplicationId
WHERE r.ReportId = @ReportId
  AND appAccess.UserName = @UserName
  AND appAccess.IsActive = 1
  AND a.IsActive = 1
  AND r.IsActive = 1
  AND (
      NOT EXISTS (SELECT 1 FROM UserReportAccess reportAccess WHERE reportAccess.UserName = @UserName AND reportAccess.ReportId = @ReportId)
      OR EXISTS (SELECT 1 FROM UserReportAccess reportAccess WHERE reportAccess.UserName = @UserName AND reportAccess.ReportId = @ReportId AND reportAccess.CanView = 1)
  )";

            using (var connection = connectionFactory.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@ReportId", reportId);
                connection.Open();
                return (int)command.ExecuteScalar() > 0;
            }
        }
    }
}