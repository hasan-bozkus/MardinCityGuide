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
    }
}