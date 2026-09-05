
using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.Mobile.Dtos.EditorialHighlightDtos;
using MardinCityGuide.Mobile.Dtos.HomeNevTileDtos;
using MardinCityGuide.Mobile.Helpers;
using MardinCityGuide.Mobile.Models;
using MardinCityGuide.Mobile.Views.PlaceDetail;

namespace MardinCityGuide.Mobile.Views.Home;

public partial class HomePage : ContentPage
{
    private readonly IHighlightService _highlightService;
    private readonly IHomeNavTileService _homeNavTileService;
    private readonly IPlaceService _placeService;
    private readonly IEditorialHighlightService _editoralHighlightService;
    private string CurrentUserName => CurrentSession.UserName;

    public List<ResultHomeNawTileDto> ResultHomeNawTileDtos { get; set; }
    public ResultGetRandomEditorialHighlightDto ResultGetRandomEditorialHighlightDto { get; set; }

    public HomePage()
    {
        InitializeComponent();
        _highlightService = ServiceHelper.GetService<IHighlightService>();
        _homeNavTileService = ServiceHelper.GetService<IHomeNavTileService>();
        _placeService = ServiceHelper.GetService<IPlaceService>();
        _editoralHighlightService = ServiceHelper.GetService<IEditorialHighlightService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        CurrentUserNameLabel.Text = "Merhaba, " + CurrentUserName;

        var highlightList = await _highlightService.TGetHighlightListBySortOrderAsync();
        HighlightListCollection.ItemsSource = highlightList.Take(8).ToList();

        var homeNavTileList = await _homeNavTileService.TGetHomeNavTileListBySortOrderAndIsActiveAsync();

        ResultHomeNawTileDtos = homeNavTileList.Select(x => new ResultHomeNawTileDto
        {
            Title = x.Title,
            IconKey = ParseUnicodeIcon(x.IconKey)
        }).Take(4).ToList();



        var placeList = await _placeService.TGetPlaceListWithSortOrderAsync();
        PlaceListCollection.ItemsSource = placeList.Take(2).ToList();

        var randomEditoralHighlight = await _editoralHighlightService.TGetRandomEditoralHighlightAsync();

        ResultGetRandomEditorialHighlightDto = new ResultGetRandomEditorialHighlightDto
        {
            BadgeLabel = randomEditoralHighlight.BadgeLabel,
            Description = randomEditoralHighlight.Description,
            IllustrationUrl = ParseUnicodeIcon(randomEditoralHighlight.IllustrationUrl),
            Title = randomEditoralHighlight.Title
        };

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

    private async void OnPlaceTapped(object sender, TappedEventArgs e)
    {
        if (sender is not Element element) return;
        if (element.BindingContext is not Place place) return;

        await Shell.Current.GoToAsync($"placedetail?id={place.PlaceId}");
    }
}