using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.EntityLayer.Enums;
using MardinCityGuide.Mobile.Helpers;

namespace MardinCityGuide.Mobile.Views.MonasteriesAndMosques;

public partial class MonasteriesAndMosquesPage : ContentPage
{
    private readonly IReligiousSiteService _religiousSiteService;

    private List<ReligiousSite> _allReligiousSites = new List<ReligiousSite>();
    private Border? _previouslySelectedBorder;

    public MonasteriesAndMosquesPage()
    {
        InitializeComponent();
        _religiousSiteService = ServiceHelper.GetService<IReligiousSiteService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var siteTypes = Enum.GetValues(typeof(ReligiousSiteType)).Cast<ReligiousSiteType>().Select(x => new
        {
            Value = (int)x,
            Text = x.ToString()
        }).ToList();

        siteTypes.Insert(0, new { Value = 0, Text = "Tümü" });
        SelectedSiteTypeListCollection.ItemsSource = siteTypes;

        _allReligiousSites = await _religiousSiteService.TGetMosquesAndMonasteriesListAsync();
       

        MosquesAndMonasteriesAndMedresesColleciton.ItemsSource = _allReligiousSites.Select(x =>
        {
            var siteType = (ReligiousSiteType)x.SiteType;
            return new
            {
                x.LocationId,
                x.Name,
                x.ShortDescription,
                x.ImageUrl,
                OpeningTimeStartAndEimeEnd = $@"{TimeSpan.FromHours(x.OpeningTimeStart.Value.Ticks):hh\:mm} - {TimeSpan.FromHours(x.OpeningTimeEnd.Value.Ticks):hh\:mm}",
                x.SiteType,
                SiteTypeTextColor = x.SiteType switch
                {
                    ReligiousSiteType.Cami => GetResourceColor(this.Resources, "TerracottaSunset"),
                    ReligiousSiteType.Medrese => GetResourceColor(this.Resources, "MardinSand"),
                    ReligiousSiteType.Manastır => GetResourceColor(this.Resources, "MesopotamianBlue"),
                    _ => Colors.Transparent
                },
                EntryFeeAmount = x.IsFreeEntry == true ? "Ücretsiz" : x.EntryFeeAmount + " ₺"
            };
        }).Take(8).ToList();
    }

    private async void OnSiteTypeSelectionChanged(object sender, TappedEventArgs e)
    {
        if (sender is not Border tappedBorder) return;
        if (e.Parameter is not ReligiousSiteType Value) return;

        if (_previouslySelectedBorder != null)
            VisualStateManager.GoToState(_previouslySelectedBorder, "Normal");

        VisualStateManager.GoToState(tappedBorder, "Selected");
        _previouslySelectedBorder = tappedBorder;

        if (Value == 0)
            MosquesAndMonasteriesAndMedresesColleciton.ItemsSource = _allReligiousSites;
        else
        {
            MosquesAndMonasteriesAndMedresesColleciton.ItemsSource = _allReligiousSites.Where(b => b.SiteType == Value && b.IsActive == true).Take(8).ToList();
        }
    }

    private static Color GetResourceColor(ResourceDictionary resources, string key)
    {
        if (resources.TryGetValue(key, out var value) && value is Color color)
            return color;

        return Colors.Transparent;
    }

    private void OnBackButtonClicked(object sender, EventArgs e)
    {

    }

    private void OnSearchButtonClicked(object sender, EventArgs e)
    {

    }
}