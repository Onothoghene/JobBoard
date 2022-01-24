using System.Collections.Generic;

namespace JobBoard.Models
{
    public class JobType
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }
}
