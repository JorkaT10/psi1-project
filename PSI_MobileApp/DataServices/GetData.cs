using ProfileClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using ClassLibrary;
using System.Net.Http.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PSI_MobileApp.DataServices
{
    public class GetData : IGetData
    {
        private String _baseUrl = "https://localhost:7034";
        public async Task<ObservableCollection<Profile>> GetAllProfiles()
        {
            var returnResponse = new ObservableCollection<Profile>();
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/GetAllProfiles";
                var apiResponse = await client.GetAsync(url);
                if(apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<ObservableCollection<Profile>>(response);
                }
            }
            return returnResponse;
        }
        public async Task<Profile> GetProfileById(Guid? id)
        {
            Profile returnResponse = null;
            if((id == null) || (id == Guid.Empty))
            {
                return null;
            }
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/GetProfileById?id={id}";
                var apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Profile>(response);
                }
            }
            return returnResponse;
        }
        public async Task<ObservableCollection<Profile>> GetDistributorProfiles()
        {
            ObservableCollection<Profile> returnResponse = null;
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/GetDistributorProfiles";
                var apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<ObservableCollection<Profile>>(response);
                }
            }
            return returnResponse;
        }
        public async Task<ObservableCollection<Account>> GetAllAccounts()
        {
            var returnResponse = new ObservableCollection<Account>();
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/GetAllAccounts";
                var apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<ObservableCollection<Account>>(response);
                }
            }
            return returnResponse;
        }


        public async Task<ObservableCollection<Advertisement>> GetAllAdvertisements()
        {
            var returnResponse = new ObservableCollection<Advertisement>();
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/GetAllAdvertisements";
                var apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<ObservableCollection<Advertisement>>(response);
                }
            }
            return returnResponse;
        }

        public async Task<Advertisement>GetAdvertisementsById(Guid id)
        {
            var returnResponse = new Advertisement();
            if(id == Guid.Empty)
            {
                return null;
            }
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/GetAdvertisementsById?id={id}";
                var apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Advertisement>(response);
                }
            }
            return returnResponse;
        }

        public async Task<ObservableCollection<Advertisement>>GetAdsByDistributorId(Guid id)
        {
            var returnResponse = new ObservableCollection<Advertisement>();
            if (id == Guid.Empty)
            {
                return null;
            }
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/GetAdsByDistributorId?id={id}";
                var apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<ObservableCollection<Advertisement>>(response);
                }
            }
            return returnResponse;
        }

        public async Task<ObservableCollection<Advertisement>>GetAdsByBuyerId(Guid id)
        {
            var returnResponse = new ObservableCollection<Advertisement>();
            if (id == Guid.Empty)
            {
                return null;
            }
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/GetAdsByBuyerId?id={id}";
                var apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<ObservableCollection<Advertisement>>(response);
                }
            }
            return returnResponse;
        }

        public async Task<ObservableCollection<Distributor>>GetAllDistributors()
        {
            var returnResponse = new ObservableCollection<Distributor>();
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/GetAllDistributors";
                var apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<ObservableCollection<Distributor>>(response);
                }
            }
            return returnResponse;
        }

        public async Task<Distributor>GetDistributorsById(Guid id)
        {
            var returnResponse = new Distributor();
            if (id == Guid.Empty)
            {
                return null;
            }
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/GetDistributorsById?id={id}";
                var apiResponse = await client.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Distributor>(response);
                }
            }
            return returnResponse;
        }
        public Distributor GetDistributorsByIdConcurrent(Guid id)
        {
            var returnResponse = new Distributor();
            if (id == Guid.Empty)
            {
                return null;
            }
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/GetDistributorsByIdConcurrent?id={id}";
                var apiResponse = client.GetAsync(url).Result;
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = apiResponse.Content.ReadAsStringAsync().Result;
                    returnResponse = JsonConvert.DeserializeObject<Distributor>(response);
                }
            }
            return returnResponse;
        }
        public async Task AddDistributor(Guid id)
        {
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/AddDistributor";
                var apiResponse = await client.PutAsJsonAsync(url, id);
                if (!apiResponse.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();
                }
            }
        }
        public async Task AddNewProfile(Profile profile)
        {
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/AddNewProfile";
                var apiResponse = await client.PutAsJsonAsync(url, profile);
                if (!apiResponse.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();
                }
            }
        }
        public async Task AddNewAccount(Account account)
        {
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/AddNewAccount";
                var apiResponse = await client.PutAsJsonAsync(url, account);
                if (!apiResponse.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();
                }
            }
        }
        public async Task AddAd(Advertisement advertisement, Guid id)
        {
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/AddAd?distributorId={id}";
                var text = JsonConvert.SerializeObject(advertisement);
                var apiResponse = await client.PutAsJsonAsync(url, advertisement);
                if (!apiResponse.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();
                }
            }
        }
        public async Task RemoveOutdated(DateTime now)
        {
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/RemoveOutdated";
                var apiResponse = await client.PutAsJsonAsync(url, now);
                if (!apiResponse.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();
                }
            }
        }
        public async Task RemoveAdvertisement(Advertisement advertisement)
        {
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/RemoveAdvertisement";
                var apiResponse = await client.PutAsJsonAsync(url, advertisement);
                if (!apiResponse.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();
                }
            }
        }
        public async Task ChangeOrderStatus(Advertisement advertisement, Guid id)
        {
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/ChangeOrderStatus?id={id}";
                var apiResponse = await client.PutAsJsonAsync(url, advertisement);
                if (!apiResponse.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();
                }
            }
        }
        public async Task<Guid> GetAccount(string username, string password)
        {
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/GetAccount?username={username}&password={password}";
                var apiResponse = await client.GetAsync(url);
                if (!apiResponse.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();
                }
                var response = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Guid>(response);
            }
        }
        public async Task UpdateProfile()
        {
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/Update";
                var apiResponse = await client.GetAsync(url);
                if (!apiResponse.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();
                }
            }
        }
    }
}
