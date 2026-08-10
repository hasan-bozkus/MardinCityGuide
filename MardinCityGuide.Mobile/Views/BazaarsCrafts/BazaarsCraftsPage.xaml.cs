using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.Mobile.Helpers;

namespace MardinCityGuide.Mobile.Views.BazaarsCrafts;

public partial class BazaarsCraftsPage : ContentPage
{
	private readonly IBazaarService _bazaarService;
	private readonly ICategoryService _categoryService;
    private List<Bazaar> _allBazaars = new List<Bazaar>();
    private Border? _previouslySelectedBorder;

    public BazaarsCraftsPage()
	{
		InitializeComponent();
		_bazaarService = ServiceHelper.GetService<IBazaarService>();
		_categoryService = ServiceHelper.GetService<ICategoryService>();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var categories = await _categoryService.TGetListAllAsync();

		categories.Insert(0, new Category { CategoryId = 0, CategoryName = "Tümü"});

        SelectedCategoryListCollection.ItemsSource = categories.Take(8);

        _allBazaars = await _bazaarService.TGetBazaarsWithCategoryAsync();

        SelectedCategoryListCollection.SelectedItem = categories[0];


    }

    private void OnCategorySelectionChanged(object sender, TappedEventArgs e)
	{
        if (sender is not Border tappedBorder) return;
        if (e.Parameter is not Category category) return;

        if (_previouslySelectedBorder != null)
            VisualStateManager.GoToState(_previouslySelectedBorder, "Normal");

        VisualStateManager.GoToState(tappedBorder, "Selected");
        _previouslySelectedBorder = tappedBorder;

        //if (category.CategoryId == 0)
        //    BazaarCollection.ItemsSource = _allBazaars;
        //else
        //    BazaarCollection.ItemsSource = _allBazaars.Where(b => b.CategoryId == category.CategoryId).ToList();
    }
}