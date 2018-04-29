using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FinpeApi.Integration.DatabaseDTOs
{
    public class DbUtils
    {
        private SqlConnection connection;

        public static string GetConnectionString() => 
            "Server=tcp:database,1433;Initial Catalog=finpedb;Persist Security Info=False;User ID=SA;Password=P24d!dBX!qRf;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        public DbUtils()
        {
            SqlConnection connection = new SqlConnection(DbUtils.GetConnectionString());
            connection.Open();
            this.connection = connection;
        }

        public void DeteleAll<T>() where T: DbEntity, new()
        {
            var t = new T();
            connection.Execute("DELETE FROM " + t.GetName() + ";");
        }

        public void Insert(DbEntity entity)
        {
            string insertSql = entity.GetInsert() + " SELECT CAST(SCOPE_IDENTITY() as int);";
            entity.Id = connection.Query<int>(insertSql, entity).Single();
        }

        public IReadOnlyCollection<T> GetAll<T>() where T : DbEntity, new()
        {
            var t = new T();
            return connection.Query<T>("SELECT * FROM " + t.GetName() + ";").ToList();
        }
    }
}
