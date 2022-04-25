using static RocketLanding.LandingRequest;

namespace RocketLanding;

public record class Platform
{
    private static Platform? _instance = null;
    private static readonly object _locker = new object();

    public LandSize LandSize { get; set; }
    public Location Location { get; set;}

    public Platform(LandSize landSize, Location location)
    {
        LandSize = landSize;
        Location = location;
    }
    public Platform GetPlatform(LandSize LandSize, Location Location)
    {
        if (_instance is null)
            lock (_locker)
            {
                if (_instance is null)
                    _instance = new Platform(LandSize, Location);
            }
        
        return _instance;
    }

}
