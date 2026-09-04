using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.Mobile.AuthServices;
using MardinCityGuide.Mobile.Dtos.UserDtos;
using MardinCityGuide.Mobile.Helpers;
using MardinCityGuide.Mobile.Views.Login;

namespace MardinCityGuide.Mobile.Views.Register;

public partial class RegisterPage : ContentPage
{
    private bool _isPasswordHidden = true;
    private bool _isConfirmPasswordHidden = true;
    private FileResult _selectedPhoto;
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public RegisterPage()
	{
		InitializeComponent();
        _authService = ServiceHelper.GetService<IAuthService>();
        _userService = ServiceHelper.GetService<IUserService>();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void OnTogglePasswordClicked(object sender, EventArgs e)
    {
        _isPasswordHidden = !_isPasswordHidden;
        PasswordHashEntry.IsPassword = _isPasswordHidden;

        // Metin unicode'unu değiştiriyoruz
        TogglePasswordBtn.Text = _isPasswordHidden ? "\ue8f4" : "\ue8f5";
    }

    private void OnToggleConfirmPasswordClicked(object sender, EventArgs e)
    {
        _isConfirmPasswordHidden = !_isConfirmPasswordHidden;
        ConfirmPasswordEntry.IsPassword = _isConfirmPasswordHidden;

        ToggleConfirmPasswordBtn.Text = _isConfirmPasswordHidden ? "\ue8f4" : "\ue8f5";
    }

    private async void OnSelectProfileImageTapped(object sender, TappedEventArgs e)
    {
        try
        {
            var photo = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Lütfen bir profil fotoğrafı seçin"
            });

            if (photo != null)
            {
                _selectedPhoto = photo;

                // Ekrandaki Label text'ini seçilen dosya adıyla güncelle
                SelectedImageLabel.Text = photo.FileName;
                SelectedImageLabel.TextColor = Color.FromArgb("#1B1C1A");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Fotoğraf seçilemedi: {ex.Message}", "Tamam");
        }
    }

    private async void OnCreateAccountClicked(object sender, EventArgs e)
    {            
        string savedImagePath = null;

        if (_selectedPhoto != null)
        {
            try
            {
                string newFileName = $"{Guid.NewGuid()}_{_selectedPhoto.FileName}";
                savedImagePath = Path.Combine(FileSystem.AppDataDirectory, newFileName);

                using var sourceStream = await _selectedPhoto.OpenReadAsync();
                using var localFileStream = File.Create(savedImagePath);
                await sourceStream.CopyToAsync(localFileStream);
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Hata", $"Görsel kaydedilirken bir hata oluştu: {ex.Message}", "Tamam");
                return;
            }
        }

        var registerUserDto = new RegisterUserDtos()
        {
            NameSurname = NameSurnameEntry.Text,
            UserName = UserNameEntry.Text,
            Email = EmailEntry.Text,
            PasswordHash = PasswordHashEntry.Text,
            ImageUrl = savedImagePath
        };

        string confirmPassword = ConfirmPasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(registerUserDto.NameSurname) ||
            string.IsNullOrWhiteSpace(registerUserDto.UserName) ||
            string.IsNullOrWhiteSpace(registerUserDto.Email) ||
            string.IsNullOrWhiteSpace(registerUserDto.PasswordHash) ||
            string.IsNullOrWhiteSpace(registerUserDto.ImageUrl) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlertAsync("Eksik Bilgi", "Lütfen tüm alanları doldurun.", "Tamam");
            return;
        }

        if (registerUserDto.PasswordHash != confirmPassword)
        {
            await DisplayAlertAsync("Hata", "Şifreler eşleşmiyor.", "Tamam");
            return;
        }

        if (!TermsCheckBox.IsChecked)
        {
            await DisplayAlertAsync("Hata", "Şartları kabul etmelisiniz.", "Tamam");
            return;
        }

        try
        {

            bool isSuccess = await _authService.RegisterAsync(registerUserDto);

            if (isSuccess)
            {
                var user = new User
                {
                    NameSurname = registerUserDto.NameSurname,
                    UserName = registerUserDto.UserName,
                    Email = registerUserDto.Email,
                    Password = registerUserDto.PasswordHash,
                    AvatarUrl = savedImagePath,
                };

                await _userService.TCreateAsync(user);
                await DisplayAlertAsync("Başarılı", $"Hoş geldiniz, {registerUserDto.NameSurname}! Kaydınız oluşturuldu.", "Tamam");

                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                await DisplayAlertAsync("Hata", "Kayıt oluşturulamadı. Lütfen tekrar deneyin.", "Tamam");
            }
        }
        catch (Exception)
        {
            await DisplayAlertAsync("Hata", "Bu e-posta adresi zaten kayıtlı.", "Tamam");
        }
    }

    private async void OnLoginTapped(object sender, EventArgs e)
    {
        // Login Sayfasına Yönlendir
        await Navigation.PushAsync(new LoginPage());
    }

    private async void OnTermsTapped(object sender, TappedEventArgs e)
    {
        string termsText = "Mardin Şerih Rehberi - Şartlar ve Koşullar\n\n" +
            "1. Üyekil\n" +
            "Bu uygulamayı kullanarak, sağladığımız bilgilerin doğru ve güncel olduğunu kabul edersiniz.\n\n" +
            "2. Gizlilik\n" +
            "Kişisel verileriniz yalnızca uygulama hiztemtlerini sunmak amacı ile kullanılır ve üçüncü taraflara paylaşılmaz.\n\n" +
            "3. Kullanım\n" +
            "Uygulama içeriği yalnızca kişisel ve ticari olmayan amaçlarla kullanılabilir.\n\n" +
            "4. Sorumluluk\n" +
            "Etkinlik bilgileri değişebilir; güncel bilgileri ilgili mekândan teyit etmeniz önerilir.\n\n" +
            "Bu şartları kabul ederek devam etmiş olursunuz.";

        await DisplayAlertAsync("Şartlar ve Koşulllar", termsText, "Kapat");
    }
}