using System.Threading.Tasks;
using NServiceBus;

public static class Initiator
{
    public static void Initiate(this IBus bus)
    {
        Parallel.ForEach(EndpointNames.All, endpointName =>
        {
            bus.Send(endpointName, new FirstMessage
            {
                Sender = EndpointNames.EndpointName,
                EncryptedProperty = "Secret"
            });
        });
    }
}