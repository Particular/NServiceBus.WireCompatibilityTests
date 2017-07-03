using CommonMessages;
using NServiceBus;

public class SendHandler : IHandleMessages<DataBusSendMessage>
{
    public IBus Bus { get; set; }

    public void Handle(DataBusSendMessage message)
    {
        DataBusVerifier.SendReceivedFromSites.Add(message.SentFrom);
        Asserter.IsTrue(message.PropertyDataBus != null, "Incorrect property value");
        Bus.Reply(new DataBusResponseMessage
            {
                PropertyDataBus = new byte[100],
                Sender = EndpointNames.EndpointName
        });
    }
}