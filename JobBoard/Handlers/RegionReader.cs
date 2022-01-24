using JobBoard.ViewModels.OutputModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobBoard.Handlers
{
    public class RegionReader
    {
        public async Task<List<string>> GetRegionsAsync()
        {
            try
            {
                var regions = new List<string>();
                var endpointOutPut = new EndpointOutPutModel();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"https://countriesnow.space/api/v0.1/countries"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        endpointOutPut = JsonConvert.DeserializeObject<EndpointOutPutModel>(apiResponse);
                        regions = endpointOutPut.data.SelectMany(x => x.cities).ToList();
                    }
                }
                return regions;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
