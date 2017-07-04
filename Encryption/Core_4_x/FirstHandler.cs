using NServiceBus;

public class FirstHandler : IHandleMessages<FirstMessage>
{
    public IBus Bus { get; set; }

    public void Handle(FirstMessage message)
    {
        Verifier.FirstMessageReceivedFrom.Add(message.Sender);
        Asserter.IsTrue("Secret" == message.EncryptedProperty, "Incorrect EncryptedProperty value");
        Bus.Reply(new SecondMessage
        {
            Sender = EndpointNames.EndpointName,
            EncryptedProperty = "Secret"
        });
    }
}