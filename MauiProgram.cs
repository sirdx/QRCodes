using CommunityToolkit.Maui;
using MauiIcons.Fluent;
using Microsoft.Extensions.Logging;
using QRCodes.Data;
using QRCodes.ViewModels;
using QRCodes.Views;

namespace QRCodes;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseFluentMauiIcons()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<DatabaseContext>();

        builder.Services.AddTransient<CreatorViewModel>();
        builder.Services.AddTransient<CreatorPage>();
        builder.Services.AddTransient<ListViewModel>();
        builder.Services.AddTransient<ListPage>();

        return builder.Build();
    }
}