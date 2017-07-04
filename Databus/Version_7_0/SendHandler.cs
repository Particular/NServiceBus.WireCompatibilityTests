using System.Threading.Tasks;
using NServiceBus;

public class SendHandler : IHandleMessages<SendMessage>
{
    public Task Handle(SendMessage message, IMessageHandlerContext context)
    {
        Verifier.SendReceivedFromSites.Add(message.SentFrom);
        Asserter.IsTrue(message.PropertyDataBus != null, "Incorrect property value");
        return context.Reply(new ResponseMessage
            {
                PropertyDataBus = new byte[100],
                Sender = EndpointNames.EndpointName
        });
    }
}