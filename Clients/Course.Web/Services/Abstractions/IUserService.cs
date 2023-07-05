using Course.Web.Models;

namespace Course.Web.Services.Abstractions
{
    public interface IUserService
    {
        Task<UserVM> GetUserAsync();
    }
}
