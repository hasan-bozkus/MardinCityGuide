using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.Mobile.Helpers;

namespace MardinCityGuide.Mobile.Views.SeazonalEvents;

public partial class SeazonalEventsPage : ContentPage
{
	private readonly ISkyGazingInfoService _skyGazingInfoService;
	private readonly ISeasonalSpotService _seasonalSpotService;
	private readonly ICulturalEventService _culturalEventService;

	public SeazonalEventsPage()
	{
		InitializeComponent();
		_skyGazingInfoService = ServiceHelper.GetService<ISkyGazingInfoService>();
		_seasonalSpotService = ServiceHelper.GetService<ISeasonalSpotService>();
		_culturalEventService = ServiceHelper.GetService<ICulturalEventService>();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

		var skyGazingInfoForToday = await _skyGazingInfoService.TLoadSkyGazingDataForMardinAsync();
		SkyGazingInfoForToday.BindingContext = skyGazingInfoForToday;

		var randomReligiousSiteList = await _seasonalSpotService.TGetRandom2ReligiousListAsync();
		RandomReligiousSiteList.BindingContext = randomReligiousSiteList;

        var upcoming4EventsWithImage = await _culturalEventService.TGetUpcoming4EventsWithImageAsync();
        Upcoming4EventsWithImage.ItemsSource = upcoming4EventsWithImage.Take(8).ToList();

		var getUpcoming4Events = await _culturalEventService.TGetUpcoming4EventsWithAsync();
		GetUpcoming4EventsCollection.ItemsSource = getUpcoming4Events;
    }
}