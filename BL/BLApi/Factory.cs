using BlImplementation;

namespace BLApi;

/// <summary>
///  factory of bl
/// </summary>
public static class Factory
{
    /// <summary>
    /// Access to the implementation of bl
    /// </summary>
    /// <returns>object of bl</returns>
    public static IBl Get() => new Bl();
}