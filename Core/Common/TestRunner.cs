using System;
using System.Threading;
using NServiceBus;

public static class TestRunner
{

    public static void RunTests(IBus bus)
    {
        Thread.Sleep(TimeSpan.FromSeconds(25));
        bus.InitiatePubSub();
        bus.InitiateSaga();
        bus.InitiateSendReply();

        for (var i = 0; i < 10; i++)
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));
            if (
                PubSubVerifier.IsFinished() &&
                SagaVerifier.IsFinished() &&
                SendReplyVerifier.IsFinished())
            {
                break;
            }
        }

        var disposable = bus as IDisposable;
        disposable?.Dispose();
        PubSubVerifier.AssertExpectations();
        SagaVerifier.AssertExpectations();
        SendReplyVerifier.AssertExpectations();
    }
}