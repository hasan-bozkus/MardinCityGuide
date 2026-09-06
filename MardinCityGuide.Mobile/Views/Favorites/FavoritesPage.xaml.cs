using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Enums;
using MardinCityGuide.Mobile.Dtos.FavoritesDtos;
using MardinCityGuide.Mobile.Helpers;
using MardinCityGuide.Mobile.Models;

namespace MardinCityGuide.Mobile.Views.Favorites;

public partial class FavoritesPage : ContentPage
{
    //private int CurrentUserId => CurrentSession.UserId;

    private readonly IFavoriteService _favoriteService;
    private int CurrentUserId => CurrentSession.UserId;

    private FavoriteType? _selectedFilter = null;

    public FavoritesPage()
	{
		InitializeComponent();
		_favoriteService = ServiceHelper.GetService<IFavoriteService>();
	}

	override protected async void OnAppearing()
	{
		base.OnAppearing();

         var allFavorites = await _favoriteService.TGetUserFavoritesAsync(CurrentUserId, null);
        FavoritesCollection.ItemsSource = allFavorites;
        //favori alanı login işleminden sonraya açılacak. şimdilik ertelendi
    }
}