using System.Threading.Tasks;
using NServiceBus;

public class FirstHandler : IHandleMessages<FirstMessage>
{
    public Task Handle(FirstMessage message, IMessageHandlerContext context)
    {
        Verifier.FirstMessageReceivedFrom.Add(message.Sender);
        Asserter.IsTrue("Secret" == message.EncryptedProperty, "Incorrect EncryptedProperty value");

        return context.Reply(new SecondMessage
        {
            Sender = EndpointNames.EndpointName,
            EncryptedProperty = "Secret"
        });
    }
}