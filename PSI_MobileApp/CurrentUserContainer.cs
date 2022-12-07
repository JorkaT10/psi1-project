using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ProfileClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PSI_MobileApp
{
    public class CurrentUserContainer
    {
        private Guid? _userId { get; set; }
        private Lazy<Account> _userAccount { get; set; }
        private Lazy<Profile> _userProfile { get; set; }
        private Lazy<Distributor> _userDistributor { get; set; }
        public Guid? UserId
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
        public Account GetAccountFromDB(ExceptionLogger logger, ProjectDatabaseContext context)
        {
            try
            {
                return context.Accounts.Where(profile => profile.Id == this.UserId).FirstOrDefault();
            }
            catch(Exception ex)
            {
                logger.Log(ex);
                return null;
            }
        }

        public Profile GetProfileFromDB(ExceptionLogger logger, ProjectDatabaseContext context)
        {
            try
            {
                return context.Profiles.Where(profile => profile.Id == this.UserId).FirstOrDefault();
            }
            catch(Exception ex)
            {
                logger.Log(ex);
                return null;
            }
        }
        public Distributor GetDistributorFromDB(ExceptionLogger logger, ProjectDatabaseContext context)
        {
            try
            {
                return context.Distributors.Where(profile => profile.Id == this.UserId).FirstOrDefault();
            }
            catch(Exception ex)
            {
                logger.Log(ex);
                return null;
            }
        }

        public void Logout(ExceptionLogger logger, ProjectDatabaseContext context)
        {
            UserId = null;
            _userDistributor = new Lazy<Distributor>(delegate () { return GetDistributorFromDB(logger, context); });
            _userAccount = new Lazy<Account>(delegate () { return GetAccountFromDB(logger, context); });
            _userProfile = new Lazy<Profile>(delegate () { return GetProfileFromDB(logger, context); });
        }
        public void RefreshProfile(ExceptionLogger logger, ProjectDatabaseContext context)
        {
            _userProfile = new Lazy<Profile>(delegate () { return GetProfileFromDB(logger, context); });
        }

        public CurrentUserContainer(ExceptionLogger logger, ProjectDatabaseContext context)
        {
            this._userAccount = new Lazy<Account>(delegate () { return GetAccountFromDB(logger, context); });
            this._userProfile = new Lazy<Profile>(delegate () { return GetProfileFromDB(logger, context); });
            this._userDistributor = new Lazy<Distributor>(delegate () { return GetDistributorFromDB(logger, context); });

        }
        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }


}