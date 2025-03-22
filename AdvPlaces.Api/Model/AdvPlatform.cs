namespace AdvPlaces.Api.Model
{
    public sealed class AdvPlatform
    {
        public string PlatformName { get; private set; }
        public string PlatformLocation { get; private set; }         
        private AdvPlatform(string name, string location)
        {
            PlatformName = name;
            PlatformLocation = location;
        }
        public static AdvPlatform Initialize(string place, string location)
        {
            if (string.IsNullOrWhiteSpace(place))
                throw new DomainException("Name is incorrect or empty.");
            if (string.IsNullOrWhiteSpace(location))
                throw new DomainException("Location is incorrect or empty.");
            if (!IsValidLocationFormat(location))
                throw new DomainException("Location must be in the format /ru, /ru/msk, /ru/msk/smth, etc.");
            return new AdvPlatform(place, location);
        }
        private static bool IsValidLocationFormat(string location)
        {
            if (!location.StartsWith("/"))
                return false;
            if (location.EndsWith("/")) 
                return false;
            var parts = location.Split(['/'], StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                if (string.IsNullOrWhiteSpace(part) || !part.All(char.IsLetter))
                    return false;
                if (part.Length < 2 || part.Length > 10)
                    return false;
            }
            return true;
        }
    }
    
}

