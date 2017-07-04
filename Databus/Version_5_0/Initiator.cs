using System.Threading.Tasks;
using NServiceBus;

public static class Initiator
{

    public static void Initiate(this IBus bus)
    {
        Parallel.ForEach(EndpointNames.All, endpointName =>
        {
            var sendMessage = new SendMessage
            {
                PropertyDataBus = new byte[10],
                SentFrom = EndpointNames.EndpointName
            };
            bus.Send(endpointName, sendMessage);
        });
    }
}