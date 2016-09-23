using System.Collections.Generic;
using System.IO;
using System.Linq;

public class EndpointNames
{
    static EndpointNames()
    {
        var location = typeof(EndpointNames).Assembly.Location;
        var directoryName = Path.GetDirectoryName(location);
        var allMessagesAssemblies = Directory.GetFiles(directoryName,"*.Messages.dll");
        All = allMessagesAssemblies
            .Select(x => "WireCompat" + Path.GetFileNameWithoutExtension(x).Split('.')
                .First()).ToList();
    }
    public static List<string> All ;
}