using System.Threading.Tasks;
using CommonMessages;
using NServiceBus;

public static class SendReturnInitiator
{

    public static async Task InitiateSendReturn(this IEndpointInstance bus)
    {
        foreach (var endpoint in EndpointNames.All)
        {
            var sendOptions = new SendOptions();
            sendOptions.SetDestination(endpoint);

            var result = await bus.Request<int>(new SendReturnMessage(), sendOptions).ConfigureAwait(false);
            Asserter.IsTrue(5 == result, "Incorrect property value");
            SendReturnVerifier.ReplyReceivedFrom.Add(endpoint);
        }
    }
}