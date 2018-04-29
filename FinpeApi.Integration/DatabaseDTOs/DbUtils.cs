using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FinpeApi.Integration.DatabaseDTOs
{
    public class DbUtils
    {
        private SqlConnection connection;

        public DbUtils(SqlConnection connection)
        {
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
