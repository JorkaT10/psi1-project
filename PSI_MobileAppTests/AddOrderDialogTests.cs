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

            comp.FindComponent<MudDatePicker>().SetParametersAndRender(parameters => parameters
            .Add(p => p.Date, DateTime.SpecifyKind(DateTime.UtcNow.Date, DateTimeKind.Unspecified).AddDays(1)));
            var datePicker = comp.FindComponent<MudDatePicker>().Instance;
            Assert.Equal(DateTime.SpecifyKind(DateTime.UtcNow.Date, DateTimeKind.Unspecified).AddDays(1),
                            datePicker.Date);

            comp.FindAll("button").Where(b => b.TextContent == "Ok").First().Click();

            var rv = await dialogReference.GetReturnValueAsync<Profile>();
            Assert.Null(rv);

        }

        [Theory]
        [InlineData("0.00", 0.00)]
        [InlineData("-5.00", -5.00)]
        public async void SubmitPriceValidationShouldWork(string price, double expected)
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
            comp.FindAll("input")[1].Input(new ChangeEventArgs() { Value = price }); ;
            var secondInput = comp.FindComponent<MudNumericField<double>>();
            Assert.Equal(expected, secondInput.Instance.Value);

            comp.FindAll("div.mud-picker-stick-inner.mud-hour")[10].Click();
            comp.FindAll("div.mud-minute")[30].Click();
            var picker = comp.FindComponent<MudTimePicker>().Instance;
            Assert.Equal(new TimeSpan(11, 30, 0), picker.Time);

            comp.FindComponent<MudDatePicker>().SetParametersAndRender(parameters => parameters
           .Add(p => p.Date, DateTime.SpecifyKind(DateTime.UtcNow.Date, DateTimeKind.Unspecified).AddDays(1)));
            var datePicker = comp.FindComponent<MudDatePicker>().Instance;
            Assert.Equal(DateTime.SpecifyKind(DateTime.UtcNow.Date, DateTimeKind.Unspecified).AddDays(1),
                            datePicker.Date);

            comp.FindAll("button").Where(b => b.TextContent == "Ok").First().Click();

            Assert.Equal("The price needs to be Higher then 0.",
                        comp.FindAll("p.mud-error-text")[0].TextContent);

        }

        [Fact]
        public async void SubmitDateValidationShouldWork()
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

            comp.FindComponent<MudDatePicker>().SetParametersAndRender(parameters => parameters
           .Add(p => p.Date, new DateTime(2002-05-23)));
            var datePicker = comp.FindComponent<MudDatePicker>().Instance;
            Assert.Equal(new DateTime(2002-05-23), datePicker.Date);

            comp.FindAll("button").Where(b => b.TextContent == "Ok").First().Click();


            Assert.Equal("The date and time needs to be selected and Higher then current time.",
                            comp.FindAll("p.mud-error-text")[1].TextContent);

        }

    }
}
