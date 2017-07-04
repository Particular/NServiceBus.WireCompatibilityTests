using System.Threading.Tasks;
using NServiceBus;

public static class SendReplyInitiator
{
    public static async Task InitiateSendReply(this IEndpointInstance bus)
    {
        foreach (var endpoint in EndpointNames.All)
        {
            await bus.Send(endpoint, new FirstMessage
                {
                    Sender = EndpointNames.EndpointName,
                    EncryptedProperty = "Secret"
                }).ConfigureAwait(false);
        }
    }
}