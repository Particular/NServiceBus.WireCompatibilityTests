using System.Threading.Tasks;
using NServiceBus;

public static class Initiator
{

    public static void Initiate(this IEndpointInstance bus)
    {
        foreach (var endpoint in EndpointNames.All)
        {
            Task.Run(() =>
            {
                var sendOptions = new SendOptions();
                sendOptions.SetDestination(endpoint);

                var result = bus.Request<int>(new Message(), sendOptions).Result;
                Asserter.IsTrue(5 == result, "Incorrect property value");
                Verifier.ReplyReceivedFrom.Add(endpoint);
            });
        }
    }
}