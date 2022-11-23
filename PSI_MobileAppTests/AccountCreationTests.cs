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

namespace PSI_MobileAppTests
{
	public class AccountCreationTests
	{
		[Fact]
		public async void UsernameValidationTest()
		{
			using var Context = new TestContext();
			var configuration = new MudServicesConfiguration();
			Context.Services.AddDbContextFactory<ProjectDatabaseContext>();
			Context.Services.AddScoped<StateContainer>();
			Context.Services.AddScoped<ExceptionLogger>();
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
		[Fact]
		public async void PasswordValidationTest()
		{
			using var Context = new TestContext();
			var configuration = new MudServicesConfiguration();
			Context.Services.AddDbContextFactory<ProjectDatabaseContext>();
			Context.Services.AddScoped<StateContainer>();
			Context.Services.AddScoped<ExceptionLogger>();
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
			Context.Services.AddScoped<ExceptionLogger>();
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
	}
}

