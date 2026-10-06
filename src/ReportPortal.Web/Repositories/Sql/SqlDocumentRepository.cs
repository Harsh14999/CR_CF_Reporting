using System.Collections.Generic;
using System.Data.SqlClient;
using ReportPortal.Web.Models;

namespace ReportPortal.Web.Repositories.Sql
{
    public class SqlDocumentRepository : IDocumentRepository
    {
        private readonly SqlConnectionFactory connectionFactory;

        public SqlDocumentRepository(SqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public IList<ApplicationDocumentInfo> GetDocumentsForApplication(string applicationCode)
        {
            var documents = new List<ApplicationDocumentInfo>();
            const string sql = @"
SELECT d.DocumentId, d.ApplicationId, a.ApplicationCode, a.ApplicationName, d.DocumentName, d.DocumentDescription, d.DocumentUrl, d.DisplayOrder, d.IsActive
FROM ApplicationDocuments d
INNER JOIN Applications a ON a.ApplicationId = d.ApplicationId
WHERE a.ApplicationCode = @ApplicationCode AND a.IsActive = 1 AND d.IsActive = 1
ORDER BY d.DisplayOrder, d.DocumentName";

            using (var connection = connectionFactory.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@ApplicationCode", applicationCode);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        documents.Add(ReadDocument(reader));
                    }
                }
            }

            return documents;
        }

        public ApplicationDocumentInfo GetDocument(int documentId)
        {
            const string sql = @"
SELECT d.DocumentId, d.ApplicationId, a.ApplicationCode, a.ApplicationName, d.DocumentName, d.DocumentDescription, d.DocumentUrl, d.DisplayOrder, d.IsActive
FROM ApplicationDocuments d
INNER JOIN Applications a ON a.ApplicationId = d.ApplicationId
WHERE d.DocumentId = @DocumentId AND a.IsActive = 1 AND d.IsActive = 1";

            using (var connection = connectionFactory.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@DocumentId", documentId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    return reader.Read() ? ReadDocument(reader) : null;
                }
            }
        }

        private static ApplicationDocumentInfo ReadDocument(SqlDataReader reader)
        {
            return new ApplicationDocumentInfo
            {
                DocumentId = (int)reader["DocumentId"],
                ApplicationId = (int)reader["ApplicationId"],
                ApplicationCode = reader["ApplicationCode"].ToString(),
                ApplicationName = reader["ApplicationName"].ToString(),
                DocumentName = reader["DocumentName"].ToString(),
                DocumentDescription = reader["DocumentDescription"] == System.DBNull.Value ? null : reader["DocumentDescription"].ToString(),
                DocumentUrl = reader["DocumentUrl"].ToString(),
                DisplayOrder = (int)reader["DisplayOrder"],
                IsActive = (bool)reader["IsActive"]
            };
        }
    }
}