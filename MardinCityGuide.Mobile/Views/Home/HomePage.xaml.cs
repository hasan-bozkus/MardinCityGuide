using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.EntityLayer.Enums;
using MardinCityGuide.DataAccessLayer.Dtos.EditorialHighlightDtos;
using MardinCityGuide.DataAccessLayer.Dtos.HomeNevTileDtos;
using MardinCityGuide.Mobile.Helpers;
using MardinCityGuide.Mobile.Models;

namespace MardinCityGuide.Mobile.Views.Home;

public partial class HomePage : ContentPage
{
    private readonly IHighlightService _highlightService;
    private readonly IHomeNavTileService _homeNavTileService;
    private readonly IPlaceService _placeService;
    private readonly IEditorialHighlightService _editoralHighlightService;
    private readonly IFavoriteService _favoriteService;
    private string CurrentUserName => CurrentSession.UserName;
    private int CurrentUserId => CurrentSession.UserId;

    public List<ResultHomeNawTileDto> ResultHomeNawTileDtos { get; set; }
    public ResultGetRandomEditorialHighlightDto ResultGetRandomEditorialHighlightDto { get; set; }

    public HomePage()
    {
        InitializeComponent();
        _highlightService = ServiceHelper.GetService<IHighlightService>();
        _homeNavTileService = ServiceHelper.GetService<IHomeNavTileService>();
        _placeService = ServiceHelper.GetService<IPlaceService>();
        _editoralHighlightService = ServiceHelper.GetService<IEditorialHighlightService>();
        _favoriteService = ServiceHelper.GetService<IFavoriteService>();
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

        PlaceListCollection.ItemsSource = placeList.Take(2).Select(x => new
        {
            x.PlaceId,
            x.Name,
            x.Description,
            x.CoverImageUrl,
            x.Rating,
            x.ReviewCount,
            x.TypeLabel,
            IsFavoriteColor = x.IsFavorite ? Color.FromArgb("#D9381E") : Color.FromArgb("#7F766A")
        }).ToList();

        var randomEditoralHighlight = await _editoralHighlightService.TGetRandomEditoralHighlightAsync();

        ResultGetRandomEditorialHighlightDto = new ResultGetRandomEditorialHighlightDto
        {
            BadgeLabel = randomEditoralHighlight.BadgeLabel,
            Description = randomEditoralHighlight.Description,
            IllustrationUrl = ParseUnicodeIcon(randomEditoralHighlight.IllustrationUrl),
            Title = randomEditoralHighlight.Title,
            CtaUrl = randomEditoralHighlight.CtaUrl
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

    private async void OnRouteTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("travelroutes");
    }

    private async void OnRouteTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("travelroutes");
    }

    private async void OnRedirectCtaUrlForEditoralHighlightClicked(object sender, EventArgs e)
    {
        if (sender is not Button element) return;
        if (element.CommandParameter is not string ctaUrl) return;
        await Launcher.Default.OpenAsync(new Uri(ctaUrl));
    }

    private async void OnAddFavoriteTapped(object sender, EventArgs e)
    {
        if (sender is not Button button) return;
        if (button.CommandParameter is int placeId)
        {

            var isFavorite = await _favoriteService.TGetFavoritePlaceByTargetIdRestaurantAsync(placeId);
            if (isFavorite != null)
            {
                await _favoriteService.TDeleteAsync(isFavorite);
                await _placeService.TGetChangeIsFavoriteStatusFalseAsync(placeId);
                OnAppearing();
            }
            if (isFavorite is null)
            {
                await _favoriteService.TCreateAsync(new Favorite
                {
                    UserId = CurrentUserId,
                    TargetId = placeId,
                    FavoriteType = FavoriteType.Restaurant
                });
                await _placeService.TGetChangeIsFavoriteStatusTrueAsync(placeId);
                OnAppearing();
            }
        }
        if (button.CommandParameter is not Place places) return;
    }

    private async void OnRedirectTapped(object sender, TappedEventArgs e)
    {
        if (sender is not Border border) return;
        if (e.Parameter is string title)
        {
            var isHomeNavTitleName = await _homeNavTileService.TGetHomeNawTitleByTitleAsync(title);
            if (isHomeNavTitleName.Title == "Kültür & Etkinlik")
                await Shell.Current.GoToAsync("culturehistory");
            if (isHomeNavTitleName.Title == "Tarihi Çarşılar")
                await Shell.Current.GoToAsync("bazaarscraft");
            if (isHomeNavTitleName.Title == "İnanç & Maneviyat")
                await Shell.Current.GoToAsync("monasteriesandmosques");
            if (isHomeNavTitleName.Title == "Gastronomi")
                await Shell.Current.GoToAsync("gastronomyguide");
        }

        if (e.Parameter is not HomeNavTile homeNavTile) return;
    }
}