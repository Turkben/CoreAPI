using CoreApi.WebApp.Models;

namespace CoreApi.WebApp.Interfaces
{
    public interface IUserApiService
    {
        Task<List<UserViewModel>> GetUsersAsync();
        Task<UserViewModel> GetByIdAsync(string id);
        Task<SignUpViewModel> SaveAsync(SignUpViewModel signupViewModel);
        
    }
}
