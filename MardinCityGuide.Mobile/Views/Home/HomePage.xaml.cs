using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.Mobile.Helpers;

namespace MardinCityGuide.Mobile.Views.Home;

public partial class HomePage : ContentPage
{
    private readonly IHighlightService _highlightService;

    public HomePage()
    {
        InitializeComponent();
        _highlightService = ServiceHelper.GetService<IHighlightService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var highlightList = await _highlightService.TGetHighlightListBySortOrderAsync();
        HighlightListCollection.ItemsSource = highlightList.Take(8).ToList();
    }
}