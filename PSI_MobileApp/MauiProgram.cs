﻿using ClassLibrary;
using GoogleMapsComponents;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using PSI_MobileApp.Containers;
using PSI_MobileApp.DataServices;
using PSI_MobileApp.Pages;

namespace PSI_MobileApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});
		builder.Services.AddMauiBlazorWebView();
        builder.Services.AddMudServices();
		builder.Services.AddBlazorGoogleMaps();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif
		builder.Services.AddScoped<StateContainer>();
        builder.Services.AddScoped<IdStateContainer>();
		builder.Services.AddScoped<CurrentUserContainer>();
		builder.Services.AddSingleton<IGetData, GetData>();
		return builder.Build();
	}
}
