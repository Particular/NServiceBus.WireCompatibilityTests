using System.Threading.Tasks;
using NServiceBus;

public class ResponseHandler : IHandleMessages<ResponseMessage>
{
    public Task Handle(ResponseMessage message, IMessageHandlerContext context)
    {
        Verifier.ResponseReceivedFromSites.Add(message.Sender);
        Asserter.IsTrue(message.PropertyDataBus != null, "Incorrect property value");
        return Task.FromResult(0);
    }
}