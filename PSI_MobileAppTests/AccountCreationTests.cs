using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bunit;
using System.Threading.Tasks;
using MudBlazor.Services;
using PSI_MobileApp.Pages;
using ClassLibrary;
using PSI_MobileApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using PSI_MobileApp.DataServices;
using Moq;
using System.Collections.ObjectModel;
using ProfileClasses;
using Microsoft.Identity.Client;

namespace PSI_MobileAppTests
{
	public class AccountCreationTests
	{
		[Fact]
		public async void UsernameValidationTest()
		{
            var mockAPI = new Mock<IGetData>();
			mockAPI.Setup(a => a.GetAllAccountsConcurrent()).Returns(GetAccountsConcurently());
            using var Context = new TestContext();
			var configuration = new MudServicesConfiguration();
			Context.Services.AddDbContextFactory<ProjectDatabaseContext>();
			Context.Services.AddScoped<StateContainer>();
            Context.Services.AddSingleton(mockAPI.Object);
            var comp = Context.RenderComponent<AccountCreation>();
			var form = comp.FindComponent<MudForm>().Instance;
			Assert.NotNull(form);
			var textFieldcomp = comp.FindComponents<MudTextField<string>>();
			var textField = textFieldcomp[0].Instance;
			Assert.False(textField.Error);
			textFieldcomp[0].Find("input").Change("a");
			Assert.True(textField.Error);
			textFieldcomp[0].Find("input").Change("DefinitelyNotInDB");
			Assert.False(textField.Error);

		}

		private ObservableCollection<Account> GetAccountsConcurently()
		{
			return new ObservableCollection<Account>
			{
				new Account{UserName="a", Password="pw", Id=Guid.NewGuid()}
			};
		}

		[Fact]
		public async void PasswordValidationTest()
		{
			using var Context = new TestContext();
			var configuration = new MudServicesConfiguration();
			Context.Services.AddDbContextFactory<ProjectDatabaseContext>();
			Context.Services.AddScoped<StateContainer>();
            Context.Services.AddSingleton<IGetData, GetData>();
            var comp = Context.RenderComponent<AccountCreation>();
			var form = comp.FindComponent<MudForm>().Instance;
			Assert.NotNull(form);
			var textFieldcomp = comp.FindComponents<MudTextField<string>>();
			var passwordField = textFieldcomp[1].Instance;
			var confirmField = textFieldcomp[2].Instance;
			Assert.NotNull(passwordField);
			Assert.NotNull(confirmField);
			Assert.False(passwordField.Error);
			Assert.False(confirmField.Error);
			textFieldcomp[1].Find("input").Change("passwordWithWhiteSpace ");
			Assert.True(passwordField.Error);
			textFieldcomp[1].Find("input").Change("passwordWithNoWhiteSpace");
			Assert.False(passwordField.Error);
			textFieldcomp[2].Find("input").Change("DefinitelyNotMatching");
			Assert.True(confirmField.Error);
			textFieldcomp[2].Find("input").Change("passwordWithNoWhiteSpace");
			Assert.False(confirmField.Error);
			Assert.False(passwordField.Error);
		}
		[Fact]
		public async void EmailValidationTest()
		{
			using var Context = new TestContext();
			var configuration = new MudServicesConfiguration();
			Context.Services.AddDbContextFactory<ProjectDatabaseContext>();
			Context.Services.AddScoped<StateContainer>();
            Context.Services.AddSingleton<IGetData, GetData>();
            var comp = Context.RenderComponent<AccountCreation>();
			var form = comp.FindComponent<MudForm>().Instance;
			Assert.NotNull(form);
			var textFieldcomp = comp.FindComponents<MudTextField<string>>();
			var textField = textFieldcomp[3].Instance;
			Assert.False(textField.Error);
			textFieldcomp[3].Find("input").Change("a");
			Assert.True(textField.Error);
			textFieldcomp[3].Find("input").Change("a@a.a");
			Assert.False(textField.Error);
		}



        [Fact]
        public void ChooseCuisineCheckTest()
        {
            using var Context = new TestContext();
            Context.JSInterop.Mode = JSRuntimeMode.Loose;
            var configuration = new MudServicesConfiguration();
            Context.Services.AddDbContextFactory<ProjectDatabaseContext>();
            Context.Services.AddScoped<StateContainer>();
            Context.Services.AddSingleton<IGetData, GetData>();
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
			var comp = Context.RenderComponent<ChooseCuisine>();
			var stateContainer = Context.Services.GetRequiredService<StateContainer>();
			stateContainer.TempProfile = new();
			Thread.Sleep(1000);
			comp.Instance.cuisines = new HashSet<string>() { };
			comp.Instance.Check();
			Assert.Contains(Cuisines.None, stateContainer.TempProfile.CuisineArray);

			HashSet<string> list = new HashSet<string>();
			list.Add("European");
            list.Add("Asian"); // possible expancsions later
            comp.Instance.cuisines = list;
			comp.Instance.Check();
            Assert.Contains(Cuisines.European, stateContainer.TempProfile.CuisineArray);
            Assert.Contains(Cuisines.Asian, stateContainer.TempProfile.CuisineArray);
        }
    }
}

