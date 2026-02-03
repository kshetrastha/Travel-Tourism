using System.Net;
using System.Net.Http.Json;

namespace TravelAndTours.Web.Services;

public sealed class ApiClient
{
    private readonly HttpClient _http;

    public ApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<T?> GetAsync<T>(string url, CancellationToken ct = default)
    {
        var res = await _http.GetAsync(url, ct);
        if (res.StatusCode == HttpStatusCode.NoContent) return default;
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<T>(cancellationToken: ct);
    }

    public async Task<TOut?> PostAsync<TIn, TOut>(string url, TIn body, CancellationToken ct = default)
    {
        var res = await _http.PostAsJsonAsync(url, body, ct);
        if (res.StatusCode == HttpStatusCode.NoContent) return default;
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<TOut>(cancellationToken: ct);
    }

    public async Task PutAsync<TIn>(string url, TIn body, CancellationToken ct = default)
    {
        var res = await _http.PutAsJsonAsync(url, body, ct);
        res.EnsureSuccessStatusCode();
    }

    public async Task<TOut?> PutAsync<TIn, TOut>(string url, TIn body, CancellationToken ct = default)
    {
        var res = await _http.PutAsJsonAsync(url, body, ct);
        if (res.StatusCode == HttpStatusCode.NoContent) return default;
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<TOut>(cancellationToken: ct);
    }

    public async Task DeleteAsync(string url, CancellationToken ct = default)
    {
        var res = await _http.DeleteAsync(url, ct);
        res.EnsureSuccessStatusCode();
    }

    public async Task<TOut?> DeleteAsync<TOut>(string url, CancellationToken ct = default)
    {
        var res = await _http.DeleteAsync(url, ct);
        if (res.StatusCode == HttpStatusCode.NoContent) return default;
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<TOut>(cancellationToken: ct);
    }
}
