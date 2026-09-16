using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.Mobile.Helpers;
using MardinCityGuide.Mobile.Views.Map;

namespace MardinCityGuide.Mobile.Views.BazaarsCrafts;

public partial class BazaarsCraftsPage : ContentPage
{
	private readonly IBazaarService _bazaarService;
	private readonly ICategoryService _categoryService;
    private readonly IArtisanCraftService _artisanCraftService;

    private List<Bazaar> _allBazaars = new List<Bazaar>();
    private Border? _previouslySelectedBorder;

    public BazaarsCraftsPage()
	{
		InitializeComponent();
		_bazaarService = ServiceHelper.GetService<IBazaarService>();
		_categoryService = ServiceHelper.GetService<ICategoryService>();
        _artisanCraftService = ServiceHelper.GetService<IArtisanCraftService>();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var categories = await _categoryService.TGetListAllAsync();

		categories.Insert(0, new Category { CategoryId = 0, CategoryName = "Tümü"});

        SelectedCategoryListCollection.ItemsSource = categories.Take(8);

        _allBazaars = await _bazaarService.TGetBazaarsWithCategoryAsync();

        BazaarCollection.BindingContext = _allBazaars.Where(x => x.IsFeatured == true).Take(3).ToList();

        SelectedCategoryListCollection.SelectedItem = categories[0];
        
        RandomArtisanCraftListCollection.BindingContext = await _artisanCraftService.TGetRandom2ArtisanCraftWithCategoryAsycn();

        IsActiveArtisanCraftListCollection.ItemsSource = await _artisanCraftService.TGetIsActiveArtisanCraftListAsync();
    }

    private void OnCategorySelectionChanged(object sender, TappedEventArgs e)
	{
        if (sender is not Border tappedBorder) return;
        if (e.Parameter is not Category category) return;

        if (_previouslySelectedBorder != null)
            VisualStateManager.GoToState(_previouslySelectedBorder, "Normal");

        VisualStateManager.GoToState(tappedBorder, "Selected");
        _previouslySelectedBorder = tappedBorder;

        if (category.CategoryId == 0)
            BazaarCollection.BindingContext = _allBazaars.Where(x => x.IsFeatured == true).Take(3).ToList();
        else
          BazaarCollection.BindingContext = _allBazaars.Where(b => b.CategoryId == category.CategoryId && b.IsFeatured == true).Take(3).ToList();
    }

    private async void OnOpenMapClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("map");
    }
}