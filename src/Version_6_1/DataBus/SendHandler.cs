using System.Threading.Tasks;
using CommonMessages;
using NServiceBus;

public class SendHandler : IHandleMessages<DataBusSendMessage>
{
    public Task Handle(DataBusSendMessage message, IMessageHandlerContext context)
    {
        DataBusVerifier.SendReceivedFromSites.Add(message.SentFrom);
        Asserter.IsTrue(message.PropertyDataBus != null, "Incorrect property value");
        Asserter.IsTrue("Secret" == message.EncryptedProperty, "Incorrect EncryptedProperty value");
        return context.Reply(new DataBusResponseMessage
            {
                PropertyDataBus = new byte[1024 * 1024],
                EncryptedProperty = "Secret",
                Sender = TestRunner.EndpointName
            });
    }
}