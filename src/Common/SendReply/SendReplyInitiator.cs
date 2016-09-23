using System.Threading.Tasks;
using CommonMessages;
using NServiceBus;

public static class SendReplyInitiator
{
    public static void InitiateSendReply(this IBus bus)
    {
        Parallel.ForEach(EndpointNames.All, endpointName =>
        {
            bus.Send(endpointName, new SendReplyFirstMessage
            {
                Sender = TestRunner.EndpointName,
                EncryptedProperty = "Secret"
            });
        });
    }
}