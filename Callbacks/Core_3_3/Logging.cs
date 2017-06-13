using log4net.Appender;
using log4net.Core;
using NServiceBus;


public static class Logging
{

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