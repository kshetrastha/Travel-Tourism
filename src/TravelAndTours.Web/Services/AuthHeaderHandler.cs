using System.Net.Http.Headers;

namespace TravelAndTours.Web.Services;

public sealed class AuthHeaderHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _http;

    public AuthHeaderHandler(IHttpContextAccessor http)
    {
        _http = http;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _http.HttpContext?.Session.GetString(SessionKeys.AccessToken);
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
