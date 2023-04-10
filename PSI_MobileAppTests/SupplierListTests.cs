using AngleSharp.Dom;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MudBlazor;
using MudBlazor.Services;
using Newtonsoft.Json.Schema;
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
    public class SupplierListTests
    {
        [Fact]
        public void TableShouldWork()
        {
            var mockAPI = new Mock<IGetData>();
            mockAPI.Setup(i => i.GetDistributorProfiles()).ReturnsAsync(GetProfiles());
            mockAPI.Setup(i => i.GetAllDistributors()).ReturnsAsync(GetDistributors());
            mockAPI.Setup(i => i.GetDistributorsById(Guid.Parse("199d3103-f222-44de-b48f-05b412971a1e"))).ReturnsAsync(GetDistributorById(Guid.Parse("199d3103-f222-44de-b48f-05b412971a1e")));
            mockAPI.Setup(i => i.TestConnection()).ReturnsAsync(true);
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

            var comp = Context.RenderComponent<SupplierList>();
            Thread.Sleep(1000);
            var table = comp.FindAll("tr");
            Assert.NotNull(table);
            var tableRow = table[1];
            Assert.NotNull(tableRow);
            Assert.Contains("Test1", tableRow.TextContent);
            Assert.Contains("testPhoneNumber", tableRow.TextContent);
            Assert.Contains("FastFood", tableRow.TextContent);
            var tableRowRating = comp.FindComponent<MudRating>();
            Assert.NotNull(tableRowRating);
            Assert.Equal(5, tableRowRating.Instance.SelectedValue);
        }
        [Fact]
        public void LoadingShouldWork()
        {
            var mockAPI = new Mock<IGetData>();
            mockAPI.Setup(i => i.GetDistributorProfiles()).ReturnsAsync(GetProfiles());
            mockAPI.Setup(i => i.GetAllDistributors()).ReturnsAsync(GetDistributors());
            mockAPI.Setup(i => i.GetDistributorsById(Guid.Parse("199d3103-f222-44de-b48f-05b412971a1e"))).ReturnsAsync(GetDistributorById(Guid.Parse("199d3103-f222-44de-b48f-05b412971a1e")));
            mockAPI.Setup(i => i.TestConnection()).ReturnsAsync(false);
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
            
            var comp = Context.RenderComponent<SupplierList>();
            var loading = comp.FindComponent<MudProgressCircular>();
            Assert.NotNull(loading);
            var loadingText = comp.FindAll("p");
            Assert.NotNull(loadingText);
            Assert.Contains("Loading...", loadingText[0].TextContent);

        }


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

        private static ObservableCollection<Distributor> GetDistributors()
        {
            return new ObservableCollection<Distributor>
            {
                new Distributor
                {
                    Id = Guid.Parse("199d3103-f222-44de-b48f-05b412971a1e"),
                    Rating = 5,
                    //RatingAmount = 100
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
                    //RatingAmount = 100
                };
        }
    }
}
