using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobBoard.ViewModels.InputModel
{
    public class SearchInPutModel
    {
        public string SearchString { get; set; }
        public int JobType { get; set; }
        public string Region { get; set; }
        public SelectList Regions { get; set; }
    }
    
}
