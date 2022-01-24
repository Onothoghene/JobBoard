using JobBoard.Models.common;
using System.Collections.Generic;

namespace JobBoard.Models
{
    public class Job : CommonEntities
    {
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobLocation { get; set; }
        public string Region { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public int JobTypeId { get; set; }
        public int CreatedBy { get; set; }

        public JobType JobType { get; set; }
        public UserProfile CreatedByNavigation { get; set; }
        public ICollection<Files> Files { get; set; }
        public ICollection<SavedJobs> SavedJobs { get; set; }
        public ICollection<JobApplication> JobApplications { get; set; }
    }
}
