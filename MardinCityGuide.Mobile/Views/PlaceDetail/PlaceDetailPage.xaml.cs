using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.Mobile.Helpers;

namespace MardinCityGuide.Mobile.Views.PlaceDetail;

[QueryProperty(nameof(PlaceId), "id")]
public partial class PlaceDetailPage : ContentPage
{
    private readonly IPlaceService _placeService;
    public string PlaceId { get; set; } = string.Empty;

    public PlaceDetailPage()
    {
        InitializeComponent();
        _placeService = ServiceHelper.GetService<IPlaceService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (int.TryParse(PlaceId, out int id))
        {
            await LoadPlaceDetails(id);
        }
    }

    private async Task LoadPlaceDetails(int id)
    {
        var placeDetail = await _placeService.TGetByIdAsync(id);

        if (placeDetail is null) return;

        ImageGallery.ItemsSource = new List<string> { placeDetail.CoverImageUrl };
        

    }
}