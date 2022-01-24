using JobBoard.Models.common;
using System.Collections.Generic;

namespace JobBoard.Models
{
    public class JobApplication : CommonEntities
    {
        public int UserId { get; set; }
        public int JobId { get; set; }

        public UserProfile User { get; set; }
        public Job Job { get; set; }
    }
}
