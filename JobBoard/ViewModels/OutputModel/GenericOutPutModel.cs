using JobBoard.ViewModels.InputModel;
using System.Collections.Generic;

namespace JobBoard.ViewModels.OutputModel
{
    public class GenericOutPutModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class GeneralOutPutModel
    {
        public SearchInPutModel Search { get; set; }
    }
    
    public class RegionOutPutModel
    {
        public string Name { get; set; }
    }
    
    public class CountryOutPutModel
    {
        public string country { get; set; }
        public List<string> cities { get; set; }
    }

    public class EndpointOutPutModel
    {
        public List<CountryOutPutModel> data { get; set; }
    }
}
