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
        var placeDetail = await _placeService.TGetPlaceWithLocationAndCategoryAsync(id);

        if (placeDetail is null) return;

        ImageGallery.ItemsSource = new List<string> { placeDetail.CoverImageUrl };
        Tag.Text = placeDetail!.Tag;
        Rating.Text = placeDetail!.Rating.ToString();
        ReviewCount.Text = "(" + placeDetail!.ReviewCount.ToString() + ")";
        Title.Text = placeDetail!.Name;
        District.Text = placeDetail!.Location.District;
        Description.Text = placeDetail!.Description.ToString();

        AddressText.Source = placeDetail.Location.AddressText;

        var start = TimeSpan.FromHours(placeDetail.OpeningTimeStart.Value.Ticks);
        var end = TimeSpan.FromHours(placeDetail.OpeningTimeEnd.Value.Ticks);

        OpeningTime.Text = $@"{start:hh\:mm} - {end:hh\:mm}";

        var priceLevel = (int)placeDetail.PriceLevel;
        if (priceLevel == 1)
        {
            PriceLevel.Text = "Uygun Fiyatlı";
        }
        if (priceLevel == 2)
        {
            PriceLevel.Text = "Orta Fiyatlı";
        }
        if (priceLevel == 3)
        {
            PriceLevel.Text = "Yüksek Fiyatlı";
        }

        var placeType = (int)placeDetail.PlaceType;
        if (placeType == 1)
        {
            PlaceType.Text = "Restoran";
        }
        if (placeType == 2)
        {
            PlaceType.Text = "Kafe";
        }
        if (placeType == 3)
        {
            PlaceType.Text = "Zıkkım";
        }
        if (placeType == 4)
        {
            PlaceType.Text = "Geleneksel";
        }
    }
}