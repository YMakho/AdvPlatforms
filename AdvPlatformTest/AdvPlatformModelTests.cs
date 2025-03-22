using AdvPlaces.Api.Model;

namespace AdvPlatformTest
{
    public class AdvPlatformModelTests
    {
        [Theory]
        [MemberData(nameof(AdvPlatformModelTestData.ValidPlaceAndLocation), MemberType = typeof(AdvPlatformModelTestData))]
        public void Initialize_ValidPlaceAndLocation_ReturnsAdvPlatform(string platformName, string location)
        {
            //Act
            var advPlatform = AdvPlatform.Initialize(platformName, location);
            //Assert
            Assert.NotNull(advPlatform);
            Assert.Equal(platformName, advPlatform.PlatformName);
            Assert.Equal(location, advPlatform.PlatformLocation);
        }
        [Theory]
        [MemberData(nameof(AdvPlatformModelTestData.NullOrEmptyPlatformName), MemberType = typeof(AdvPlatformModelTestData))]
        public void Initialize_NullOrEmptyPlace_ThrowsDomainException(string platformName, string location)
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => AdvPlatform.Initialize(platformName, location));
        }
        [Theory]
        [MemberData(nameof(AdvPlatformModelTestData.NullOrEmptyLocation), MemberType = typeof(AdvPlatformModelTestData))]
        public void Initialize_NullOrEmptyLocation_ThrowsDomainException(string platformName, string location)
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => AdvPlatform.Initialize(platformName, location));
        }
        [Theory]
        [MemberData(nameof(AdvPlatformModelTestData.InvalidLocationFormat), MemberType = typeof(AdvPlatformModelTestData))]
        public void Initialize_InvalidLocationFormat_ThrowsDomainException(string platformName, string location)
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => AdvPlatform.Initialize(platformName, location));
        }
    }
}

