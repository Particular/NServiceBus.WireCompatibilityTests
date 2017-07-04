using System;
using System.Threading;
using NServiceBus;

public static class TestRunner
{

    public static void RunTests(IBus bus)
    {
        Thread.Sleep(TimeSpan.FromSeconds(10));
        bus.Initiate();

        for (var i = 0; i < 10; i++)
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            if (Verifier.IsFinished())
            {
                break;
            }
        }

        var disposable = bus as IDisposable;
        disposable?.Dispose();
        Verifier.AssertExpectations();
    }
}