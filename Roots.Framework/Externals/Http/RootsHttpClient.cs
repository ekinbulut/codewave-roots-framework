using RestSharp;

namespace Roots.Framework.Externals.Http;

public class RootsHttpClient : IRootsHttpClient
{
    private readonly IRestClient _client;

    public RootsHttpClient(IRestClient restClient)
    {
        _client = restClient;
    }

    public async Task<RestResponse<T>> GetAsync<T>(string resource, Dictionary<string, string> parameters = null) where T : new()
    {
        
        var request = new RestRequest(resource, Method.Get);
        AddParameters(request, parameters);
        return await _client.ExecuteAsync<T>(request);
    }

    public async Task<RestResponse<T>> PostAsync<T>(string resource, object body, Dictionary<string, string> parameters = null) where T : new()
    {
        var request = new RestRequest(resource, Method.Post);
        request.AddJsonBody(body);
        AddParameters(request, parameters);
        return await _client.ExecuteAsync<T>(request);
    }

    public async Task<RestResponse<T>> PutAsync<T>(string resource, object body, Dictionary<string, string> parameters = null) where T : new()
    {
        var request = new RestRequest(resource, Method.Put);
        request.AddJsonBody(body);
        AddParameters(request, parameters);
        return await _client.ExecuteAsync<T>(request);
    }

    public async Task<RestResponse<T>> DeleteAsync<T>(string resource, Dictionary<string, string> parameters = null) where T : new()
    {
        var request = new RestRequest(resource, Method.Delete);
        AddParameters(request, parameters);
        return await _client.ExecuteAsync<T>(request);
    }

    private void AddParameters(RestRequest request, Dictionary<string, string> parameters)
    {
        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                request.AddParameter(param.Key, param.Value);
            }
        }
    }
}