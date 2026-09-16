namespace MardinCityGuide.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute("splash", typeof(Views.Splash.SplashPage));
            Routing.RegisterRoute("welcome", typeof(Views.Welcome.WelcomePage));
            Routing.RegisterRoute("register", typeof(Views.Register.RegisterPage));
            Routing.RegisterRoute("login", typeof(Views.Login.LoginPage));
            Routing.RegisterRoute("home", typeof(Views.Home.HomePage));
            Routing.RegisterRoute("placedetail", typeof(Views.PlaceDetail.PlaceDetailPage));
            Routing.RegisterRoute("map", typeof(Views.Map.MapPage));
            Routing.RegisterRoute("favorites", typeof(Views.Favorites.FavoritesPage));
            Routing.RegisterRoute("profile", typeof(Views.Profile.ProfilePage));
            Routing.RegisterRoute("culturehistory", typeof(Views.CultureHistory.CultureHistoryPage));
            Routing.RegisterRoute("bazaarscraft", typeof(Views.BazaarsCrafts.BazaarsCraftsPage));
            Routing.RegisterRoute("gastronomyguide", typeof(Views.GastronomyGuide.GastronomyGuidePage));
            Routing.RegisterRoute("monasteriesandmosques", typeof(Views.MonasteriesAndMosques.MonasteriesAndMosquesPage));
            Routing.RegisterRoute("seazonalevent", typeof(Views.SeazonalEvents.SeazonalEventsPage));
            Routing.RegisterRoute("travelroutes", typeof(Views.TravelRoutes.TravelRoutesPage));
        }
    }
}
