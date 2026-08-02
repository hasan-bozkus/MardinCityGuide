using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.Mobile.Dtos.HomeNevTileDtos;
using MardinCityGuide.Mobile.Helpers;
using System.Collections.ObjectModel;

namespace MardinCityGuide.Mobile.Views.Home;

public partial class HomePage : ContentPage
{
    private readonly IHighlightService _highlightService;
    private readonly IHomeNavTileService _homeNavTileService;

    public List<ResultHomeNawTileDto> ResultHomeNawTileDtos { get; set; }

    public HomePage()
    {
        InitializeComponent();
        _highlightService = ServiceHelper.GetService<IHighlightService>();
        _homeNavTileService = ServiceHelper.GetService<IHomeNavTileService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var highlightList = await _highlightService.TGetHighlightListBySortOrderAsync();
        HighlightListCollection.ItemsSource = highlightList.Take(8).ToList();

        var homeNavTileList = await _homeNavTileService.TGetHomeNavTileListBySortOrderAndIsActiveAsync();

        ResultHomeNawTileDtos = homeNavTileList.Select(x => new ResultHomeNawTileDto
        {
            Title = x.Title,
            IconKey = ParseUnicodeIcon(x.IconKey)
        }).Take(4).ToList();

        BindingContext = this;
    }

    // Yardımcı Metod:
    private string ParseUnicodeIcon(string iconKey)
    {
        if (string.IsNullOrEmpty(iconKey)) return string.Empty;

        // &#xe878; formatı geliyorsa temizleyelim
        string cleanHex = iconKey.Replace("&#x", "").Replace(";", "").Replace("\\x", "").Trim();

        if (int.TryParse(cleanHex, System.Globalization.NumberStyles.HexNumber, null, out int codePoint))
        {
            return char.ConvertFromUtf32(codePoint); // Karakteri ikon karşılığına çevirir
        }

        return iconKey;
    }
}