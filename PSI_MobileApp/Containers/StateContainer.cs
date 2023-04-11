using ProfileClasses;

namespace PSI_MobileApp.Containers
{
    public class StateContainer
    {
        private Profile profile;
        private bool creatingDistributor = false; // bool used to store whether the current user wants to create a new distributor profile or customer profile
        private Profile tempProfile; // a temporary profile used to store data of a profile in the middle of creation.
        private Account tempAccount; // a temporary account used to store data of a account in the middle of creation.

        public bool CreatingDistributor
        {
            get { return creatingDistributor; }
            set { creatingDistributor = value; }
        }
        public Account TempAccount
        {
            get => tempAccount;
            set
            {
                tempAccount = value;
            }
        }
        public Profile TempProfile
        {
            get => tempProfile;
            set
            {
                tempProfile = value;
            }
        }
        public Profile Supplier
        {
            get => profile;
            set
            {
                profile = value;
                NotifyStateChanged();
            }
        }

#nullable enable
        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
