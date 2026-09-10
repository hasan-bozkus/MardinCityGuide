using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.EntityLayer.Enums;
using MardinCityGuide.Mobile.Helpers;
using MardinCityGuide.Mobile.Models;

namespace MardinCityGuide.Mobile.Views.CultureHistory;

public partial class CultureHistoryPage : ContentPage
{
    private readonly IMuseumService _museumService;
    private readonly IReligiousSiteService _religiousSiteService;
    private readonly IHistoricalSiteService _historicalSiteService;
    private readonly ICulturalEventService _culturalEventService;
    private readonly IFavoriteService _favoriteService;
    private int CurrentUserId = CurrentSession.UserId;


    public CultureHistoryPage()
    {
        InitializeComponent();
        _museumService = ServiceHelper.GetService<IMuseumService>();
        _religiousSiteService = ServiceHelper.GetService<IReligiousSiteService>();
        _historicalSiteService = ServiceHelper.GetService<IHistoricalSiteService>();
        _culturalEventService = ServiceHelper.GetService<ICulturalEventService>();
        _favoriteService = ServiceHelper.GetService<IFavoriteService>();
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
            x.ReligiousSiteId,
            x.Name,
            x.ImageUrl,
            Era = x.Dynasty + ", " + x.Era,
            IsFavoriteColor = x.IsFavorite == true ? Color.FromArgb("#D9381E") : Color.FromArgb("#7F766A")
        }).ToList();

        var firstHistoricalSite = await _historicalSiteService.TGetFirstHistoricalSiteWithImageGaleriesAsync();

        //firstHistoricalSite.Images.Select(x => x.I)

        if(firstHistoricalSite.IsSignatureExperience == true)
        {
            IsSignatureExperience.Text = "Mutlaka Ziyaret Edilmeli";
        }

        HistoricalSiteColleciton.BindingContext = firstHistoricalSite;

        var upcoming2Events = await _culturalEventService.TGetUpcoming2EventsAsync();

        Upcoming2EventsCollection.BindingContext = upcoming2Events.Select(x => new
        {
            x.Title,
            x.LocationName,
            StartDay = x.EventDate.ToString("dd"),
            StartMonth = x.EventDate.ToString("MMM")
        }).ToList();
    }

    private async void OnAddFavoriteTapped(object sender, TappedEventArgs e)
    {
        if (sender is not Border border) return;
        if (e.Parameter is int religiousSiteId)
        {
            var isFavorite = await _favoriteService.TGetFavoriteReligiousSiteByTargetIdSiteAsync(religiousSiteId);
            if (isFavorite != null)
            {
                await _favoriteService.TDeleteAsync(isFavorite);
                await _religiousSiteService.TGetChangeIsFavoriteStatusFalseAsync(religiousSiteId);
                OnAppearing();
            }
            if (isFavorite is null)
            {
                await _favoriteService.TCreateAsync(new Favorite
                {
                    UserId = CurrentUserId,
                    TargetId = religiousSiteId,
                    FavoriteType = FavoriteType.Site
                });
                await _religiousSiteService.TGetChangeIsFavoriteStatusTrueAsync(religiousSiteId);
                OnAppearing();
            }
        }
        if (e.Parameter is not ReligiousSite religiousSite) return;
    }
}