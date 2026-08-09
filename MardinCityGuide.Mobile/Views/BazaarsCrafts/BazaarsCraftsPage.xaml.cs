using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.Mobile.Helpers;

namespace MardinCityGuide.Mobile.Views.BazaarsCrafts;

public partial class BazaarsCraftsPage : ContentPage
{
	private readonly IBazaarService _bazaarService;
	private readonly ICategoryService _categoryService;
    public List<Bazaar> _allBazaars = new List<Bazaar>();

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

    private static async void OnCategorySelectionChanged(object sender, SelectionChangedEventArgs e)
	{
        // Seçili öğeyi al
        if (e.CurrentSelection.FirstOrDefault() is not Category category)
            return;

        if (category.CategoryId == 0)
        {
            // "Tümü" -> hepsini göster
            //BazaarCollection.ItemsSource = _allBazaars;
        }
        else
        {
            // Seçilen kategoriye ait etkinlikleri süz
            //var filtered = _allEvents
            //    .Where(ev => ev.CategoryId == category.CategoryId)
            //    .ToList();

            //EventsCollection.ItemsSource = filtered;
        }
    }
}