using ProfileClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace PSI_MobileApp.DataServices
{
    public class GetData : IGetData
    {
        private String _baseUrl = "https://localhost:7034";
        public async Task<List<Profile>> GetAllProfiles()
        {
            var returnResponse = new List<Profile>();
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/api/test";
                var apiResponse = await client.GetAsync(url);
                if(apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<List<Profile>>(response);
                }
            }
            return returnResponse;
        }
    }
}
