using MardinCityGuide.Mobile.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MardinCityGuide.Mobile.AuthServices
{
    public class AuthService : IAuthService
    {

        private readonly HttpClient _httpClient = new HttpClient();

        public AuthService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://hasanbozkus.tr/api/"),
                Timeout = TimeSpan.FromSeconds(60)
            };
        }

        public async Task<bool> LoginAsync(string userName, string password)
        {
            try
            {
                var dto = new LoginUserDto
                {
                    UserName = userName,
                    PasswordHash = password
                };

                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var response = await _httpClient.PostAsync("User/LoginUser", content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    Preferences.Default.Set("IsLoggedIn", true);
                    Preferences.Default.Set("CurrentUserName", userName);

                    return true;
                }
                if (!response.IsSuccessStatusCode)
                {

                    var errorDetail = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"API HATA DETAYI: {errorDetail}");
                    return false;
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RegisterAsync(RegisterUserDtos registerUserDtos)
        {
            try
            {
                var json = JsonSerializer.Serialize(registerUserDtos);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var response = await _httpClient.PostAsync("User/RegisterUser", content).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {

                    var errorDetail = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"API HATA DETAYI: {errorDetail}");
                    return false;
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"HATA OLUŞTU: {ex.GetType().Name} - {ex.Message}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"INNER EXCEPTION: {ex.InnerException.Message}");
                }
                return false;
            }
        }
    }
}
