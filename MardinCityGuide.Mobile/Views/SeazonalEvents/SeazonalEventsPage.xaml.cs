using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.Mobile.Helpers;

namespace MardinCityGuide.Mobile.Views.SeazonalEvents;

public partial class SeazonalEventsPage : ContentPage
{
	private readonly ISkyGazingInfoService _skyGazingInfoService;

	public SeazonalEventsPage()
	{
		InitializeComponent();
		_skyGazingInfoService = ServiceHelper.GetService<ISkyGazingInfoService>();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

		var values = await _skyGazingInfoService.TLoadSkyGazingDataForMardinAsync();
		//SkyGazingInfoForToday.BindingContext = valuesa;
    }
}