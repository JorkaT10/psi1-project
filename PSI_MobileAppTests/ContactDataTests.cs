using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bunit;
using System.Threading.Tasks;
using MudBlazor.Services;
using PSI_MobileApp.Pages;
using ClassLibrary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using PSI_MobileApp.DataServices;
using Moq;
using System.Collections.ObjectModel;
using ProfileClasses;
using Microsoft.Identity.Client;
using static MudBlazor.CategoryTypes;
using PSI_MobileApp.Containers;

namespace PSI_MobileAppTests
{
    public class ContactDataTests
	{
		[Fact]
		public async void ContactDetails_HandleValidSubmit_ChangesValuesCorrectly()
		{
			using var Context = new TestContext();
			var configuration = new MudServicesConfiguration();
			Context.Services.AddScoped<StateContainer>();
			Context.Services.AddScoped<CurrentUserContainer>();
			var stateContainer = Context.Services.GetRequiredService<StateContainer>();
			var currentUserContainer = Context.Services.GetRequiredService<CurrentUserContainer>();
			currentUserContainer.UserId = Guid.NewGuid();
			stateContainer.CreatingDistributor = false;
			Profile profile = await GetProfileById(currentUserContainer.UserId);
			ContactData contactData = new();
			int? StreetNumber = null;
			string? StreetName = "naujas pavadinimas";
			string? City = null;
			string? Phone = "+6";
			Address address = new();
			address.StreetNumber = StreetNumber == null ? profile.TypedAddress.StreetNumber : (int)StreetNumber;
			address.StreetName = StreetName == null ? profile.TypedAddress.StreetName : StreetName;
			address.City = City == null ? profile.TypedAddress.City : City;
			Phone ??= profile.PhoneNumber;
			Assert.Equal(StreetName, address.StreetName);
			Assert.Equal(address.City, profile.TypedAddress.City);
			Assert.Equal(address.StreetNumber, profile.TypedAddress.StreetNumber);
			Assert.NotEqual(Phone, profile.PhoneNumber);
		}

		private async Task<Profile> GetProfileById(Guid id)
		{
			var Profile = new Profile { Name = "test", PhoneNumber = "+123", TypedAddress = new(), Email = "a@a.a", Id = id };
			return Profile;
		}
	}
}
