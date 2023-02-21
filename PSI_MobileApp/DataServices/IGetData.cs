using ClassLibrary;
using ProfileClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PSI_MobileApp.DataServices
{
    public interface IGetData // interface which contains methods relating to communication with the database. Interface is used in order to use dependency injection
    {
        Task<ObservableCollection<Profile>> GetAllProfiles();
        Task<ObservableCollection<Profile>> GetDistributorProfiles();
        Task<Profile> GetProfileById(Guid? id);
        Task<ObservableCollection<Account>> GetAllAccounts();
        Task<Account> GetAccountById(Guid id);

        Task<ObservableCollection<Advertisement>> GetAllAdvertisements();
        Task<Advertisement> GetAdvertisementsById(Guid id);
        Task<ObservableCollection<Advertisement>> GetAdsByDistributorId(Guid id);
        Task<ObservableCollection<Advertisement>> GetAdsByBuyerId(Guid id);
        Task<ObservableCollection<Distributor>> GetAllDistributors();
        Task<Distributor> GetDistributorsById(Guid id);
        Distributor GetDistributorsByIdConcurrent(Guid id);
        Task AddDistributor(Guid id);
        Task AddNewProfile(Profile profile);
        Task AddNewAccount(Account account);
        Task AddAd(Advertisement advertisement, Guid id);
        Task RemoveOutdated(DateTime now);
        Task RemoveAdvertisement(Guid Id);
        Task ChangeOrderStatus(Guid advertisementId, Guid id);
        Task<Guid> GetAccount(string username, string password);
        Task UpdateProfile();
        Task ChangeSubscriptionStatus(Guid distributorId, Guid subscriberId);
        ObservableCollection<Account> GetAllAccountsConcurrent();
        Task<bool> TestConnection();
        Task ChangeContactData(Address address, string name, string phone, Guid id);
    }
}
