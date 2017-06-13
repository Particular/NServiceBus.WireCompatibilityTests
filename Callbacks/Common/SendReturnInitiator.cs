using System.Threading.Tasks;
using CommonMessages;
using NServiceBus;

public static class SendReturnInitiator
{

    public static void InitiateSendReturn(this IBus bus)
    {
        Parallel.ForEach(EndpointNames.All, endpointName =>
        {
            var remoteName = endpointName;
            bus.Send(endpointName, new SendReturnMessage())
                .Register<int>(i =>
                {
                    Asserter.IsTrue(5 == i, "Incorrect property value");
                    SendReturnVerifier.ReplyReceivedFrom.Add(remoteName);
                });
        });
    }
}