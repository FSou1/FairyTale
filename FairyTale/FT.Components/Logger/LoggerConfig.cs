using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Filter;
using log4net.loggly;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace FT.Components.Logger {
    public class LoggerConfig {
        public static void Configure(string logglyInputKey) {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout {
                ConversionPattern = "%date [%thread] %-5level %logger - %message%newline"
            };
            patternLayout.ActivateOptions();

            LogglyAppender loggly = new LogglyAppender();
            loggly.AddFilter(new LoggerMatchFilter() {
                LoggerToMatch = "NHibernate.SQL",
                AcceptOnMatch = true
            });
            loggly.AddFilter(new LoggerMatchFilter()
            {
                LoggerToMatch = "NHibernate",
                AcceptOnMatch = false
            });
            loggly.RootUrl = "https://logs-01.loggly.com/";
            loggly.InputKey = logglyInputKey;
            loggly.Tag = "all";
            loggly.ActivateOptions();
            hierarchy.Root.AddAppender(loggly);

            hierarchy.Root.Level = Level.Debug;
            hierarchy.Configured = true;
        }
    }
}