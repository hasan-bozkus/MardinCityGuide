using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.BusinessLayer.Concrete;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.DataAccessLayer.Repositories;
using MardinCityGuide.Mobile.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Handlers.Items;
using Microsoft.Maui.Storage;
using SQLite;
using System.Net.Http.Json;
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
                .UseMauiMaps()
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

            var connectionStrings = new SQLiteConnectionString(dbPath, storeDateTimeAsTicks: true);

            // 2. SQLite Bağlantısını DI Container'a Singleton Olarak Kaydetme
            builder.Services.AddSingleton(s => new SQLiteAsyncConnection(connectionStrings));

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

            builder.Services.AddScoped<IPlaceDal, SLitePlaceRepository>();
            builder.Services.AddScoped<IPlaceService, PlaceManager>();

            builder.Services.AddScoped<IEditorialHighlightDal, SLiteEditorialHighlightRepository>();
            builder.Services.AddScoped<IEditorialHighlightService, EditorialHighlightManager>();

            builder.Services.AddScoped<IBazaarDal, SLiteBazaarRepository>();
            builder.Services.AddScoped<IBazaarService, BazaarManager>();

            builder.Services.AddScoped<IArtisanCraftDal, SLiteArtisanCraftRepository>();
            builder.Services.AddScoped<IArtisanCraftService, ArtisanCraftManager>();

            builder.Services.AddScoped<IMuseumDal, SLiteMuseumRepository>();
            builder.Services.AddScoped<IMuseumService, MuseumManager>();

            builder.Services.AddScoped<IReligiousSiteDal, SLiteReligiousSiteRepository>();
            builder.Services.AddScoped<IReligiousSiteService, ReligiousSiteManager>();

            builder.Services.AddScoped<IHistoricalSiteDal, SLiteHistoricalSiteRepository>();
            builder.Services.AddScoped<IHistoricalSiteService, HistoricalSiteManager>();

            builder.Services.AddScoped<ICulturalEventDal, SLiteCulturalEventRepository>();
            builder.Services.AddScoped<ICulturalEventService, CulturalEventManager>();

            builder.Services.AddScoped<IFavoriteDal, SLiteFavoriteRepository>();
            builder.Services.AddScoped<IFavoriteService, FavoriteManager>();

            builder.Services.AddScoped<ISkyGazingInfoDal, SLiteSkyGazingInfoRepository>();
            builder.Services.AddScoped<ISkyGazingInfoService, SkyGazingInfoManager>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            ServiceHelper.Initialize(app.Services);

            return app;
        }
    }
}
