using CommonMessages;
using NServiceBus;

public class SendHandler : IHandleMessages<DataBusSendMessage>
{
    public IBus Bus { get; set; }

    public void Handle(DataBusSendMessage message)
    {
        DataBusVerifier.SendReceivedFromSites.Add(message.SentFrom);
        Asserter.IsTrue(message.PropertyDataBus != null, "Incorrect property value");
        Asserter.IsTrue("Secret" == message.EncryptedProperty, "Incorrect EncryptedProperty value");
        Bus.Reply(new DataBusResponseMessage
            {
                PropertyDataBus = new byte[1024 * 1024],
                EncryptedProperty = "Secret",
                Sender = TestRunner.EndpointName
            });
    }
}