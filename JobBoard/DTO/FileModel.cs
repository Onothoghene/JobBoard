namespace JobBoard.DTO
{
    public class FileModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileUniqueName { get; set; }
        public string FileExt { get; set; }
        public bool IsCV { get; set; }
        public bool IsProfileImage { get; set; }
        public bool IsCompanyImage { get; set; }
    }
}
