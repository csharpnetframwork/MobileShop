
using Microsoft.EntityFrameworkCore;
using Shop.Controllers;
using Shop.Models;


namespace Shop.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet representing the Tasks table in the database
        public DbSet<RegisterModel> RegisterModel { get; set; }
        public DbSet<StockItem>Stocks { get; set; }
        public DbSet<Sell>Sells { get; set; }
    }
}