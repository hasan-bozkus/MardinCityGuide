using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.Mobile.Helpers;
using MardinCityGuide.EntityLayer.Concrete;

namespace MardinCityGuide.Mobile.Views.TravelRoutes;

public partial class TravelRoutesPage : ContentPage
{
	private readonly ICategoryService _categoryService;
    private readonly IRouteService _routeService;
    private readonly IRouteStopService _routeStopService;
    private Border? _previouslySelectedBorder;

    public TravelRoutesPage()
	{
		InitializeComponent();
		_categoryService = ServiceHelper.GetService<ICategoryService>();
        _routeService = ServiceHelper.GetService<IRouteService>();
        _routeStopService = ServiceHelper.GetService<IRouteStopService>();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var categories = await _categoryService.TGetListAllAsync();

        categories.Insert(0, new Category { CategoryId = 0, CategoryName = "Tümü" });

        SelectedCategoryListCollection.ItemsSource = categories.Take(8);

        var getOneRandomRoute = await _routeService.TGetOneRandomRouteAsync();
        GetOneRandomRoute.BindingContext = getOneRandomRoute;

        var getRouteStopsByRouteId = await _routeStopService.TGetRouteStopsByRouteIdAsync(getOneRandomRoute.RouteId);
        RouteStopListByRouteIdCollection.ItemsSource = getRouteStopsByRouteId;
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
        //    //BazaarCollection.BindingContext = _allBazaars.Where(x => x.IsFeatured == true).Take(3).ToList();
        //else
        //    BazaarCollection.BindingContext = _allBazaars.Where(b => b.CategoryId == category.CategoryId && b.IsFeatured == true).Take(3).ToList();
    }

    private async void OnViewMapTapped(object sender, TappedEventArgs e)
    {
        if (sender is not Element element) return;
        if (element.BindingContext is not Route route) return;
        await Launcher.Default.OpenAsync(new Uri(route.MapUrl));
    }
}