using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.BusinessLayer.Concrete;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.DataAccessLayer.Repositories;
using MardinCityGuide.Mobile.Helpers;
using MardinCityGuide.Mobile.Views.Home;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Storage;
using SQLite;
using System;
using System.IO;
namespace MardinCityGuide.Mobile
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
                    fonts.AddFont("CustomFontTemplate.ttf", "ManropeBold");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("material-symbols-outlined-latin-400-normal.ttf", "MaterialSymbols");

                });

            // 1. Veritabanı Yolu Belirleme (Geliştirme Ortamı ve Canlı Ayrımı)
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MardinCityGuide.db");

            //// Android / iOS cihazlarda ve canlı ortamda (Release) cihazın güvenli klasörüne yazar
            //dbPath = Path.Combine(FileSystem.AppDataDirectory, "MardinCityGuide.db");

            // 2. SQLite Bağlantısını DI Container'a Singleton Olarak Kaydetme
            builder.Services.AddSingleton(s => new SQLiteAsyncConnection(dbPath));

            // 3. Generic Repository ve Service Yapılarının DI Container'a Eklenmesi
            builder.Services.AddScoped<AppDatabase>();
            builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));

            builder.Services.AddScoped<IHighlightDal, SLiteHighlightRepository>();
            builder.Services.AddScoped<IHighlightService, HighlightManager>();

            builder.Services.AddScoped<IHomeNavTileDal, SLiteHomeNavTileRepository>();
            builder.Services.AddScoped<IHomeNavTileService, HomeNavTileManager>();

            builder.Services.AddScoped<ICategoryDal, SLiteCategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryManager>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            ServiceHelper.Initialize(app.Services);

            return app;
        }
    }
}
