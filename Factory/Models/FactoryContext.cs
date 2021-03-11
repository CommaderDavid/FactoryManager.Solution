using Microsoft.EntityFrameworkCore;

namespace EngineerMachine.Models
{
    public class EngineerMachineContext : DbContext
    {
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<Engineer> Engineers { get; set; }
        public virtual DbSet<EngineerMachine> EngineerMachine { get; set; }

        public EngineerMachineContext(DbContextOptions options) : base(options) { }
    }
}