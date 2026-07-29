using MardinCityGuide.Mobile.Views.Welcome;
using Microsoft.Extensions.DependencyInjection;

namespace MardinCityGuide.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            bool hasSeenWelcome = Preferences.Default.Get("HasSeenWelcome", false);

            Page initialPage;

            if (hasSeenWelcome)
            {
                // Daha önce girmiş -> Doğrudan AppShell (veya MainPage)
                initialPage = new AppShell();
            }
            else
            {
                // İlk defa giriyor -> NavigationPage ile sarmalanmış WelcomePage
                initialPage = new NavigationPage(new WelcomePage());
            }

            // Seçilen başlangıç sayfası ile Window nesnesini oluşturup döndürüyoruz
            return new Window(initialPage);
        }
    }
}