using System.Threading.Tasks;
using CommonMessages;
using NServiceBus;

public class SendHandler : IHandleMessages<DataBusSendMessage>
{
    public Task Handle(DataBusSendMessage message, IMessageHandlerContext context)
    {
        DataBusVerifier.SendReceivedFromSites.Add(message.SentFrom);
        Asserter.IsTrue(message.PropertyDataBus != null, "Incorrect property value");
        return context.Reply(new DataBusResponseMessage
            {
                PropertyDataBus = new byte[100],
                Sender = TestRunner.EndpointName
            });
    }
}