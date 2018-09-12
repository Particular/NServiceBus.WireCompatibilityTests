using System.Threading.Tasks;
using NServiceBus;

public static class Initiator
{
    public static async Task Initiate(this IEndpointInstance bus)
    {
        foreach (var endpoint in EndpointNames.All)
        {
            await bus.Send(endpoint, new FirstMessage
                {
                    Sender = EndpointNames.EndpointName,
                    EncryptedProperty = "Secret"
                }).ConfigureAwait(false);
        }
    }
}