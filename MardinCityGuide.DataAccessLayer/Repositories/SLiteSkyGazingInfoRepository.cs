using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using SQLite;
using SunCalcNet;
using SunCalcNet.Model;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteSkyGazingInfoRepository : GenericRepository<SkyGazingInfo>, ISkyGazingInfoDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;
        private readonly HttpClient _httpClient = new HttpClient();

        public SLiteSkyGazingInfoRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task<SkyGazingInfo> LoadSkyGazingDataForMardinAsync()
        {
            try
            {
                // Mardin Koordinatları
                double latitude = 37.3129;
                double longitude = 40.7350;

                // İnternet veya dış servis olmadan doğrudan astronomik matematiksel hesaplama
                var sunPhases = SunCalc.GetSunPhases(DateTime.Now, latitude, longitude);

                // SunCalc UTC döner, ToLocalTime() ile Türkiye saatine çeviriyoruz
                var goldenHourPhase = sunPhases.FirstOrDefault(x => x.Name.Value == SunPhaseName.GoldenHour.Value);
                var sunsetPhase = sunPhases.FirstOrDefault(x => x.Name.Value == SunPhaseName.Sunset.Value);

                DateTime peakStart = goldenHourPhase != null ? goldenHourPhase.PhaseTime.ToLocalTime() : DateTime.Today.Add(new TimeSpan(18, 42, 0));

                DateTime peakEnd = sunsetPhase != null
                    ? sunsetPhase.PhaseTime.ToLocalTime()
                    : DateTime.Today.Add(new TimeSpan(19, 15, 0));

                return new SkyGazingInfo()
                {
                    ForMonth = DateTime.Today,
                    PeakStart = peakStart,
                    PeakEnd = peakEnd,
                    VisibilityLabel = "Mükemmel Görüş",
                    ForecastUrl = "https://www.mgm.gov.tr/tahmin/il-ve-ilceler.aspx?il=Mardin",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                throw new NotImplementedException($"SkyGazing API Hata: {ex.Message}");
            }

            throw new NotImplementedException();
        }
    }

    public class SunTimesResponseDto
    {
        public string GoldenHourEvening { get; set; } = string.Empty;
        public string Sunset { get; set; } = string.Empty;
    }
}
