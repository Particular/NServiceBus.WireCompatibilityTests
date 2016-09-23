using System.Threading.Tasks;
using CommonMessages;
using NServiceBus;

public static class SagaInitiator
{
    public static async Task InitiateSaga(this IEndpointInstance bus)
    {
        foreach (var endpoint in EndpointNames.All)
        {
            await bus.SendLocal(new SagaInitiateRequestingMessage
                {
                    TargetEndpoint = endpoint
                }).ConfigureAwait(false);
        }
    }
}