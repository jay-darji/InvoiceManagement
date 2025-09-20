using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InvoiceManagement.DBContext
{
    public class DatabaseContext : DbContext
    {
        private readonly string dbName;
        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration) : base(options)
        {
            dbName = configuration.GetConnectionString("Database");

        }

        public DbSet<Models.Invoices> Invoices { get; set; }
        public DbSet<Models.InvoiceItems> InvoiceItems { get; set; }
    }
}
