using System;
using NServiceBus;

public static class MessageConventions
{
#if(Version3 || Version4)

    public static void ApplyMessageConventions(this Configure configure)
    {
        configure.DefiningMessagesAs(t => IsMessageNamespace(t) && t.Name.EndsWith("Message"));
    }

#elif(Version5)

    public static void ApplyMessageConventions(this ConventionsBuilder b)
    {
        b.DefiningMessagesAs(t => IsMessageNamespace(t) && t.Name.EndsWith("Message"));
    }

#elif(Version6 || Version7)

    public static void ApplyMessageConventions(this ConventionsBuilder b)
    {
        b.DefiningMessagesAs(t => IsMessageNamespace(t) && t.Name.EndsWith("Message"));
    }

#endif

    static bool IsMessageNamespace(Type t)
    {
        return t.Namespace != null && (t.Namespace.StartsWith("CommonMessages") || t.Namespace.Contains("Messages"));
    }
}