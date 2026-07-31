using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.Mobile.Helpers;

namespace MardinCityGuide.Mobile.Views.Home;

public partial class HomePage : ContentPage
{
    private readonly ICategoryService _categoryService;

    public HomePage()
    {
        InitializeComponent();
        _categoryService = ServiceHelper.GetService<ICategoryService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var categoryName = "Medrese";
        var sectionType = EntityLayer.Enums.SectionType.BazaarsAndCrafts;
        var sortOrder = 1;
        var model = new Category();

        model.CategoryName = categoryName;
        model.SortOrder = sortOrder;
        model.SectionType = sectionType;

        await _categoryService.TCreateAsync(model);

        var values = await _categoryService.TGetListAllAsync();
    }
}