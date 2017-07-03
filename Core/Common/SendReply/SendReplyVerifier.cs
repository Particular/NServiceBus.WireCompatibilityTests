using System.Collections.Concurrent;
using System.Linq;

public class SendReplyVerifier
{
    public static void AssertExpectations()
    {
        foreach (var endpointName in EndpointNames.All)
        {
            FirstMessageReceivedFrom.VerifyContains(endpointName, $"{EndpointNames.EndpointName} expected a FirstMessage to be Received From {endpointName}");
            SecondMessageReceivedFrom.VerifyContains(endpointName, $"{EndpointNames.EndpointName} expected a SecondMessage to be Received From {endpointName}");
        }
    }

    public static bool IsFinished()
    {
        foreach (var endpointName in EndpointNames.All)
        {
            if (!FirstMessageReceivedFrom.Contains(endpointName))
            {
                return false;
            }
            if (!SecondMessageReceivedFrom.Contains(endpointName))
            {
                return false;
            }
        }
        return true;
    }

    public static ConcurrentBag<string> FirstMessageReceivedFrom = new ConcurrentBag<string>();
    public static ConcurrentBag<string> SecondMessageReceivedFrom = new ConcurrentBag<string>();
}