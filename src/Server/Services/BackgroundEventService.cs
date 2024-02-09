using Grpc.Core;
using Shared.Protos;

namespace Server.Services;

public class BackgroundEventService : Shared.Protos.BackgroundEventgRPC.BackgroundEventgRPCBase
{
    public override Task<BackgroundEventProto> GetBackgroundEvents(BackgroundEventRequest request, ServerCallContext context)
    {
        var a = new BackgroundEventProto();

        var b = new Shared.Protos.Value() { StringValue = "" };

        return base.GetBackgroundEvents(request, context);
    }

    public override Task<BackgroundEventResponse> NotifyOfEvent(BackgroundEventProto request, ServerCallContext context)
    {
        request.Data.ToDictionary(key => key.Key, value => value.Value);
        return base.NotifyOfEvent(request, context);
    }
}
