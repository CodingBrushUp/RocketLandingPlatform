using System.Collections.Concurrent;

namespace RocketLanding;
public class LandingRequest
{
    public record Location(int X, int Y);
    public record LandSize(int Width, int Height);

    ConcurrentDictionary<Rocket, Location> ReservedLands = new ConcurrentDictionary<Rocket, Location>();
    private readonly LandingArea _landingArea;
    private readonly Platform _platform;

    public LandingRequest(LandingArea landingArea, Platform platform)
    {
        _landingArea = landingArea;
        _platform = platform;
    }

    public LandingRequestResult ReserveLand(Rocket rocket)
    {
        ArgumentNullException.ThrowIfNull(rocket);

        if (!isInLandingArea())
            throw new ArgumentOutOfRangeException("Platform size is out of Landing Area!");

        if (IsRocketOutofTrajectory(rocket))
            return LandingRequestResult.out_of_platform;

        if (ReservedLands.IsEmpty)
            return ReserveLandingLocation(rocket);

        if (ReservedLands.ContainsKey(rocket))
            return ReserveLandingLocation(rocket);

        return IsRocketInPreviousRocketArea(rocket);

        return LandingRequestResult.ok_for_landing;
    }

    private LandingRequestResult IsRocketInPreviousRocketArea(Rocket rocket)
    {
        foreach (var item in ReservedLands)
        {
            if (IsThereConflict(rocket, item.Key))
                return LandingRequestResult.clash;
        }
        return LandingRequestResult.ok_for_landing;
    }

    private bool IsThereConflict(Rocket rocket, Rocket item)
    {
        if ((rocket.Location.X >= (item.Location.X - 1) && rocket.Location.X <= (item.Location.X + 1)) &&
            (rocket.Location.Y >= (item.Location.Y - 1) && rocket.Location.Y <= (item.Location.Y + 1))
            )
            return true;

        return false;
    }

    private LandingRequestResult ReserveLandingLocation(Rocket rocket)
    {
        ReservedLands?.AddOrUpdate(rocket, rocket.Location, (Rocket, Location) => rocket.Location);
        return LandingRequestResult.ok_for_landing;
    }

    private bool IsRocketOutofTrajectory(Rocket rocket)
    {
        if (rocket.Location.X < _platform.Location.X || rocket.Location.X > (_platform.Location.X + _platform.LandSize.Width) ||
            rocket.Location.Y < _platform.Location.Y || rocket.Location.Y > (_platform.Location.Y + _platform.LandSize.Height))
            return true;

        return false;
    }

    private bool isInLandingArea()
    {
        if (_platform.LandSize.Width > _landingArea.Width
            || _platform.LandSize.Height > _landingArea.Height)
            return false;

        if ((_platform.Location.X + _platform.LandSize.Width) > _landingArea.Width
            || (_platform.Location.Y + _platform.LandSize.Height) > _landingArea.Height)
            return false;

        return true;
    }

}
