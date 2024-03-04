using Microsoft.Extensions.Logging;
using NewAppTrivia.Services;
using NewAppTrivia.ViewModels;
using NewAppTrivia.Views;

namespace NewAppTrivia
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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Sweet Honey.otf", "SweetHoney");
                });

            //register services
            builder.Services.AddSingleton<TriviaServices>();

            //register viewmodels
            builder.Services.AddTransient<ApproveQuestionsPageViewModel>();
            builder.Services.AddTransient<BestScoresPageViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<UserAdminPageViewModel>();
            builder.Services.AddTransient<UserQuestionsPageViewModel>();

            //register views
            builder.Services.AddTransient<ApproveQuestionsPage>();
            builder.Services.AddTransient<BestScoresPage>();
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<UserAdminPage>();
            builder.Services.AddTransient<UserQuestionsPage>();




#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}