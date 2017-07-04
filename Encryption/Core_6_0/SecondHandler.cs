using System.Threading.Tasks;using NServiceBus;

public class SecondHandler : IHandleMessages<SecondMessage>
{
    public Task Handle(SecondMessage message, IMessageHandlerContext context)
    {
        Verifier.SecondMessageReceivedFrom.Add(message.Sender);
        Asserter.IsTrue("Secret" == message.EncryptedProperty, "Incorrect EncryptedProperty value");

        return Task.FromResult(0);
    }
}