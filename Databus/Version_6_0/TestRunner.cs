using System;
using System.Threading.Tasks;
using NServiceBus;

public static class TestRunner
{

    public static async Task RunTests(IEndpointInstance bus)
    {
        await Task.Delay(TimeSpan.FromSeconds(10)).ConfigureAwait(false);
        await bus.InitiateDataBus().ConfigureAwait(false);

        for (var i = 0; i < 10; i++)
        {
            await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
            if (DataBusVerifier.IsFinished())
            {
                break;
            }
        }

        await bus.Stop().ConfigureAwait(false);
        DataBusVerifier.AssertExpectations();
    }
}