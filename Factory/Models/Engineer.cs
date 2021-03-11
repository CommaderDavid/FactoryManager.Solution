using System.Collections.Generic;

namespace Factory.Models
{
    public class Engineer
    {
        public int EngineerId { get; set; }
        public string Name { get; set; }
        public string License { get; set; }
        public virtual ICollection<EngineerMachine> Machines { get; set; }

        public Machine()
        {
            this.Machines = new HashSet<EngineerMachine>();
        }
    }
}