using Bunit;
using ClassLibrary;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MudBlazor;
using MudBlazor.Services;
using ProfileClasses;
using PSI_MobileApp.Containers;
using PSI_MobileApp.DataServices;
using PSI_MobileApp.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_MobileAppTests
{
    public class SupplierDetailTests
    {

        /*[Fact]
        public void SupplierDetailsShouldRender()
        {
            var mockAPI = new Mock<IGetData>();
            mockAPI.Setup(a => a.TestConnection()).ReturnsAsync(true);
            mockAPI.Setup(a => a.GetProfileById(Guid.Parse("199d3103-f222-44de-b48f-05b412971a1e"))).ReturnsAsync(GetProfile());
            mockAPI.Setup(a => a.GetAdsByDistributorId(Guid.Parse("199d3103-f222-44de-b48f-05b412971a1e"))).ReturnsAsync(GetAdvertisements());
            using var Context = new TestContext();
            var configuration = new MudServicesConfiguration();
            Context.Services.AddMudBlazorDialog()
                .AddMudBlazorSnackbar(configuration.SnackbarConfiguration)
                .AddMudBlazorResizeListener(configuration.ResizeOptions)
                .AddMudBlazorResizeObserver(configuration.ResizeObserverOptions)
                .AddMudBlazorResizeObserverFactory()
                .AddMudBlazorKeyInterceptor()
                .AddMudBlazorJsEvent()
                .AddMudBlazorScrollManager()
                .AddMudBlazorScrollListener()
                .AddMudBlazorJsApi()
                .AddMudBlazorScrollSpy()
                .AddMudPopoverService(configuration.PopoverOptions)
                .AddMudEventManager()
                .AddSingleton(mockAPI.Object)
                .AddScoped<StateContainer>()
                .AddScoped<IdStateContainer>()
                .AddScoped<CurrentUserContainer>();
            var idContainer = Context.Services.GetRequiredService<IdStateContainer>();
            idContainer.Id = Guid.Parse("199d3103-f222-44de-b48f-05b412971a1e");
            var comp = Context.RenderComponent<SupplierDetails>();
            var texts = comp.FindComponents<MudText>();
            Assert.Contains("Test1", texts[1].Nodes.First().TextContent);
            Assert.Contains("test@test.test", texts[2].Nodes.First().TextContent);
            Assert.Contains("testPhoneNumber", texts[3].Nodes.First().TextContent);
            Assert.Contains("testC, Testing st. 555", texts[4].Nodes.First().TextContent);
            Assert.Contains("FastFood", texts[5].Nodes.First().TextContent);

        }*/

        private static ObservableCollection<Profile> GetProfiles()
        {
            return new ObservableCollection<Profile>
            {
                new Profile
                {
                    Id=Guid.Parse("199d3103-f222-44de-b48f-05b412971a1e"),
                    Name= "Test1",
                    TypedAddress= new ClassLibrary.Address() {City="testC",StreetName="Testing",StreetNumber=555},
                    PhoneNumber = "testPhoneNumber",
                    Email="test@test.test",
                    Subscriptions= null,
                    CuisineArray= new ClassLibrary.Cuisines[] {ClassLibrary.Cuisines.FastFood}

                }
            };
        }

        private static ObservableCollection<Advertisement> GetAdvertisements()
        {
            return new ObservableCollection<Advertisement> 
            { 
                 
            };
        }

        private static Profile GetProfile()
        {
            return new Profile
            {
                Id = Guid.Parse("199d3103-f222-44de-b48f-05b412971a1e"),
                Name = "Test1",
                TypedAddress = new ClassLibrary.Address() { City = "testC", StreetName = "Testing", StreetNumber = 555 },
                PhoneNumber = "testPhoneNumber",
                Email = "test@test.test",
                Subscriptions = null,
                CuisineArray = new ClassLibrary.Cuisines[] { ClassLibrary.Cuisines.FastFood }

            };
        }
        private static ObservableCollection<Distributor> GetDistributors()
        {
            return new ObservableCollection<Distributor>
            {
                new Distributor
                {
                    Id = Guid.Parse("199d3103-f222-44de-b48f-05b412971a1e"),
                    Rating = 5,
                    RatingAmount = 100
                }
            };
        }

        private static Distributor GetDistributorById(Guid i)
        {
            return
                new Distributor
                {
                    Id = Guid.Parse("199d3103-f222-44de-b48f-05b412971a1e"),
                    Rating = 5,
                    RatingAmount = 100
                };
        }
    }
}
