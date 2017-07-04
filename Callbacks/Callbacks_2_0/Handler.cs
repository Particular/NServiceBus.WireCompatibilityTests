using System.Threading.Tasks;
using CommonMessages;
using NServiceBus;

public class Handler : IHandleMessages<SendReturnMessage>
{
    public Task Handle(SendReturnMessage message, IMessageHandlerContext context)
    {
        return context.Reply(5);
    }
}