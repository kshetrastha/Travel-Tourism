namespace TravelAndTours.Application.Common.Errors;

public sealed class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message) { }
}
