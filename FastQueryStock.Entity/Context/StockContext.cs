using FastQueryStock.Entity.Entity;
using Microsoft.Data.Entity;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Entity.Context
{
    public class StockContext : DbContext
    {
        private const string DATABASE_NAME = "stock.db";
        public DbSet<StockEntity> Stocks { get; set; }
        public DbSet<FavoriteStockEntity> FavoriteStock { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // cascade delete when delete cluster
            //modelBuilder.Entity<DeployConfig>()
            //    .HasMany(e => e.BareHosts)
            //    .WithRequired(child => child.ParentDeployServer)
            //    .WillCascadeOnDelete(true);

            //   base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = DATABASE_NAME };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }
}
