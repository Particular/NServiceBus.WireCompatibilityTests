using System.Threading.Tasks;
using CommonMessages;
using NServiceBus;

public static class SendReplyInitiator
{
    public static async Task InitiateSendReply(this IEndpointInstance bus)
    {
        foreach (var endpoint in EndpointNames.All)
        {
            await bus.Send(endpoint, new SendReplyFirstMessage
                {
                    Sender = TestRunner.EndpointName,
                    EncryptedProperty = "Secret"
                }).ConfigureAwait(false);
        }
    }
}