namespace Store.Service.CasheServices
{
    public interface ICasheService
    {
        Task SetCasheResponseAsync(string Key,object response,TimeSpan TimeToLive);
        Task<string> GetCasheResponseAsync(string key);
    }
}
