using System.Data.SqlClient;

namespace ReportPortal.Web.Repositories.Sql
{
    public class SqlAuditRepository : IAuditRepository
    {
        private readonly SqlConnectionFactory connectionFactory;

        public SqlAuditRepository(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void LogReportLaunch(string userName, int applicationId, int reportId, string reportUrl, string parametersJson)
        {
            const string sql = @"
INSERT INTO ReportExecutionLog (UserName, ApplicationId, ReportId, ReportUrl, ParametersJson)
VALUES (@UserName, @ApplicationId, @ReportId, @ReportUrl, @ParametersJson)";

            using (var connection = connectionFactory.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@ApplicationId", applicationId);
                command.Parameters.AddWithValue("@ReportId", reportId);
                command.Parameters.AddWithValue("@ReportUrl", reportUrl);
                command.Parameters.AddWithValue("@ParametersJson", (object)parametersJson ?? System.DBNull.Value);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void LogDocumentOpen(string userName, int applicationId, int documentId, string documentUrl)
        {
            const string sql = @"
INSERT INTO DocumentAccessLog (UserName, ApplicationId, DocumentId, DocumentUrl)
VALUES (@UserName, @ApplicationId, @DocumentId, @DocumentUrl)";

            using (var connection = connectionFactory.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@ApplicationId", applicationId);
                command.Parameters.AddWithValue("@DocumentId", documentId);
                command.Parameters.AddWithValue("@DocumentUrl", documentUrl);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}