using System.Threading.Tasks;
using CommonMessages;
using NServiceBus;

namespace Common.SendReply
{
    public class FirstHandler : IHandleMessages<SendReplyFirstMessage>
    {
        public Task Handle(SendReplyFirstMessage message, IMessageHandlerContext context)
        {
            SendReplyVerifier.FirstMessageReceivedFrom.Add(message.Sender);
            Asserter.IsTrue("Secret" == message.EncryptedProperty, "Incorrect EncryptedProperty value");

            return context.Reply(new SendReplySecondMessage
                {
                    Sender = TestRunner.EndpointName,
                    EncryptedProperty = "Secret"
                });
        }
    }
}