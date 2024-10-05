namespace Store.Service.Services.CasheServices
{
    public interface ICasheService
    {
        Task SetCasheResponseAsync(string Key, object response, TimeSpan TimeToLive);
        Task<string> GetCasheResponseAsync(string key);
    }
}
