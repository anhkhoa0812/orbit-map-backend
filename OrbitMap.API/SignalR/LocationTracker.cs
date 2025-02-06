using System.Collections.Concurrent;
using OrbitMap.API.Payload.Response.Location;

namespace OrbitMap.API.SignalR;

public class LocationTracker
{
    private static readonly ConcurrentDictionary<string, UserLocationDto> UserLocations = new();

    public void UpdateUserLocation(string username, UserLocationDto location)
    {
        UserLocations.AddOrUpdate(username, location, (key, oldValue) => location);
    }

    public UserLocationDto GetUserLocation(string username)
    {
        UserLocations.TryGetValue(username, out var location);
        return location;
    }

    public List<UserLocationDto> GetLocationsForUsers(List<string> usernames)
    {
        return usernames.Select(u => GetUserLocation(u)).Where(l => l != null).ToList();
    }
}