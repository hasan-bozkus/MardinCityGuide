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
        }
    }
}
