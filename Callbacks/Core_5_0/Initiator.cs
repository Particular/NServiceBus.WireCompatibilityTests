using System.Threading.Tasks;
using NServiceBus;

public static class Initiator
{

    public static void Initiate(this IBus bus)
    {
        Parallel.ForEach(EndpointNames.All, endpointName =>
        {
            var remoteName = endpointName;
            bus.Send(endpointName, new Message())
                .Register<int>(i =>
                {
                    Asserter.IsTrue(5 == i, "Incorrect property value");
                    Verifier.ReplyReceivedFrom.Add(remoteName);
                });
        });
    }
}