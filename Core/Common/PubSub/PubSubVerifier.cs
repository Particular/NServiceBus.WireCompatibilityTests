using System.Collections.Concurrent;
using System.Linq;

public class PubSubVerifier
{

    public static void AssertExpectations()
    {
        foreach (var endpointName in EndpointNames.All)
        {
            Asserter.IsTrue(EventReceivedFrom.Contains(endpointName), $"{EndpointNames.EndpointName} expected a event to be Received From {endpointName}");
        }
    }
    public static bool IsFinished()
    {
        return EndpointNames.All.All(endpointName => EventReceivedFrom.Contains(endpointName));
    }

    public static ConcurrentBag<string> EventReceivedFrom = new ConcurrentBag<string>();

}