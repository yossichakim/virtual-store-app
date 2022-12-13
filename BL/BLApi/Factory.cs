using BlImplementation;

namespace BLApi;

public class Factory
{
    public static IBl Get() => new Bl();
}
