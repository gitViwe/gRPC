namespace ProtobufGenerator.Model;

public class BackgroundEvent(Dictionary<string, object> data)
{
    public BackgroundEvent(Dictionary<string, object> data, Dictionary<string, object> headers)
        : this(data)
    {
        Headers = headers;
    }

    public Dictionary<string, object> Headers { get; set; } = [];
    public Dictionary<string, object> Data { get; set; } = data;
}

public record HubBlogUserProfileCreatedEventData(string HubIdentityUserId, string HubBlogUserProfileId);
