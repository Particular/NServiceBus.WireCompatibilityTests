using System;
using System.Threading;
using NServiceBus;

public static class TestRunner
{

    public static void RunTests(IBus bus)
    {
        Thread.Sleep(TimeSpan.FromSeconds(10));
        bus.InitiateDataBus();

        for (var i = 0; i < 10; i++)
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            if (DataBusVerifier.IsFinished())
            {
                break;
            }
        }

        var disposable = bus as IDisposable;
        disposable?.Dispose();
        DataBusVerifier.AssertExpectations();
    }
}