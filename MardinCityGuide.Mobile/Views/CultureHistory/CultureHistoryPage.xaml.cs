using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.Mobile.Helpers;

namespace MardinCityGuide.Mobile.Views.CultureHistory;

public partial class CultureHistoryPage : ContentPage
{
    private readonly IMuseumService _museumService;

    public CultureHistoryPage()
    {
        InitializeComponent();
        _museumService = ServiceHelper.GetService<IMuseumService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var featuredMuseum = await _museumService.TGetListAllAsync();

        FeaturedMuseumListCollection.BindingContext = featuredMuseum.Where(x => x.IsFeatured == true).Take(3).Select(x => new
        {

            x.Name,
            x.CoverImageUrl,
            OpeningTime = $@"Açılış - {TimeSpan.FromHours(x.OpeningTime.Value.Ticks):hh\:mm}"
        }).ToList();
    }
}