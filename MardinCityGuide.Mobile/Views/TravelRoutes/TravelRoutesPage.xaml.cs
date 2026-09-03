using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.Mobile.Helpers;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.EntityLayer.Enums;

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

        var siteTypes = Enum.GetValues(typeof(RouteCategory)).Cast<RouteCategory>().Select(x => new
        {
            Value = (int)x,
            Text = x.ToString()
        }).ToList();

        siteTypes.Insert(0, new { Value = 0, Text = "Tümü" });
        SelectedCategoryListCollection.ItemsSource = siteTypes;

        var getOneRandomRoute = await _routeService.TGetOneRandomRouteAsync();
        GetOneRandomRoute.BindingContext = getOneRandomRoute;

        var getRouteStopsByRouteId = await _routeStopService.TGetRouteStopsByRouteIdAsync(getOneRandomRoute.RouteId);
        RouteStopListByRouteIdCollection.ItemsSource = getRouteStopsByRouteId;

        var routeCountWithCategoryGastronomy = await _routeService.TGetRouteListWithCategoryIsGastronomyAsync();
        RouteCountWithCategoryGastronomyCollection.BindingContext = routeCountWithCategoryGastronomy;

        var routeCountWithCategoryPhotography = await _routeService.TGetRouteListWithCategoryIsPhotographyAsync();
        RouteCountWithCategoryPhotographyCollection.ItemsSource = routeCountWithCategoryPhotography;
    }

    private async void OnCategorySelectionChanged(object sender, TappedEventArgs e)
    {
        if (sender is not Border tappedBorder) return;
        if (e.Parameter is not Route route) return;

        if (_previouslySelectedBorder != null)
            VisualStateManager.GoToState(_previouslySelectedBorder, "Normal");

        VisualStateManager.GoToState(tappedBorder, "Selected");
        _previouslySelectedBorder = tappedBorder;

        if (route.Category == 0)
        {
            var routeCountWithCategoryGastronomy = await _routeService.TGetRouteListWithCategoryIsGastronomyAsync();
            RouteCountWithCategoryGastronomyCollection.BindingContext = routeCountWithCategoryGastronomy;

            var routeCountWithCategoryPhotography = await _routeService.TGetRouteListWithCategoryIsPhotographyAsync();
            RouteCountWithCategoryPhotographyCollection.ItemsSource = routeCountWithCategoryPhotography;
        }
        else if(route.Category == RouteCategory.Mutfak)
        {
            var routeCountWithCategoryGastronomy = await _routeService.TGetRouteListWithCategoryIsGastronomyAsync();
            RouteCountWithCategoryGastronomyCollection.BindingContext = routeCountWithCategoryGastronomy;
        }
        else if(route.Category == RouteCategory.Fotoğrafçılık)
        {
            var routeCountWithCategoryPhotography = await _routeService.TGetRouteListWithCategoryIsPhotographyAsync();
            RouteCountWithCategoryPhotographyCollection.ItemsSource = routeCountWithCategoryPhotography;
        }
        else
        {
            var routeCountWithCategoryGastronomy = await _routeService.TGetRouteListWithCategoryIsGastronomyAsync();
            RouteCountWithCategoryGastronomyCollection.BindingContext = routeCountWithCategoryGastronomy;
        }
    }

    private async void OnViewMapTapped(object sender, TappedEventArgs e)
    {
        if (sender is not Element element) return;
        if (element.BindingContext is not Route route) return;
        await Launcher.Default.OpenAsync(new Uri(route.MapUrl));
    }
}