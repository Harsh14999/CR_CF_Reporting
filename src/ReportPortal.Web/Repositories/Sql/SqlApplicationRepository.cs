using System.Collections.Generic;
using System.Data.SqlClient;
using ReportPortal.Web.Models;

namespace ReportPortal.Web.Repositories.Sql
{
    public class SqlApplicationRepository : IApplicationRepository
    {
        private readonly SqlConnectionFactory connectionFactory;

        public SqlApplicationRepository(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public IList<ApplicationInfo> GetAuthorizedApplications(string userName)
        {
            var applications = new List<ApplicationInfo>();
            const string sql = @"
SELECT a.ApplicationId, a.ApplicationCode, a.ApplicationName, a.Description, a.IconCss, a.DisplayOrder, a.IsActive,
       CAST(CASE WHEN EXISTS (SELECT 1 FROM Reports r WHERE r.ApplicationId = a.ApplicationId AND r.IsActive = 1) THEN 1 ELSE 0 END AS bit) AS HasReports,
       CAST(CASE WHEN EXISTS (SELECT 1 FROM ApplicationDocuments d WHERE d.ApplicationId = a.ApplicationId AND d.IsActive = 1) THEN 1 ELSE 0 END AS bit) AS HasDocuments
FROM Applications a
INNER JOIN UserApplicationAccess access ON access.ApplicationId = a.ApplicationId
WHERE access.UserName = @UserName AND access.IsActive = 1 AND a.IsActive = 1
ORDER BY a.DisplayOrder, a.ApplicationName";

            using (var connection = connectionFactory.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@UserName", userName);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        applications.Add(ReadApplication(reader));
                    }
                }
            }

            return applications;
        }

        private static ApplicationInfo ReadApplication(SqlDataReader reader)
        {
            return new ApplicationInfo
            {
                ApplicationId = (int)reader["ApplicationId"],
                ApplicationCode = reader["ApplicationCode"].ToString(),
                ApplicationName = reader["ApplicationName"].ToString(),
                Description = reader["Description"] == System.DBNull.Value ? null : reader["Description"].ToString(),
                IconCss = reader["IconCss"] == System.DBNull.Value ? null : reader["IconCss"].ToString(),
                DisplayOrder = (int)reader["DisplayOrder"],
                IsActive = (bool)reader["IsActive"],
                HasReports = (bool)reader["HasReports"],
                HasDocuments = (bool)reader["HasDocuments"]
            };
        }
    }
}