using MardinCityGuide.Mobile.Views.Login;

namespace MardinCityGuide.Mobile.Views.Register;

public partial class RegisterPage : ContentPage
{
    private bool _isPasswordHidden = true;
    private bool _isConfirmPasswordHidden = true;

    public RegisterPage()
	{
		InitializeComponent();
	}

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void OnTogglePasswordClicked(object sender, EventArgs e)
    {
        _isPasswordHidden = !_isPasswordHidden;
        PasswordEntry.IsPassword = _isPasswordHidden;

        // Metin unicode'unu değiştiriyoruz
        TogglePasswordBtn.Text = _isPasswordHidden ? "\ue8f4" : "\ue8f5";
    }

    private void OnToggleConfirmPasswordClicked(object sender, EventArgs e)
    {
        _isConfirmPasswordHidden = !_isConfirmPasswordHidden;
        ConfirmPasswordEntry.IsPassword = _isConfirmPasswordHidden;

        ToggleConfirmPasswordBtn.Text = _isConfirmPasswordHidden ? "\ue8f4" : "\ue8f5";
    }

    private async void OnCreateAccountClicked(object sender, EventArgs e)
    {
        // Kayıt İşlemleri
        Preferences.Default.Set("HasSeenWelcome", true);
        await DisplayAlertAsync("Başarılı", "Hesabınız oluşturuldu!", "Tamam");

        // Ana Sayfaya Yönlendir
        Application.Current.MainPage = new AppShell();
    }

    private async void OnLoginTapped(object sender, EventArgs e)
    {
        // Login Sayfasına Yönlendir
        await Navigation.PushAsync(new LoginPage());
    }
}