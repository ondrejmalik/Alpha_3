using Microsoft.Extensions.Logging;
using MauiApp1.Data;

namespace MauiApp1;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("TorusPro-Regular.ttf", "TorusProRegular"); });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug(); 
#endif
        builder.Services.AddSingleton<DBOService>();

        return builder.Build();
    }
}