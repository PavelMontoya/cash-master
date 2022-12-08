//This should be more like in Common or Resources but for the lack of Entitites here it is

using Resources.Enums;

namespace Entities;

public class MessageResponse
{
    public string response { get; set; }
    public Status status { get; set; }

    public MessageResponse(string response, Status status)
    {
        this.response = response;
        this.status = status;
    }
}
