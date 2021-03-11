using Microsoft.EntityFrameworkCore;

namespace Factory.Models
{
    public class FactoryContext : DbContext
    {
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<Engineer> Engineers { get; set; }
        public virtual DbSet<EngineerMachine> EngineerMachine { get; set; }

        public FactoryContext(DbContextOptions options) : base(options) { }
    }
}