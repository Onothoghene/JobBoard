using JobBoard.Models.common;

namespace JobBoard.Models
{
    public class Files : CommonEntities
    {
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public string FileUniqueName { get; set; }
        public int UserId { get; set; }
        public bool? IsCV { get; set; }
        public bool? IsProfileImage { get; set; }
        public bool? IsCompanyImage { get; set; }

        public UserProfile User { get; set; }
    }
}
