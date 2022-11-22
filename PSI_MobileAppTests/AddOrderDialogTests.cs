using AngleSharp;
using AngleSharp.Dom;
using Bunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using ProfileClasses;
using PSI_MobileApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_MobileAppTests
{
    public class AddOrderDialogTests 
    {

        [Fact]
        public async void CancelShouldWork()
        {
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
                .AddMudEventManager();

            var comp = Context.RenderComponent<MudDialogProvider>();
            var service = Context.Services.GetService<IDialogService>() as DialogService;
            Assert.NotNull(service);

            IDialogReference dialogReference = null;
            await comp.InvokeAsync(() => {dialogReference = service?.Show<PSI_MobileApp.Pages.AddOrderDialog>();});
            Assert.NotNull(dialogReference);


            comp.FindAll("button").Where(b => b.TextContent == "Cancel").First().Click();

            var rv = await dialogReference.GetReturnValueAsync<Profile>();
            Assert.Null(rv);
            // on hold
            // Assert.Equal(2, comp.FindComponents<MudButton>().Count);
        }
    


    }
}
