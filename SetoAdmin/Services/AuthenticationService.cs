using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using SetoAdmin.Models;

namespace SetoAdmin.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;
        private UserAuthState _authState;

        public AuthenticationService(
            HttpClient httpClient,
            ILocalStorageService localStorage,
            NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
            _authState = new UserAuthState();
        }

        public async Task<bool> Login(string email, string password)
        {
            var loginRequest = new LoginRequest
            {
                Email = email,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("auth/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (result?.Token != null)
                {
                    await _localStorage.SetItemAsync("authToken", result.Token);

                    _authState.IsAuthenticated = true;
                    _authState.Token = result.Token;
                    _authState.UserEmail = email;

                    _httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);

                    return true; // Login successful
                }
            }

            return false; // Login failed
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            _authState = new UserAuthState();
        }

        public async Task<bool> InitializeAuthentication()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                _authState.IsAuthenticated = true;
                _authState.Token = token;
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                return true;
            }
            return false;
        }

        public UserAuthState GetAuthenticationState() => _authState;
    }
}