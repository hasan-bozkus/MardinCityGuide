using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.EntityLayer.Enums;
using MardinCityGuide.Mobile.Helpers;
using MardinCityGuide.Mobile.Models;

namespace MardinCityGuide.Mobile.Views.Profile;

public partial class ProfilePage : ContentPage
{
    private readonly IUserService _userService;
    private readonly IFavoriteService _favoriteService;

    private int CurrentUserId = CurrentSession.UserId;
    private string CurrentNameSurname = CurrentSession.NameSurname;
    private string CurrentEmail = CurrentSession.Email;
    private string CurrentImageUrl = (CurrentSession.AvatarUrl.Contains(".png") || CurrentSession.AvatarUrl.Contains(".jpg") || CurrentSession.AvatarUrl.Contains(".jpeg")) ? CurrentSession.AvatarUrl : null;
    private string CurrentUserName = CurrentSession.UserName;
    private FileResult _selectedPhoto;

    public ProfilePage()
    {
        InitializeComponent();
        _userService = ServiceHelper.GetService<IUserService>();
        _favoriteService = ServiceHelper.GetService<IFavoriteService>();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        LabelName.Text = CurrentNameSurname;
        LabelEmail.Text = CurrentEmail;

        if (!string.IsNullOrEmpty(CurrentImageUrl))
        {
            if (CurrentImageUrl.StartsWith("http://") || CurrentImageUrl.StartsWith("https://"))
            {
                ImageAvatarUrl.Source = ImageSource.FromUri(new Uri(CurrentImageUrl));
            }
            else
            {
                ImageAvatarUrl.Source = ImageSource.FromFile(CurrentImageUrl);
            }
            ImageAvatarUrl.IsVisible = true;
            LabelAvatarIcon.IsVisible = false; // Görsel varsa ikonu gizle
        }
        else
        {
            ImageAvatarUrl.IsVisible = false;
            LabelAvatarIcon.IsVisible = true;  // Görsel yoksa vektörel ikonu göster
        }

        var favoriteCount = await _favoriteService.TGetUserFavoritesAsync(CurrentUserId);
        FavoriteCount.Text = favoriteCount.Count().ToString();

        var myRoutes = await _favoriteService.TGetUserFavoritesAsync(CurrentUserId);
        MyRoutes.Text = myRoutes.Count(x =>
        {
            var property = x.GetType().GetProperty("FavoriteType");
            if (property != null)
            {
                var val = property.GetValue(x);
                return val != null && (int)val == (int)FavoriteType.Route;
            }
            return false;
        }).ToString();
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
                //SelectedImageLabel.Text = photo.FileName;
                //SelectedImageLabel.TextColor = Color.FromArgb("#1B1C1A");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Hata", $"Fotoğraf seçilemedi: {ex.Message}", "Tamam");
        }

        await OnChangedAvatarImage(_selectedPhoto);
    }

    private async Task OnChangedAvatarImage(FileResult selectedPhoto)
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

        await _userService.TUpdateUserAvatarUrlAsync(CurrentUserId, savedImagePath);
        OnAppearing();
    }

    private async void OnLogoutCilcked(object sender, EventArgs e)
    {
        bool confirm = await Shell.Current.DisplayAlertAsync("Çıkış Yap", "Oturumu kapatmak istediğinize emin misiniz?", "Evet", "Hayır");
        if (confirm)
        {
            Preferences.Default.Remove("AuthToken");
            Preferences.Default.Remove("UserId");

            await Shell.Current.GoToAsync("login");
        }
    }

    // --- HESABIM MODAL İŞLEMLERİ ---
    private void OnAccountTapped(object sender, TappedEventArgs e)
    {
        EntryName.Text = CurrentNameSurname;
        EntryEmail.Text = CurrentEmail;
        EntryUserName.Text = CurrentUserName;
        AccountModalOverlay.IsVisible = true; // Modalı aynı sayfada aç
    }

    private void OnCloseAccountModalTapped(object sender, EventArgs e)
    {
        AccountModalOverlay.IsVisible = false; // Modalı kapat
    }

    private async void OnSaveAccountClicked(object sender, EventArgs e)
    {
        CurrentNameSurname = EntryName.Text;
        CurrentEmail = EntryEmail.Text;
        CurrentUserName = EntryUserName.Text;
        await _userService.TUpdateUserWithNameAndUserNameAndEmailAsync(CurrentUserId, EntryName.Text, EntryUserName.Text, EntryEmail.Text);

        LabelName.Text = CurrentNameSurname;
        LabelEmail.Text = CurrentEmail;
        

        AccountModalOverlay.IsVisible = false;
        OnAppearing();
    }

    // --- BİLDİRİM MODAL İŞLEMLERİ ---
    private void OnNotificationsTapped(object sender, TappedEventArgs e)
    {
        SwitchNotifications.IsToggled = Preferences.Default.Get("NotificationsEnabled", true);
        NotificationModalOverlay.IsVisible = true; // Modalı aynı sayfada aç
    }

    private void OnCloseNotificationModalTapped(object sender, EventArgs e)
    {
        NotificationModalOverlay.IsVisible = false; // Modalı kapat
    }

    private void OnNotificationToggled(object sender, ToggledEventArgs e)
    {
        Preferences.Default.Set("NotificationsEnabled", e.Value);
    }

    // --- YARDIM & DESTEK ---
    private async void OnSupportTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlertAsync("Yardım & Destek",
                           "Destek ekibimizle iletişime geçmek için destek@mardincityguide.com adresine e-posta gönderebilirsiniz.",
                           "Tamam");
    }
}