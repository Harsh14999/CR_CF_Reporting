using System.Collections.Generic;
using System.Data.SqlClient;
using ReportPortal.Web.Models;

namespace ReportPortal.Web.Repositories.Sql
{
    public class SqlReportRepository : IReportRepository
    {
        private readonly SqlConnectionFactory connectionFactory;

        public SqlReportRepository(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public IList<ReportInfo> GetReportsForApplication(string applicationCode)
        {
            var reports = new List<ReportInfo>();
            const string sql = @"
SELECT r.ReportId, r.ApplicationId, a.ApplicationCode, a.ApplicationName, r.ReportName, r.ReportDescription, r.ReportServerPath, r.OpenMode, r.DisplayOrder, r.IsActive
FROM Reports r
INNER JOIN Applications a ON a.ApplicationId = r.ApplicationId
WHERE a.ApplicationCode = @ApplicationCode AND a.IsActive = 1 AND r.IsActive = 1
ORDER BY r.DisplayOrder, r.ReportName";

            using (var connection = connectionFactory.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@ApplicationCode", applicationCode);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reports.Add(ReadReport(reader));
                    }
                }
            }

            return reports;
        }

        public ReportInfo GetReport(int reportId)
        {
            const string sql = @"
SELECT r.ReportId, r.ApplicationId, a.ApplicationCode, a.ApplicationName, r.ReportName, r.ReportDescription, r.ReportServerPath, r.OpenMode, r.DisplayOrder, r.IsActive
FROM Reports r
INNER JOIN Applications a ON a.ApplicationId = r.ApplicationId
WHERE r.ReportId = @ReportId AND a.IsActive = 1 AND r.IsActive = 1";

            using (var connection = connectionFactory.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@ReportId", reportId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    return reader.Read() ? ReadReport(reader) : null;
                }
            }
        }

        private static ReportInfo ReadReport(SqlDataReader reader)
        {
            return new ReportInfo
            {
                ReportId = (int)reader["ReportId"],
                ApplicationId = (int)reader["ApplicationId"],
                ApplicationCode = reader["ApplicationCode"].ToString(),
                ApplicationName = reader["ApplicationName"].ToString(),
                ReportName = reader["ReportName"].ToString(),
                ReportDescription = reader["ReportDescription"] == System.DBNull.Value ? null : reader["ReportDescription"].ToString(),
                ReportServerPath = reader["ReportServerPath"].ToString(),
                OpenMode = reader["OpenMode"].ToString(),
                DisplayOrder = (int)reader["DisplayOrder"],
                IsActive = (bool)reader["IsActive"]
            };
        }
    }
}