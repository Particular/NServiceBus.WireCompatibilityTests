using CommonMessages;
using NServiceBus;

namespace Common.SendReply
{
    public class SecondHandler : IHandleMessages<SendReplySecondMessage>
    {
        public IBus Bus { get; set; }
        public void Handle(SendReplySecondMessage message)
        {
            SendReplyVerifier.SecondMessageReceivedFrom.Add(message.Sender);
            Asserter.IsTrue("Secret" == message.EncryptedProperty, "Incorrect EncryptedProperty value");
        }
    }
}