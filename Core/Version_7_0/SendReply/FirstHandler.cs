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
            return context.Reply(new SendReplySecondMessage
                {
                    Sender = EndpointNames.EndpointName
            });
        }
    }
}