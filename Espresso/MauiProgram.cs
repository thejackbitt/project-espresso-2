using Microsoft.Extensions.Logging;

namespace Espresso
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("W95FA.otf", "W95FA");
                });

            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}
