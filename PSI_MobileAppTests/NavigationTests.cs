using Moq;
using MudBlazor.Services;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSI_MobileApp;
using PSI_MobileApp.DataServices;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using PSI_MobileApp.Pages;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components;

namespace PSI_MobileAppTests
{
    public class NavigationTests
    {
        [Fact]
        public void CreateAccountTypeNavigationButtonsShouldWork()
        {
            var mockAPI = new Mock<IGetData>();
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
            var navMan = Context.Services.GetRequiredService<NavigationManager>();
            var stateContainer = Context.Services.GetRequiredService<StateContainer>();
            var comp = Context.RenderComponent<ChooseAccountType>();
            var buttons = comp.FindAll("button");
            Assert.NotNull(buttons);
            Assert.NotNull(buttons.Where(b => b.TextContent == "Customer").First());
            comp.FindAll("button").Where(b => b.TextContent == "Customer").First().Click();
            Assert.Equal("http://localhost/usercreateaccount", navMan.Uri);
            Assert.False(stateContainer.CreatingDistributor);

            Assert.NotNull(buttons.Where(b => b.TextContent == "Supplier").Last());
            comp.FindAll("button").Where(b => b.TextContent == "Supplier").Last().Click();
            Assert.Equal("http://localhost/usercreateaccount", navMan.Uri);
            Assert.True(stateContainer.CreatingDistributor);

        }



    }
}
