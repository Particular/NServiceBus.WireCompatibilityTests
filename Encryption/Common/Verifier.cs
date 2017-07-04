using System.Collections.Concurrent;

public class Verifier
{
    public static void AssertExpectations()
    {
        foreach (var endpointName in EndpointNames.All)
        {
            FirstMessageReceivedFrom.VerifyContains(endpointName, $"{EndpointNames.EndpointName} expected a FirstMessage to be Received From {endpointName}");
            SecondMessageReceivedFrom.VerifyContains(endpointName, $"{EndpointNames.EndpointName} expected a SecondMessage to be Received From {endpointName}");
        }
    }

    public static ConcurrentBag<string> FirstMessageReceivedFrom = new ConcurrentBag<string>();
    public static ConcurrentBag<string> SecondMessageReceivedFrom = new ConcurrentBag<string>();
}