using System.Threading.Tasks;
using CommonMessages;
using NServiceBus;

public static class SagaInitiator
{
    public static void InitiateSaga(this IBus bus)
    {
        Parallel.ForEach(EndpointNames.All, endpoint =>
        {
            bus.SendLocal(new SagaInitiateRequestingMessage
            {
                TargetEndpoint = endpoint
            });
        });
    }
}