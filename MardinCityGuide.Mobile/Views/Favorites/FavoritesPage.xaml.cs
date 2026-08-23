using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Enums;
using MardinCityGuide.Mobile.Dtos.FavoritesDtos;
using MardinCityGuide.Mobile.Helpers;

namespace MardinCityGuide.Mobile.Views.Favorites;

public partial class FavoritesPage : ContentPage
{
    //private int CurrentUserId => CurrentSession.UserId;

    private readonly IFavoriteService _favoriteService;

	private List<FavoriteDisplayDto> _allFavorites = new List<FavoriteDisplayDto>();
    private FavoriteType? _selectedFilter = null;

    public FavoritesPage()
	{
		InitializeComponent();
		_favoriteService = ServiceHelper.GetService<IFavoriteService>();
	}

	override protected async void OnAppearing()
	{
		base.OnAppearing();

        //_allFavorites = await _favoriteService.TGetUserFavoritesAsync(1, null);
        //favori alanı login işleminden sonraya açılacak. şimdilik ertelendi
    }
}