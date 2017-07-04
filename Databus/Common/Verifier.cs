using System.Collections.Concurrent;
using System.Linq;

public class Verifier
{
    public static void AssertExpectations()
    {
        foreach (var endpointName in EndpointNames.All)
        {
            ResponseReceivedFromSites.VerifyContains(endpointName, $"{EndpointNames.EndpointName} expected to receive a send from site {endpointName}");
            SendReceivedFromSites.VerifyContains(endpointName, $"{EndpointNames.EndpointName} expected to receive a response from site {endpointName}");
        }
    }

    public static bool IsFinished()
    {
        foreach (var endpointName in EndpointNames.All)
        {
            if (!ResponseReceivedFromSites.Contains(endpointName))
            {
                return false;
            }
            if (!SendReceivedFromSites.Contains(endpointName))
            {
                return false;
            }
        }
        return true;
    }

    public static ConcurrentBag<string> SendReceivedFromSites = new ConcurrentBag<string>();
    public static ConcurrentBag<string> ResponseReceivedFromSites = new ConcurrentBag<string>();
}