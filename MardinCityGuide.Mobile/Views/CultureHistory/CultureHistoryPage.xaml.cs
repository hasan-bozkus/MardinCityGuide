using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Enums;
using MardinCityGuide.Mobile.Helpers;

namespace MardinCityGuide.Mobile.Views.CultureHistory;

public partial class CultureHistoryPage : ContentPage
{
    private readonly IMuseumService _museumService;
    private readonly IReligiousSiteService _religiousSiteService;
    private readonly IHistoricalSiteService _historicalSiteService;


    public CultureHistoryPage()
    {
        InitializeComponent();
        _museumService = ServiceHelper.GetService<IMuseumService>();
        _religiousSiteService = ServiceHelper.GetService<IReligiousSiteService>();
        _historicalSiteService = ServiceHelper.GetService<IHistoricalSiteService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var featuredMuseum = await _museumService.TGetListAllAsync();

        FeaturedMuseumListCollection.BindingContext = featuredMuseum.Where(x => x.IsFeatured == true).Take(3).Select(x => new
        {
            x.Name,
            x.CoverImageUrl,
            OpeningTime = $@"Açılış - {TimeSpan.FromHours(x.OpeningTime.Value.Ticks):hh\:mm}"
        }).ToList();

        var featuredReligiousSiteOfMadrasas = await _religiousSiteService.TGetListAllAsync();

        FeaturedReligiousSiteListCollection.ItemsSource = featuredReligiousSiteOfMadrasas.Where(x => x.SiteType == ReligiousSiteType.Medrese).Take(8).Select(x => new
        {
            x.Name,
            x.ImageUrl,
            Era = x.Dynasty + ", " + x.Era,
        }).ToList();

        var firstHistoricalSite = await _historicalSiteService.TGetFirstHistoricalSiteWithImageGaleriesAsync();

        //firstHistoricalSite.Images.Select(x => x.I)

        if(firstHistoricalSite.IsSignatureExperience == true)
        {
            IsSignatureExperience.Text = "Mutlaka Ziyaret Edilmeli";
        }

        HistoricalSiteColleciton.BindingContext = firstHistoricalSite;
    }
}