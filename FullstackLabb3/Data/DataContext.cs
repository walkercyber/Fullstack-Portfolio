using FullstackLabb3.Models;
using Microsoft.EntityFrameworkCore;

namespace FullstackLabb3.Data
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options) { }
        public DbSet<Portfolio> Portfolios { get; set; }
    }
}