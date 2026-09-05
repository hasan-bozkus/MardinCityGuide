using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.Mobile.AuthServices;
using MardinCityGuide.Mobile.Dtos.UserDtos;
using MardinCityGuide.Mobile.Helpers;
using MardinCityGuide.Mobile.Models;
using MardinCityGuide.Mobile.Views.Register;

namespace MardinCityGuide.Mobile.Views.Login;

public partial class LoginPage : ContentPage
{
    private bool _isPasswordHidden = true;
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public LoginPage()
    {
        InitializeComponent();
        _authService = ServiceHelper.GetService<IAuthService>();
        _userService = ServiceHelper.GetService<IUserService>();
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
        PasswordHashEntry.IsPassword = _isPasswordHidden;

        // Açık göz (\ue8f4) / Kapalı göz (\ue8f5)
        TogglePasswordBtn.Text = _isPasswordHidden ? "\ue8f4" : "\ue8f5";
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        bool hasSeenWelcome = Preferences.Default.Get("HasSeenWelcome", false);
        var loginUserDto = new LoginUserDto()
        {
            UserName = UserNameEntry.Text,
            PasswordHash = PasswordHashEntry.Text
        };

        if (string.IsNullOrWhiteSpace(loginUserDto.UserName) || string.IsNullOrWhiteSpace(loginUserDto.PasswordHash))
        {
            await DisplayAlertAsync("Eksik Bilgi", "Lütfen kullanıcı adı ve şifrenizi girin.", "Tamam");
            return;
        }

        bool isSuccess = await _authService.LoginAsync(loginUserDto.UserName, loginUserDto.PasswordHash);
        if (isSuccess)
        {
            User? user = await _userService.TGetUserByNameAsync(loginUserDto.UserName);

            if (user is null)
            {
                user = new User()
                {
                    NameSurname = loginUserDto.UserName,
                    UserName = loginUserDto.UserName,
                    Email = loginUserDto.UserName,
                    Password = loginUserDto.PasswordHash
                };

                await _userService.TCreateAsync(user);
            }

            if (user.Password != loginUserDto.PasswordHash)
            {
                await DisplayAlertAsync("Giriş Başarısız", "Kullanıcı adı veya şifre hatalı.", "Tamam");
                return;
            }
            CurrentSession.UserId = user.UserId;
            CurrentSession.NameSurname = user.NameSurname;
            CurrentSession.UserName = user.UserName;
            CurrentSession.Email = user.Email;
            CurrentSession.AvatarUrl = user.AvatarUrl;
            bool isLoggedIn = Preferences.Default.Get("IsLoggedIn", false);
            if (isLoggedIn)
            {
                await Shell.Current.GoToAsync("//home");
            }
            else
            {
                await Shell.Current.GoToAsync("//login");
            }
        }
        else
        {
            await DisplayAlertAsync("Hata", "Kullanıcı adı veya şifre hatalı.", "Tamam");
        }
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