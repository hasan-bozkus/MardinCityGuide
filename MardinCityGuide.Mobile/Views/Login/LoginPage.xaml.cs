using MardinCityGuide.Mobile.Views.Register;

namespace MardinCityGuide.Mobile.Views.Login;

public partial class LoginPage : ContentPage
{
    private bool _isPasswordHidden = true;
    public LoginPage()
	{
		InitializeComponent();
	}

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (width < 768)
        {
            // Dar ekran: Kartı gizle, Grid kolonunu tek kolona indir
            SpotlightCard.IsVisible = false;
            MainGrid.ColumnDefinitions = new ColumnDefinitionCollection
        {
            new ColumnDefinition { Width = GridLength.Star }
        };
        }
        else
        {
            // Geniş ekran: Kartı göster, 2 kolon yap
            SpotlightCard.IsVisible = true;
            MainGrid.ColumnDefinitions = new ColumnDefinitionCollection
        {
            new ColumnDefinition { Width = GridLength.Star },
            new ColumnDefinition { Width = new GridLength(340) }
        };
        }
    }

    private void OnTogglePasswordClicked(object sender, EventArgs e)
    {
        _isPasswordHidden = !_isPasswordHidden;
        PasswordEntry.IsPassword = _isPasswordHidden;

        // Açık göz (\ue8f4) / Kapalı göz (\ue8f5)
        TogglePasswordBtn.Text = _isPasswordHidden ? "\ue8f4" : "\ue8f5";
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        // Başarılı giriş simülasyonu
        Preferences.Default.Set("HasSeenWelcome", true);
        Application.Current.MainPage = new AppShell();
    }

    private async void OnRegisterTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }

    private async void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        await DisplayAlertAsync("Bilgi", "Şifre sıfırlama bağlantısı e-posta adresinize gönderilecektir.", "Tamam");
    }
}