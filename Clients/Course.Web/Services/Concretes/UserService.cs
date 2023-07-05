using Course.Shared.DTOs;
using Course.Web.Models;
using Course.Web.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace Course.Web.Services.Concretes
{
    public class UserService : IUserService
    {
        readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserVM> GetUserAsync()
            => (await _httpClient.GetFromJsonAsync<ResponseDto<UserVM>>("/api/users")).Data;
    }
}
