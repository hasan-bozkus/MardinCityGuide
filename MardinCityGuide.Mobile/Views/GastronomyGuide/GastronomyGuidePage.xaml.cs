using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.EntityLayer.Enums;
using MardinCityGuide.Mobile.Helpers;

namespace MardinCityGuide.Mobile.Views.GastronomyGuide;

public partial class GastronomyGuidePage : ContentPage
{
    private readonly IPlaceService _placeService;
    private readonly ICategoryService _categoryService;

    private List<Place> _allPlaces = new List<Place>();
    private List<Category> _allCategories = new List<Category>();
    private Border? _previouslySelectedBorder;

    public GastronomyGuidePage()
    {
        InitializeComponent();
        _placeService = ServiceHelper.GetService<IPlaceService>();
        _categoryService = ServiceHelper.GetService<ICategoryService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var getHighsetStarredPlace = await _placeService.TGetHighestStarredPlaceWithLocationAsync();

        PriceLevelText.Text = getHighsetStarredPlace.PriceLevel switch
        {
            PriceLevel.Pahalı => "₺₺₺",
            PriceLevel.Orta => "₺₺",
            PriceLevel.Uygun => "₺",
            _ => string.Empty
        };

        GetHighsetStarredPlaceColleciton.BindingContext = getHighsetStarredPlace;

        var categories = await _categoryService.TGetListAllAsync();

        categories.Insert(0, new Category { CategoryId = 0, CategoryName = "Tümü" });

        _allCategories = categories;
        _allCategories[0].IsSelected = true;

        SelectedTypeLabelCollection.ItemsSource = _allCategories.Take(5).ToList();

        _allPlaces = await _placeService.TGetPlaceListWithLocationByIsActiveAndIsFeatuderAsync();

        PlaceCollection.ItemsSource = _allPlaces.Select(x => new {
            x.CoverImageUrl,
            x.Name,
            x.Description,
            x.Rating,
            x.Location,
            PriceLevel = x.PriceLevel switch
            {
                PriceLevel.Pahalı => "₺₺₺",
                PriceLevel.Orta => "₺₺",
                PriceLevel.Uygun => "₺",
                _ => string.Empty
            }
        }).ToList();

        SelectedTypeLabelCollection.SelectedItem = _allCategories[0];


    }

    private void OnTypeLabelSelectionChanged(object sender, TappedEventArgs e)
    {
        if (sender is not Border tappedBorder) return;
        if (e.Parameter is not Category category) return;

        if (_previouslySelectedBorder != null)
            VisualStateManager.GoToState(_previouslySelectedBorder, "Normal");

        VisualStateManager.GoToState(tappedBorder, "Selected");
        _previouslySelectedBorder = tappedBorder;

        if (category.CategoryId == 0)
            PlaceCollection.ItemsSource = _allPlaces.Select(x => new {
                x.CoverImageUrl,
                x.Name,
                x.Description,
                x.Rating,
                x.Location,
                PriceLevel = x.PriceLevel switch
                {
                    PriceLevel.Pahalı => "₺₺₺",
                    PriceLevel.Orta => "₺₺",
                    PriceLevel.Uygun => "₺",
                    _ => string.Empty
                }
            }).ToList();
        else
            PlaceCollection.ItemsSource = _allPlaces.Where(b => b.CategoryId == category.CategoryId).Select(x => new {
                x.CoverImageUrl,
                x.Name,
                x.Description,
                x.Rating,
                x.Location,
                PriceLevel = x.PriceLevel switch
                {
                    PriceLevel.Pahalı => "₺₺₺",
                    PriceLevel.Orta => "₺₺",
                    PriceLevel.Uygun => "₺",
                    _ => string.Empty
                }
            }).ToList();
    }
}