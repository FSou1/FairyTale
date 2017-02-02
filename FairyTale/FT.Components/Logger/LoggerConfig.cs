using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace FT.Components.Logger {
    public class LoggerConfig {
        public static void Configure() {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();

            RollingFileAppender sqlRoller = new RollingFileAppender();
            sqlRoller.AddFilter(new LoggerMatchFilter() {
                LoggerToMatch = "NHibernate.SQL",
                AcceptOnMatch = true
            });
            sqlRoller.AddFilter(new DenyAllFilter());
            sqlRoller.AppendToFile = true;
            sqlRoller.File = $@"log/sql.txt";
            sqlRoller.Layout = patternLayout;
            sqlRoller.MaxSizeRollBackups = 5;
            sqlRoller.MaximumFileSize = "100MB";
            sqlRoller.RollingStyle = RollingFileAppender.RollingMode.Size;
            sqlRoller.StaticLogFileName = true;
            sqlRoller.ActivateOptions();
            hierarchy.Root.AddAppender(sqlRoller);

            RollingFileAppender logRoller = new RollingFileAppender();
            logRoller.AddFilter(new LoggerMatchFilter() {
                LoggerToMatch = "NHibernate",
                AcceptOnMatch = false
            });
            logRoller.AppendToFile = true;
            logRoller.File = $@"log/log.txt";
            logRoller.Layout = patternLayout;
            logRoller.MaxSizeRollBackups = 5;
            logRoller.MaximumFileSize = "100MB";
            logRoller.RollingStyle = RollingFileAppender.RollingMode.Size;
            logRoller.StaticLogFileName = true;
            logRoller.ActivateOptions();
            hierarchy.Root.AddAppender(logRoller);

            hierarchy.Root.Level = Level.Debug;
            hierarchy.Configured = true;
        }
    }
}