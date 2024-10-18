using RestSharp;

namespace Roots.Framework.Externals.Http;

public interface IRootsHttpClient
{
    Task<RestResponse<T>> GetAsync<T>(string resource, Dictionary<string, string> parameters = null) where T : new();
    Task<RestResponse<T>> PostAsync<T>(string resource, object body, Dictionary<string, string> parameters = null) where T : new();
    Task<RestResponse<T>> PutAsync<T>(string resource, object body, Dictionary<string, string> parameters = null) where T : new();
    Task<RestResponse<T>> DeleteAsync<T>(string resource, Dictionary<string, string> parameters = null) where T : new();
}