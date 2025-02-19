using FullstackLabb3.Models;
using Microsoft.EntityFrameworkCore;

namespace FullstackLabb3.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}