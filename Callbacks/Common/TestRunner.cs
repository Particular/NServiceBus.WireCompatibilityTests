using System;
using System.Threading;
using NServiceBus;

public static class TestRunner
{
    public static string EndpointName { get; set; }

    public static void RunTests(IBus bus)
    {
        Thread.Sleep(TimeSpan.FromSeconds(25));
        bus.InitiateSendReturn();

        Thread.Sleep(TimeSpan.FromMinutes(1));
        var disposable = bus as IDisposable;
        disposable?.Dispose();
        SendReturnVerifier.AssertExpectations();
    }
}