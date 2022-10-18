using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using ProfileClasses;


namespace PSI_MobileApp.ViewModels
{
    public class SupplierPageViewModel : ObservableObject
    {
        static bool isRuning;

        public ObservableCollection<Profile> Profiles
        {
            get; private set;
        }

        public SupplierPageViewModel()
        {
            Address testAddress = new() {city="Vilnius", streetName="Didlaukio g.", streetNumber=47 };
            Profiles = new ObservableCollection<Profile>()
            {
                new Profile{Email = "test1", PhoneNumber = "test12", Name = "Name1", Rating = 4, Address=testAddress},
                new Profile{Email = "test2", PhoneNumber = "test12", Name = "Name1", Rating = 4},
                new Profile{Email = "test3", PhoneNumber = "test12", Name = "Name1", Rating = 4.0},
                new Profile{Email = "test4", PhoneNumber = "test12", Name = "Name1", Rating = 4.0},
                new Profile{Email = "test5", PhoneNumber = "test12", Name = "Name1", Rating = 4.0},
                new Profile{Email = "test6", PhoneNumber = "test12", Name = "Name1", Rating = 4.0},
                new Profile{Email = "test7", PhoneNumber = "test12", Name = "Name1", Rating = 4.0},
                new Profile{Email = "test8", PhoneNumber = "test12", Name = "Name1", Rating = 4.0},
                new Profile{Email = "test9", PhoneNumber = "test12", Name = "Name1", Rating = 4.0},
                new Profile{Email = "test10", PhoneNumber = "test12", Name = "Name1", Rating = 4.0},
                
            };
            TimeSpanHandler();
        }


        async void TimeSpanHandler()
        {

            await Task.Run(async () =>
            {
                if (!isRuning)
                {
                    var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
                    isRuning = true;
                    while (await timer.WaitForNextTickAsync())
                    {
                        foreach (var j in Profiles)
                        {
                            if (j.Advertisements == null)
                                j.Advertisements = new();

                            foreach (var i in j.Advertisements.ToList())
                            {
                                if (i.PickupTimeSpan.Equals(TimeSpan.Zero))
                                {
                                    j.Advertisements.Remove(i);
                                }
                                else
                                {
                                    i.PickupTimeSpan = i.PickupTimeSpan.Subtract(TimeSpan.FromSeconds(1));
                                }


                            }
                            
                        }
                    }
                }
            });
        }

    }



}
