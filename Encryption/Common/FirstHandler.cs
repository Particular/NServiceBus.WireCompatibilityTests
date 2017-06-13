using CommonMessages;
using NServiceBus;

namespace Common.SendReply
{
    public class FirstHandler : IHandleMessages<SendReplyFirstMessage>
    {
        public IBus Bus { get; set; }

        public void Handle(SendReplyFirstMessage message)
        {
            SendReplyVerifier.FirstMessageReceivedFrom.Add(message.Sender);
            Asserter.IsTrue("Secret" == message.EncryptedProperty, "Incorrect EncryptedProperty value");
            Bus.Reply(new SendReplySecondMessage
                {
                    Sender = TestRunner.EndpointName,
                    EncryptedProperty = "Secret"
                });
        }
    }
}