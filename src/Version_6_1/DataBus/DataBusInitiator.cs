using System.Threading.Tasks;
using CommonMessages;
using NServiceBus;

public static class DataBusInitiator
{

    public static async Task InitiateDataBus(this IEndpointInstance bus)
    {
        foreach (var endpointName in EndpointNames.All)
        {
            var sendMessage = new DataBusSendMessage
                {
                    PropertyDataBus = new byte[10],
                    EncryptedProperty = "Secret",
                    SentFrom = TestRunner.EndpointName
                };
            await bus.Send(endpointName, sendMessage).ConfigureAwait(false);
        }
    }
}