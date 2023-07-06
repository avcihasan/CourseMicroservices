namespace Course.Web.Services.Abstractions
{
    public interface IClientCredentialTokenService
    {
        Task<string> GetTokenAsync();
    }
}
