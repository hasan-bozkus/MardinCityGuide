namespace MardinCityGuide.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute("splash", typeof(Views.Splash.SplashPage));
            //Routing.RegisterRoute("welcome", typeof(Views.Welcome.WelcomePage));
            Routing.RegisterRoute("register", typeof(Views.Register.RegisterPage));
        }
    }
}
