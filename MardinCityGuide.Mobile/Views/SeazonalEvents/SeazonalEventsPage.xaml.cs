using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.Mobile.Helpers;

namespace MardinCityGuide.Mobile.Views.SeazonalEvents;

public partial class SeazonalEventsPage : ContentPage
{
	private readonly ISkyGazingInfoService _skyGazingInfoService;
	private readonly ISeasonalSpotService _seasonalSpotService;

	public SeazonalEventsPage()
	{
		InitializeComponent();
		_skyGazingInfoService = ServiceHelper.GetService<ISkyGazingInfoService>();
		_seasonalSpotService = ServiceHelper.GetService<ISeasonalSpotService>();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

		var skyGazingInfoForToday = await _skyGazingInfoService.TLoadSkyGazingDataForMardinAsync();
		SkyGazingInfoForToday.BindingContext = skyGazingInfoForToday;

		var values = await _seasonalSpotService.TGetRandom2ReligiousListAsync();
    }
}