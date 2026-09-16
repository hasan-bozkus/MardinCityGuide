using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.EntityLayer.Enums;
using MardinCityGuide.DataAccessLayer.Dtos.FavoritesDtos;
using MardinCityGuide.Mobile.Helpers;
using MardinCityGuide.Mobile.Models;

namespace MardinCityGuide.Mobile.Views.Favorites;

public partial class FavoritesPage : ContentPage
{
    private readonly IFavoriteService _favoriteService;
    private readonly IReligiousSiteService _religiousSiteService;

    private int CurrentUserId => CurrentSession.UserId;
    private Border? _previouslySelectedBorder;
    private FavoriteType? _selectedFilter = null;
    private List<ResultGetUserFavoritesDto> _allFavorites = new List<ResultGetUserFavoritesDto>();


    public FavoritesPage()
    {
        InitializeComponent();
        _favoriteService = ServiceHelper.GetService<IFavoriteService>();
        _religiousSiteService = ServiceHelper.GetService<IReligiousSiteService>();
    }

    override protected async void OnAppearing()
    {
        base.OnAppearing();

        var favoriteTypes = Enum.GetValues(typeof(FavoriteType)).Cast<FavoriteType>().Select(x => new
        {
            Value = (int)x,
            Text = x.ToString()
        }).ToList();

        favoriteTypes.Insert(0, new { Value = 0, Text = "Tümü" });
        SelectedFavoriteTypeListCollection.ItemsSource = favoriteTypes;

        var favoriteCount = await _favoriteService.TGetUserFavoritesAsync(CurrentUserId);
        FavoriteCount.Text = favoriteCount.Count().ToString();

        _allFavorites = await _favoriteService.TGetUserFavoritesAsync(CurrentUserId, null);
        FavoritesCollection.ItemsSource = _allFavorites.Select(x => new
        {
                x.Rating,
                x.Title,
                x.Location,
                x.CategoryTag,
                IsFavoriteColor = x.IsFavoriteColor ? Color.FromArgb("#D9381E") : Color.FromArgb("#7F766A"),
                x.TargetId,
                x.FavoriteType,
                x.ImageUrl,
        }).ToList();


        var getReligiousSitesForRecommended = await _religiousSiteService.TGetMosquesAndMonasteriesListAsync();
        var random = new Random().Next(0, getReligiousSitesForRecommended.Where(x => x.IsActive == true && x.IsFavorite == false).Count() - 1);

        RecommendedReligiousSiteCollection.ItemsSource = getReligiousSitesForRecommended.Skip(random - 1).Take(8).ToList();



        SelectedFavoriteTypeListCollection.SelectedItem = favoriteTypes[0];
    }

    private async void OnFavoriteTypeSelectionChanged(object sender, TappedEventArgs e)
    {
        if (sender is not Border tappedBorder) return;
        if (e.Parameter is not int favorite) return;

        if (_previouslySelectedBorder != null)
            VisualStateManager.GoToState(_previouslySelectedBorder, "Normal");

        VisualStateManager.GoToState(tappedBorder, "Selected");
        _previouslySelectedBorder = tappedBorder;

        if (favorite == 0)
        {
            _allFavorites = await _favoriteService.TGetUserFavoritesAsync(CurrentUserId, null);
            FavoritesCollection.ItemsSource = _allFavorites.Select(x => new
            {
                x.Rating,
                x.Title,
                x.Location,
                x.CategoryTag,
                IsFavoriteColor = x.IsFavoriteColor ? Color.FromArgb("#D9381E") : Color.FromArgb("#7F766A"),
                x.TargetId,
                x.FavoriteType,
                x.ImageUrl,
            }).ToList();
        }
        else if (favorite == (int)FavoriteType.Restaurant)
        {
            _allFavorites = await _favoriteService.TGetUserFavoritesAsync(CurrentUserId, FavoriteType.Restaurant);
            FavoritesCollection.ItemsSource = _allFavorites.Where(x => x.FavoriteType.ToString() == FavoriteType.Restaurant.ToString()).Select(x => new
            {
                x.Rating,
                x.Title,
                x.Location,
                x.CategoryTag,
                IsFavoriteColor = x.IsFavoriteColor ? Color.FromArgb("#D9381E") : Color.FromArgb("#7F766A"),
                x.TargetId,
                x.FavoriteType,
                x.ImageUrl,
            });
        }
        else if (favorite == (int)FavoriteType.Site)
        {
            _allFavorites = await _favoriteService.TGetUserFavoritesAsync(CurrentUserId, FavoriteType.Site);
            FavoritesCollection.ItemsSource = _allFavorites.Where(x => x.FavoriteType.ToString() == FavoriteType.Site.ToString()).Select(x => new
            {
                x.Rating,
                x.Title,
                x.Location,
                x.CategoryTag,
                IsFavoriteColor = x.IsFavoriteColor ? Color.FromArgb("#D9381E") : Color.FromArgb("#7F766A"),
                x.TargetId,
                x.FavoriteType,
                x.ImageUrl,
            });
        }
        else
        {
            _allFavorites = await _favoriteService.TGetUserFavoritesAsync(CurrentUserId, FavoriteType.Restaurant);
            FavoritesCollection.ItemsSource = _allFavorites.Where(x => x.FavoriteType.ToString() == FavoriteType.Route.ToString()).Select(x => new
            {
                x.Rating,
                x.Title,
                x.Location,
                x.CategoryTag,
                IsFavoriteColor = x.IsFavoriteColor ? Color.FromArgb("#D9381E") : Color.FromArgb("#7F766A"),
                x.TargetId,
                x.FavoriteType,
                x.ImageUrl,
            });
        }
    }

    private async void OnAddFavoriteWithTargetClicked(object sender, EventArgs e)
    {
        if (sender is not ImageButton button) return;

        if (button.CommandParameter is not string rawParameter) return;

        var parts = rawParameter.Split(':');
        if (parts.Length < 2) return;

        if (!int.TryParse(parts[0], out int targetId)) return;

        string favoriteTypeStr = parts[1];
        var isFavorite = await _favoriteService.TGetFavoritePlaceByTargetIdRestaurantAsync(targetId);

        if (isFavorite != null)
        {
            await _favoriteService.TRemoveFavoriteAsync(targetId, favoriteTypeStr);
        }
        else
        {
            Enum.TryParse(favoriteTypeStr, out FavoriteType favoriteEnum);

            await _favoriteService.TCreateAsync(new Favorite
            {
                UserId = CurrentUserId,
                TargetId = targetId,
                FavoriteType = favoriteEnum
            });

            await _favoriteService.TAddFavoriteAsync(targetId, favoriteTypeStr);
        }

        OnAppearing();
    }
    private async void OnAddFavoriteClicked(object sender, EventArgs e)
    {
        if (sender is not Button button) return;
        if (button.CommandParameter is int religiousSiteId)
        {

            var isFavorite = await _favoriteService.TGetFavoritePlaceByTargetIdRestaurantAsync(religiousSiteId);
            if (isFavorite != null)
            {
                await _favoriteService.TDeleteAsync(isFavorite);
                await _religiousSiteService.TGetChangeIsFavoriteStatusFalseAsync(religiousSiteId);
                OnAppearing();
            }
            if (isFavorite is null)
            {
                await _favoriteService.TCreateAsync(new Favorite
                {
                    UserId = CurrentUserId,
                    TargetId = religiousSiteId,
                    FavoriteType = FavoriteType.Site
                });
                await _religiousSiteService.TGetChangeIsFavoriteStatusTrueAsync(religiousSiteId);
                OnAppearing();
            }
        }
        if (button.CommandParameter is not ReligiousSite religiousSite) return;
    }

}