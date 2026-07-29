namespace MardinCityGuide.Mobile.Views.Splash;

public partial class SplashPage : ContentPage
{
    private bool _isAnimating = true;
    public SplashPage()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // 1. Arka plan Zoom (+ / -) Döngüsünü Başlat
        _ = StartContinuousZoomAnimationAsync();

        // 2. Yükleme Çizgisinin Soldan Sağa Kayan Animasyonunu Başlat
        _ = StartShimmerAnimationAsync();

        // 3. Elemanların Sırayla Ekrana Gelmesi (Fade In + Up)
        await AnimateEntranceAsync();

        // 4. Belli bir süre sonra sonraki ekrana (Welcome/Login) yönlendirme
        await Task.Delay(3000); // 3 saniye bekleme
        await Shell.Current.GoToAsync("//welcome");
    }

    protected override void OnDisappearing()
    {

        base.OnDisappearing();
        _isAnimating = false; // Sayfadan çıkılınca döngüleri durdur
    }

    /// <summary>
    /// Görselin Yavaşça Yakınlaşıp (1.0 -> 1.15) Uzaklaşmasını (1.15 -> 1.0) Sağlayan Döngü
    /// </summary>
    private async Task StartContinuousZoomAnimationAsync()
    {
        while (_isAnimating)
        {
            await BackgroundImage.ScaleToAsync(1.05, 10000, Easing.SinInOut);
            if (!_isAnimating) break;
            await BackgroundImage.ScaleToAsync(1.0, 10000, Easing.SinInOut);
        }
    }

    /// <summary>
    /// Progress Bar İçindeki Çizginin Soldan Sağa Sürekli Kayması (Shimmer Effect)
    /// </summary>
    private async Task StartShimmerAnimationAsync()
    {
        while (_isAnimating)
        {
            ProgressBar.TranslationX = -140; // En sola çek
            await ProgressBar.TranslateToAsync(140, 0, 2000, Easing.CubicInOut); // Sağa kaydır
        }
    }

    /// <summary>
    /// İçeriklerin Aşağıdan Yukarıya Doğru Yumuşakça Belirmesi
    /// </summary>
    private async Task AnimateEntranceAsync()
    {
        // Başlangıç offset pozisyonları
        TopBranding.TranslationY = 20;
        MiddleContent.TranslationY = 20;
        BottomContent.TranslationY = 20;

        // Top Branding
        await Task.Delay(300);
        _ = TopBranding.TranslateToAsync(0, 0, 800, Easing.CubicOut);
        _ = TopBranding.FadeToAsync(1, 800, Easing.CubicOut);

        // Middle Content
        await Task.Delay(300);
        _ = MiddleContent.TranslateToAsync(0, 0, 800, Easing.CubicOut);
        _ = MiddleContent.FadeToAsync(1, 800, Easing.CubicOut);

        // Bottom Content
        await Task.Delay(300);
        _ = BottomContent.TranslateToAsync(0, 0, 800, Easing.CubicOut);
        await BottomContent.FadeToAsync(1, 800, Easing.CubicOut);
    }
}