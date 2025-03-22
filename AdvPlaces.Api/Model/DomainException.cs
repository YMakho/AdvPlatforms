namespace AdvPlaces.Api.Model
{
    public sealed class DomainException(string message) : Exception(message)
    {
    }
}
