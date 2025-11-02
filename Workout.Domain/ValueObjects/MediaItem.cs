using Workout.Shared.Domain;

namespace Workout.Domain.ValueObjects;

public class MediaItem
{
    public string Url { get; private set; }
    public string Type { get; private set; } // "image" nebo "video"

    public MediaItem(string url, string type)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new DomainException("Media URL cannot be empty.");
        }

        Url = url;
        Type = type;
    }

    private MediaItem()
    {
    } // EF Core
}