using System;
using NServiceBus;

public static class MessageConventions
{
#if(Version3 || Version4)

    public static void ApplyMessageConventions(this Configure configure)
    {
        configure.DefiningCommandsAs(t => IsMessageNamespace(t) && t.Name.EndsWith("Command"));
        configure.DefiningEventsAs(t => IsMessageNamespace(t) && t.Name.EndsWith("Event"));
        configure.DefiningMessagesAs(t => IsMessageNamespace(t) && t.Name.EndsWith("Message"));
        configure.DefiningEncryptedPropertiesAs(p => p.Name.StartsWith("Encrypted"));
        configure.DefiningDataBusPropertiesAs(p => p.Name.EndsWith("DataBus"));
    }

#elif(Version5 || Version6)

    public static void ApplyMessageConventions(this ConventionsBuilder b)
    {
        b.DefiningCommandsAs(t => IsMessageNamespace(t) && t.Name.EndsWith("Command"));
        b.DefiningEventsAs(t => IsMessageNamespace(t) && t.Name.EndsWith("Event"));
        b.DefiningMessagesAs(t => IsMessageNamespace(t) && t.Name.EndsWith("Message"));
        b.DefiningEncryptedPropertiesAs(p => p.Name.StartsWith("Encrypted"));
        b.DefiningDataBusPropertiesAs(p => p.Name.EndsWith("DataBus"));
    }

#endif

    static bool IsMessageNamespace(Type t)
    {
        return t.Namespace != null && (t.Namespace.StartsWith("CommonMessages") || t.Namespace.Contains("Messages"));
    }
}