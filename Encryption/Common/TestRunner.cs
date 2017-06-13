using System;
using System.Threading;
using NServiceBus;

public static class TestRunner
{
    public static string EndpointName { get; set; }

    public static void RunTests(IBus bus)
    {
        Thread.Sleep(TimeSpan.FromSeconds(5));
        bus.InitiateSendReply();

        Thread.Sleep(TimeSpan.FromSeconds(30));
        var disposable = bus as IDisposable;
        disposable?.Dispose();
        SendReplyVerifier.AssertExpectations();
    }
}