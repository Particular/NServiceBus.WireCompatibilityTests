using System;
using System.Reflection;

public static class MessageConventions
{
    public static bool IsMessage(Type type)
    {
        return type.Assembly.GetName().Name == "Common" &&
               type.Name.EndsWith("Message");
    }

    public static bool IsEncryptedProperty(PropertyInfo propertyInfo)
    {
        return propertyInfo.Name.StartsWith("Encrypted");
    }

}