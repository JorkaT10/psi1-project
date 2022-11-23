using AngleSharp;
using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;
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

        [Fact]
        public async void SubmitShouldWork()
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
            await comp.InvokeAsync(() => { dialogReference = service?.Show<PSI_MobileApp.Pages.AddOrderDialog>(); });
            Assert.NotNull(dialogReference);

            comp.FindAll("input")[0].Change("Test");
            var firstInput = comp.FindComponent<MudTextField<string>>();
            Assert.Equal("Test", firstInput.Instance.Value);
            comp.FindAll("input")[1].Input(new ChangeEventArgs() { Value = "6.25" }); ;
            var secondInput = comp.FindComponent<MudNumericField<double>>();
            Assert.Equal(6.25, secondInput.Instance.Value);

            comp.FindAll("div.mud-picker-stick-inner.mud-hour")[10].Click();
            comp.FindAll("div.mud-minute")[30].Click();
            var picker = comp.FindComponent<MudTimePicker>().Instance;
            Assert.Equal(new TimeSpan(11, 30, 0), picker.Time);

            //var date = DateTime.Now.AddDays(1).Day.ToString();

            var date = comp.FindAll("button.mud-picker-calendar-day");
            comp.FindAll("button.mud-picker-calendar-day")
                .Where(x => x.TextContent.Equals("25")).First().Click();
            var datePicker = comp.FindComponent<MudDatePicker>().Instance;
            Assert.Equal(new DateTime(2022, 11, 25), datePicker.Date);

            comp.FindAll("button").Where(b => b.TextContent == "Ok").First().Click();

            var rv = await dialogReference.GetReturnValueAsync<Profile>();
            Assert.Null(rv);

        }


    }
}
