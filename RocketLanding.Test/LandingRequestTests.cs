using System;
using Xunit;
using static RocketLanding.LandingRequest;

namespace RocketLanding.Test
{
    public class LandingRequestTests
    {
        [Fact]
        public void LandingArea_Constructor_Sets_Properties()
        {
            LandingArea _landingArea = new LandingArea(100, 100);

            Assert.Equal(100, _landingArea.Width);
            Assert.Equal(100, _landingArea.Height);
        }

        [Fact]
        public void Platform_Constructor_Check_LandSize_and_Location()
        {
            var landSize = new LandSize(15, 15);
            var location = new Location(5, 5);
            var platform = new Platform(landSize, location);

            Assert.Equal(landSize, platform.LandSize);
            Assert.Equal(location, platform.Location);
        }


        [Fact]
        public void ReserveLand_StateUnderTest_ExpectedBehavior_ok_for_landing()
        {
            var _landingArea = new LandingArea(100, 100);
            
            var landSize = new LandSize(15, 15);
            var location = new Location(5, 5);
            var platform = new Platform(landSize, location);

            
            // Arrange
            var landingRequest = new LandingRequest(_landingArea, platform);
            var rocket_location = new Location(10, 10);
            Rocket rocket = new Rocket(Guid.NewGuid(), rocket_location);

            // Act
            var result = landingRequest.ReserveLand(rocket);

            // Assert
            Assert.Equal(LandingRequestResult.ok_for_landing, result);
        }

        [Fact]
        public void ReserveLand_StateUnderTest_ExpectedBehavior_out_of_platform()
        {
            var _landingArea = new LandingArea(100, 100);

            var landSize = new LandSize(15, 15);
            var location = new Location(5, 5);
            var platform = new Platform(landSize, location);


            // Arrange
            var landingRequest = new LandingRequest(_landingArea, platform);
            var rocket_location = new Location(100, 10);
            Rocket rocket = new Rocket(Guid.NewGuid(), rocket_location);

            // Act
            var result = landingRequest.ReserveLand(rocket);

            // Assert
            Assert.Equal(LandingRequestResult.out_of_platform, result);
        }

        [Fact]
        public void ReserveLand_StateUnderTest_ExpectedBehavior_clash()
        {
            // Arrange
            var _landingArea = new LandingArea(100, 100);

            var landSize = new LandSize(15, 15);
            var location = new Location(5, 5);
            var platform = new Platform(landSize, location);

            var landingRequest = new LandingRequest(_landingArea, platform);

            //Act
            Rocket rocket1 = new Rocket(Guid.NewGuid(), new Location(10, 10));
            var result = landingRequest.ReserveLand(rocket1);

            Rocket rocket2 = new Rocket(Guid.NewGuid(), new Location(11, 9));
            var result2 = landingRequest.ReserveLand(rocket2);

            Rocket rocket3 = new Rocket(Guid.NewGuid(), new Location(12, 9));
            var result3 = landingRequest.ReserveLand(rocket3);

            // Assert
            Assert.Equal(LandingRequestResult.ok_for_landing, result);
            Assert.Equal(LandingRequestResult.clash, result2);
            Assert.Equal(LandingRequestResult.ok_for_landing, result3);
        }
    }
}
