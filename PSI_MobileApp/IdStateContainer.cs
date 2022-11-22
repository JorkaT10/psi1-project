namespace PSI_MobileApp
{
    internal class IdStateContainer
    {
        private Guid id;
        public Guid Id
        {
            get => id;
            set { id = value; NotifyStateChanged(); }
        }


#nullable enable
        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }


}
