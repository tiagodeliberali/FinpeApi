using FinpeApi.Integration.DatabaseDTOs;
using FinpeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinpeApi.Integration
{
    public static class ConnectionManager
    {
        static ConnectionManager()
        {
            DbContextOptionsBuilder contextBuidler = new DbContextOptionsBuilder();
            contextBuidler.UseSqlServer(DbUtils.GetConnectionString());
            FinpeDbContext dbContext = new FinpeDbContext(contextBuidler.Options);
            dbContext.Database.Migrate();

            DbContext = dbContext;
        }

        public static FinpeDbContext DbContext { get; }
    }
}
