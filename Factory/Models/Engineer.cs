using System.Collections.Generic;

namespace Factory.Models
{
    public class Engineer
    {
        public int CampaignId { get; set; }
        public string Name { get; set; }
        public string Setting { get; set; }
        public string Rulebook { get; set; }
        public virtual ICollection<CampaignCharacter> Characters { get; set; }

        public Campaign()
        {
            this.Characters = new HashSet<CampaignCharacter>();
        }
    }
}