using System.Threading.Tasks;
using NServiceBus;

public static class Initiator
{

    public static async Task Initiate(this IEndpointInstance bus)
    {
        foreach (var endpointName in EndpointNames.All)
        {
            var sendMessage = new SendMessage
                {
                    PropertyDataBus = new byte[10],
                    SentFrom = EndpointNames.EndpointName
            };
            await bus.Send(endpointName, sendMessage).ConfigureAwait(false);
        }
    }
}