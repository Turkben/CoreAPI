using CoreApi.WebApp.Interfaces;
using CoreApi.WebApp.Models;

namespace CoreApi.WebApp.Services
{
    public class UserApiService:IUserApiService
    {
        private readonly HttpClient _httpClient;

        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       

        public async Task<SignUpViewModel> SaveAsync(SignUpViewModel signupViewModel)
        {
            //var response = await _httpClient.PostAsJsonAsync("user", signupViewModel);
            throw new NotImplementedException();
        }

        public async Task<UserViewModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserViewModel>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
