using System;
using System.Threading;
using NServiceBus;

public static class TestRunner
{
    public static void RunTests(IBus bus)
    {
        Thread.Sleep(TimeSpan.FromSeconds(25));
        bus.InitiateSendReply();

        Thread.Sleep(TimeSpan.FromMinutes(2));
        var disposable = bus as IDisposable;
        disposable?.Dispose();
        SendReplyVerifier.AssertExpectations();
    }
}