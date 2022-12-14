using ClassLibrary;
using ProfileClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_MobileApp.DataServices
{
    public interface IGetData
    {
        Task<ObservableCollection<Profile>> GetAllProfiles();
        Task<ObservableCollection<Profile>> GetDistributorProfiles();
        Task<Profile> GetProfileById(Guid? id);
        Task<ObservableCollection<Account>> GetAllAccounts();
        Task<ObservableCollection<Advertisement>> GetAllAdvertisements();
        Task<Advertisement> GetAdvertisementsById(Guid id);
        Task<ObservableCollection<Advertisement>> GetAdsByDistributorId(Guid id);
        Task<ObservableCollection<Advertisement>> GetAdsByBuyerId(Guid id);
        Task<ObservableCollection<Distributor>> GetAllDistributors();
        Task<Distributor> GetDistributorsById(Guid id);
        Distributor GetDistributorsByIdConcurrent(Guid id);
        Task AddDistributor(Distributor distributor);
        Task AddNewProfile(Profile profile);
        Task AddNewAccount(Account account);

    }
}
