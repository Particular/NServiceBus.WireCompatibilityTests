using NServiceBus;

public class SendHandler : IHandleMessages<SendMessage>
{
    public IBus Bus { get; set; }

    public void Handle(SendMessage message)
    {
        Verifier.SendReceivedFromSites.Add(message.SentFrom);
        Asserter.IsTrue(message.PropertyDataBus != null, "Incorrect property value");
        Bus.Reply(new ResponseMessage
            {
                PropertyDataBus = new byte[100],
                Sender = EndpointNames.EndpointName
        });
    }
}