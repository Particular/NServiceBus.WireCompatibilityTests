using System;
using NServiceBus;

public static class MessageConventions
{
#if(Version3 || Version4)

    public static void ApplyMessageConventions(this Configure configure)
    {
        configure.DefiningMessagesAs(t => IsMessageNamespace(t) && t.Name.EndsWith("Message"));
        configure.DefiningDataBusPropertiesAs(p => p.Name.EndsWith("DataBus"));
    }

#elif(Version5)

    public static void ApplyMessageConventions(this ConventionsBuilder b)
    {
        b.DefiningMessagesAs(t => IsMessageNamespace(t) && t.Name.EndsWith("Message"));
        b.DefiningDataBusPropertiesAs(p => p.Name.EndsWith("DataBus"));
    }

#elif(Version6 || Version7)

    public static void ApplyMessageConventions(this ConventionsBuilder b)
    {
        b.DefiningMessagesAs(t => IsMessageNamespace(t) && t.Name.EndsWith("Message"));
        b.DefiningDataBusPropertiesAs(p => p.Name.EndsWith("DataBus"));
    }

#endif

    static bool IsMessageNamespace(Type t)
    {
        return t.Namespace != null && t.Namespace.StartsWith("CommonMessages");
    }
}