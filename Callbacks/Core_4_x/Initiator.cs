using System.Threading.Tasks;
using NServiceBus;

public static class Initiator
{

    public static void Initiate(this IBus bus)
    {
        Parallel.ForEach(EndpointNames.All, endpointName =>
        {
            var remoteName = endpointName;

            bus.Send(remoteName, new IntMessage())
                .Register<int>(i =>
                {
                    Asserter.IsTrue(5 == i, "Incorrect int value");
                    Verifier.IntReplyReceivedFrom.Add(remoteName);
                });

            bus.Send(remoteName, new EnumMessage())
                .Register<CustomEnum>(status =>
                {
                    Asserter.IsTrue(CustomEnum.Value2 == status, "Incorrect enum value");
                    Verifier.EnumReplyReceivedFrom.Add(remoteName);
                });
        });
    }
}