using System.Threading.Tasks;
using CommonMessages;
using NServiceBus;

public static class SendReturnInitiator
{

    public static void InitiateSendReturn(this IEndpointInstance bus)
    {
        foreach (var endpoint in EndpointNames.All)
        {
            Task.Run(() =>
            {
                var sendOptions = new SendOptions();
                sendOptions.SetDestination(endpoint);

                var result = bus.Request<int>(new SendReturnMessage(), sendOptions).Result;
                Asserter.IsTrue(5 == result, "Incorrect property value");
                SendReturnVerifier.ReplyReceivedFrom.Add(endpoint);
            });
        }
    }
}