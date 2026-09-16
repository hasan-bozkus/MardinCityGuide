namespace MardinCityGuide.Mobile.Views.Layout;

public partial class FooterLayoutPage : ContentView
{
    public static readonly BindableProperty SelectedTabProperty =
        BindableProperty.Create(nameof(SelectedTab), typeof(int), typeof(FooterLayoutPage), 0);

    public int SelectedTab
    {
        get => (int)GetValue(SelectedTabProperty);
        set => SetValue(SelectedTabProperty, value);
    }

    public FooterLayoutPage()
	{
		InitializeComponent();
        Loaded += CustomBottomBarView_Loaded;

    }

    private void CustomBottomBarView_Loaded(object? sender, EventArgs e)
    {
        UpdateSelectedTab();
    }

    private void UpdateSelectedTab()
    {
        if (Shell.Current == null) return;

        // O an açık olan sayfanın x:Class adını veya Shell Route adını alır
        var currentPage = Shell.Current.CurrentPage;

        if (currentPage == null) return;

        string pageName = currentPage.GetType().Name;

        // Sayfa adına göre ilgili Tab'ı aktif yap
        SelectedTab = pageName switch
        {
            "MainPage" or "HomePage" => 0,
            "SeazonalEventsPage" or "EventsPage" => 1,
            "FavoritesPage" => 2,
            "ProfilePage" => 3,
            _ => 0
        };
    }

    private async void OnFavoritesTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("favorites");
    }

    private async void OnProfileTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("profile");
    }

    private async void OnHomeTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//home");
    }

    private async void OnSeazonalEventTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("seazonalevent");
    }
}