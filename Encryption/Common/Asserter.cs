using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;

public static class Asserter
{
    public static Action<string> LogError;


    public static void IsTrue(bool value, string message)
    {
        if (value)
        {
            return;
        }
        LogError($"VERIFICATION-FAILED: {message}");
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