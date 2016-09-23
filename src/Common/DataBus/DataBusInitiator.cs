using System.Threading.Tasks;
using CommonMessages;
using NServiceBus;

public static class DataBusInitiator
{

    public static void InitiateDataBus(this IBus bus)
    {
        Parallel.ForEach(EndpointNames.All, endpointName =>
        {
            var sendMessage = new DataBusSendMessage
            {
                PropertyDataBus = new byte[10],
                EncryptedProperty = "Secret",
                SentFrom = TestRunner.EndpointName
            };
            bus.Send(endpointName, sendMessage);
        });
    }
}