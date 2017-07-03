using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;

public static class Asserter
{
#if (Version5 || Version6 || Version7)
    static NServiceBus.Logging.ILog log = NServiceBus.Logging.LogManager.GetLogger("Asserter");
#else
    static log4net.ILog log = log4net.LogManager.GetLogger("Asserter");
#endif

    public static void IsTrue(bool value, string message)
    {
        if (value)
        {
            return;
        }
        log.Error($"VERIFICATION-FAILED: {message}");
        if (Debugger.IsAttached)
        {
            throw new Exception(message);
        }
    }

    public static void VerifyContains(this ConcurrentBag<string> existing, string expecting, string message)
    {
        var list = existing.ToList();
        IsTrue(list.Any(x => x.ToLowerInvariant() == expecting.ToLowerInvariant()), message);
    }
}