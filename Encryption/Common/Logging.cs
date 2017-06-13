using log4net.Appender;
using log4net.Core;

#if(Version3 || Version4)

    using NServiceBus;

#elif(Version5)

using NServiceBus.Log4Net;

#endif

public static class Logging
{
#if(Version3 || Version4)

    public static void ConfigureLogging()
    {
        SetLoggingLibrary.Log4Net<RollingFileAppender>(null,
            a =>
            {
                a.CountDirection = 1;
                a.DatePattern = "yyyy-MM-dd";
                a.RollingStyle = RollingFileAppender.RollingMode.Composite;
                a.MaxFileSize = 1024 * 1024;
                a.MaxSizeRollBackups = 10;
                a.LockingModel = new FileAppender.MinimalLock();
                a.StaticLogFileName = true;
                a.File = "logfile.txt";
                a.AppendToFile = true;
                a.ImmediateFlush = true;
            });

        SetLoggingLibrary.Log4Net<ColoredConsoleAppender>(null,
            a =>
            {
                PrepareColors(a);
                a.Threshold = Level.Info;
            }
            );
    }

#elif(Version5)

    public static void ConfigureLogging()
    {
        var pl = new log4net.Layout.PatternLayout
        {
            ConversionPattern = "%d [%t] %-5p %c [%x] <%X{auth}> - %m%n"
        };
        pl.ActivateOptions();

        var fileAppender = new RollingFileAppender
        {
            CountDirection = 1,
            DatePattern = "yyyy-MM-dd",
            RollingStyle = RollingFileAppender.RollingMode.Composite, 
            MaxFileSize = 1024*1024,
            MaxSizeRollBackups = 10,
            LockingModel = new FileAppender.MinimalLock(),
            StaticLogFileName = true,
            File = "logfile.txt", 
            AppendToFile = true,
            ImmediateFlush = true, 
            Layout = pl
        };
        fileAppender.ActivateOptions();

        var consoleAppender = new ColoredConsoleAppender();
        PrepareColors(consoleAppender);
        consoleAppender.Threshold = Level.Info;
        consoleAppender.Layout = pl;
        consoleAppender.ActivateOptions();

        ((Hierarchy)LogManager.GetRepository()).Root.RemoveAllAppenders();

        BasicConfigurator.Configure(fileAppender, consoleAppender);
        NServiceBus.Logging.LogManager.Use<Log4NetFactory>();
    }

#endif

    static void PrepareColors(ColoredConsoleAppender a)
    {
        var mapping = new ColoredConsoleAppender.LevelColors
            {
                Level = Level.Debug,
                ForeColor = ColoredConsoleAppender.Colors.White
            };
        a.AddMapping(mapping);
        var colors2 = new ColoredConsoleAppender.LevelColors
            {
                Level = Level.Info,
                ForeColor = ColoredConsoleAppender.Colors.Green
            };
        a.AddMapping(colors2);
        var colors3 = new ColoredConsoleAppender.LevelColors
            {
                Level = Level.Warn,
                ForeColor = ColoredConsoleAppender.Colors.HighIntensity | ColoredConsoleAppender.Colors.Yellow
            };
        a.AddMapping(colors3);
        var colors4 = new ColoredConsoleAppender.LevelColors
            {
                Level = Level.Error,
                ForeColor = ColoredConsoleAppender.Colors.HighIntensity | ColoredConsoleAppender.Colors.Red
            };
        a.AddMapping(colors4);
    }
}