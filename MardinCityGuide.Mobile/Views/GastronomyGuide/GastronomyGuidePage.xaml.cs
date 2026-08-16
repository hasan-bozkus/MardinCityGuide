using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.Mobile.Helpers;

namespace MardinCityGuide.Mobile.Views.GastronomyGuide;

public partial class GastronomyGuidePage : ContentPage
{
	private readonly IPlaceService _placeService;

	public GastronomyGuidePage()
	{
		InitializeComponent();
		_placeService = ServiceHelper.GetService<IPlaceService>();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        GetHighsetStarredPlaceColleciton.BindingContext = await _placeService.TGetHighestStarredPlaceWithLocationAsync();
    }
}