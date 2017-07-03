using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public class EndpointNames
{
    public static string EndpointName = $"WireCompat{Assembly.GetEntryAssembly().GetName().Name}";

    static EndpointNames()
    {
        var location = typeof(EndpointNames).Assembly.Location;
        var directoryName = Path.GetDirectoryName(location);
        var allMessagesAssemblies = Directory.GetFiles(directoryName, "*.Messages.dll");
        All = allMessagesAssemblies
            .Select(x => $"WireCompat{Path.GetFileNameWithoutExtension(x).Split('.').First()}").ToList();
    }

    public static List<string> All;
}