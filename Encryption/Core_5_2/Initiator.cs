using System.Threading.Tasks;
using NServiceBus;

public static class SendReplyInitiator
{
    public static void InitiateSendReply(this IBus bus)
    {
        Parallel.ForEach(EndpointNames.All, endpointName =>
        {
            bus.Send(endpointName, new FirstMessage
            {
                Sender = EndpointNames.EndpointName,
                EncryptedProperty = "Secret"
            });
        });
    }
}