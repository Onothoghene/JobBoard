using JobBoard.Models.common;

namespace JobBoard.Models
{
    public class SavedJobs : CommonEntities
    {
        public int UserId { get; set; }
        public int JobId { get; set; }

        public UserProfile User { get; set; }
        public Job Job { get; set; }

    }
}
