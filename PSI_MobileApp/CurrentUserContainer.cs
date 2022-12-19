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

namespace PSI_MobileApp
{
    public class CurrentUserContainer
    {
        private Guid _userId { get; set; }
        private Lazy<Account> _userAccount { get; set; }
        private Lazy<Profile> _userProfile { get; set; }
        private GetData getData = new();
        private Lazy<Distributor> _userDistributor { get; set; }
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
            _userAccount = new Lazy<Account>(delegate ()  { return GetAccountFromWeb(); }) ;
            _userProfile = new Lazy<Profile>(delegate () { return GetProfileFromWeb(); });

        }
        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }


}