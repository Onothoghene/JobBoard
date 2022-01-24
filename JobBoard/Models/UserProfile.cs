using JobBoard.Models.common;
using System.Collections.Generic;

namespace JobBoard.Models
{
    public class UserProfile : CommonEntities
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }

        public ICollection<Files> Files { get; set; }
        public ICollection<SavedJobs> SavedJobs { get; set; }
        public ICollection<JobApplication> JobApplications { get; set; }
    }
}
