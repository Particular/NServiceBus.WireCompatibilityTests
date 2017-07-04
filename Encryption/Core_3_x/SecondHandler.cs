using NServiceBus;

public class SecondHandler : IHandleMessages<SecondMessage>
{
    public IBus Bus { get; set; }

    public void Handle(SecondMessage message)
    {
        Verifier.SecondMessageReceivedFrom.Add(message.Sender);
        Asserter.IsTrue("Secret" == message.EncryptedProperty, "Incorrect EncryptedProperty value");
    }
}