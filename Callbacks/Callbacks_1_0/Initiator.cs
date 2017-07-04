using System.Threading.Tasks;
using NServiceBus;

public static class Initiator
{

    public static void Initiate(this IEndpointInstance bus)
    {
        foreach (var endpoint in EndpointNames.All)
        {
            var remoteName = endpoint;
            Task.Run(() =>
            {
                var sendOptions = new SendOptions();
                sendOptions.SetDestination(remoteName);

                var intResult = bus.Request<int>(new IntMessage(), sendOptions).Result;
                Asserter.IsTrue(5 == intResult, "Incorrect int value");
                Verifier.IntReplyReceivedFrom.Add(remoteName);

                var enumResult = bus.Request<CustomEnum>(new EnumMessage(), sendOptions).Result;
                Asserter.IsTrue(CustomEnum.Value2 == enumResult, "Incorrect enum value");
                Verifier.EnumReplyReceivedFrom.Add(remoteName);
            });
        }
    }
}