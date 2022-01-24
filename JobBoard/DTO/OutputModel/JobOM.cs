using System;

namespace JobBoard.DTO.OutputModel
{
    public class JobOM
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobLocation { get; set; }
        public string Region { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public int JobTypeId { get; set; }
        public string JobType { get; set; }
    }
}
