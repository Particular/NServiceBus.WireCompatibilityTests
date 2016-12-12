using System.Threading.Tasks;
using CommonMessages;
using NServiceBus;

namespace Common.DataBus
{
    public class ResponseHandler : IHandleMessages<DataBusResponseMessage>
    {
        public Task Handle(DataBusResponseMessage message, IMessageHandlerContext context)
        {
            DataBusVerifier.ResponseReceivedFromSites.Add(message.Sender);
            Asserter.IsTrue(message.PropertyDataBus != null, "Incorrect property value");
            Asserter.IsTrue("Secret" == message.EncryptedProperty, "Incorrect property value");

            return Task.FromResult(0);
        }
    }
}