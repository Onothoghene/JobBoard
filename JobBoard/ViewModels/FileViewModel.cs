namespace JobBoard.ViewModels
{
    public class FileViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public string FileUniqueName { get; set; }
        public bool IsCV { get; set; }
        public bool IsProfileImage { get; set; }
        public bool IsCompanyImage { get; set; }
    }
}
