using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ProfileClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSI_MobileApp.DataServices;
using Microsoft.Identity.Client;
// WARNING: this class contains a lot of unexpected behaviour with the lazy initialization. Therefore if your team wants to feel free to remove change it to eager.
namespace PSI_MobileApp.Containers
{
    public class CurrentUserContainer
    {
        private Guid _userId { get; set; } // stores the id of the current logged in user.
        private Lazy<Account> _userAccount { get; set; } // stores the account data (if initialized) of the current logged in user.
        private Lazy<Profile> _userProfile { get; set; } // stores the profile data (if initialized) of the current logged in user.
        private GetData getData = new();
        private Lazy<Distributor> _userDistributor { get; set; } // stores the distributor (if a distributor) data (if initialized) of the current logged in user.
        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public Account GetUserAccount()
        {
            return _userAccount.Value;
        }
        public Profile GetUserProfile()
        {
            return _userProfile.Value;
        }
        public Distributor GetUserDistributor()
        {
            return _userDistributor.Value;
        }
        public Account GetAccountFromWeb()
        {
            return getData.GetAccountByIdConcurrent(UserId);
        }
        public Distributor GetDistributorFromWeb()
        {
            return getData.GetDistributorsByIdConcurrent(UserId);
        }
        public Profile GetProfileFromWeb()
        {
            return getData.GetProfileByIdConcurrent(UserId);
        }
        public void Logout()
        {
            UserId = Guid.Empty;
            _userDistributor = new Lazy<Distributor>(delegate () { return GetDistributorFromWeb(); });
            _userAccount = new Lazy<Account>(delegate () { return GetAccountFromWeb(); });
            _userProfile = new Lazy<Profile>(delegate () { return GetProfileFromWeb(); });
        }

        public CurrentUserContainer()
        {
            UserId = Guid.Empty;
            _userDistributor = new Lazy<Distributor>(delegate () { return GetDistributorFromWeb(); });
            _userAccount = new Lazy<Account>(delegate () { return GetAccountFromWeb(); });
            _userProfile = new Lazy<Profile>(delegate () { return GetProfileFromWeb(); });

        }
        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }


}