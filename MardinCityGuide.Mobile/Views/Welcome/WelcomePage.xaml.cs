using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;

namespace MardinCityGuide.Mobile.Views.Welcome;

public partial class WelcomePage : ContentPage
{
    private readonly ICategoryService _categoryService;

   

    private bool _isAnimating = false;
    public WelcomePage()
	{
		InitializeComponent();
	}

    public WelcomePage(ICategoryService categoryService)
    {
        InitializeComponent();
        _categoryService = categoryService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _isAnimating = true;
        StartSubtleZoomAnimation();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isAnimating = false; // Sayfadan ayrılınca animasyon döngüsünü durdurur
    }

    // Arka plan görseline sürekli zoom-in / zoom-out efekti veren metod
    private async void StartSubtleZoomAnimation()
    {
        while (_isAnimating)
        {
            // 10 saniyede hafifçe büyüt
            await BackgroundImage.ScaleToAsync(1.06, 10000, Easing.SinInOut);
            if (!_isAnimating) break;

            // 10 saniyede tekrar normal boyutuna döndür
            await BackgroundImage.ScaleToAsync(1.0, 10000, Easing.SinInOut);
        }
    }

    private async void OnContinueAsGuestClicked(object sender, EventArgs e)
    {
        // Kullanıcının uygulamaya daha önce girdiğini kaydedelim
        Preferences.Default.Set("HasSeenWelcome", true);

        // Ana Sayfaya (MainPage / Dashboard) yönlendir
        await Navigation.PushAsync(new MainPage());
    }

    private async void OnCreateAccountClicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("HasSeenWelcome", true);
        await Shell.Current.GoToAsync("//register");
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("HasSeenWelcome", true);
        await Shell.Current.GoToAsync("//login");
    }
}