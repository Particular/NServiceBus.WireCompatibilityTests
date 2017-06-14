using System.Collections.Concurrent;
using System.Linq;

public class SagaVerifier
{

    public static void AssertExpectations()
    {
        foreach (var endpointName in EndpointNames.All)
        {
            RequestingSagaGotTheResponse.VerifyContains(endpointName, $"{TestRunner.EndpointName} expected Requesting Saga Got The Response From {endpointName}");
        }
    }

    public static bool IsFinished()
    {
        return EndpointNames.All.All(endpointName => RequestingSagaGotTheResponse.Contains(endpointName));
    }

    public static ConcurrentBag<string> RequestingSagaGotTheResponse = new ConcurrentBag<string>();
}