using CommonMessages;
using NServiceBus;

namespace Common.DataBus
{
    public class ResponseHandler : IHandleMessages<DataBusResponseMessage>
    {
        public IBus Bus { get; set; }

        public void Handle(DataBusResponseMessage message)
        {
            DataBusVerifier.ResponseReceivedFromSites.Add(message.Sender);
            Asserter.IsTrue(message.PropertyDataBus != null, "Incorrect property value");
            Asserter.IsTrue("Secret" == message.EncryptedProperty, "Incorrect property value");
        }
    }
}