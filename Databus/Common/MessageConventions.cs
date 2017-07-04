using System;

public static class MessageConventions
{
    public static bool IsMessage(this Type type)
    {
        return type.Namespace != null &&
               type.Namespace.StartsWith("CommonMessages") &&
               type.Name.EndsWith("Message");
    }
}