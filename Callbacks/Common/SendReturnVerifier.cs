using System.Collections.Concurrent;

public class SendReturnVerifier
{

    public static void AssertExpectations()
    {
        foreach (var endpointName in EndpointNames.All)
        {
            ReplyReceivedFrom.VerifyContains(endpointName, $"{TestRunner.EndpointName} expected a reply to be Received From {endpointName}");
        }
    }

    public static ConcurrentBag<string> ReplyReceivedFrom = new ConcurrentBag<string>();
}