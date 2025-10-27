using Microsoft.Extensions.Logging;

namespace Sharenv
{
    public static class Core
    {
        /// <summary>
        /// Gets default logger instance
        /// </summary>
        public static ILogger DefaultLogger { get; private set; }

        /// <summary>
        /// Gets exception manager
        /// </summary>
        public static IExceptionManager ExceptionManager { get; private set; }

        /// <summary>
        /// Register default logger instance
        /// </summary>
        /// <param name="implementation"></param>
        public static void RegisterLogger(ILogger implementation)
        {
            DefaultLogger = implementation;
        }

        /// <summary>
        /// Register default exception manager
        /// </summary>
        /// <param name="implementation"></param>
        public static void RegisterExceptionManager(IExceptionManager implementation)
        {
            ExceptionManager = implementation;
        }
    }
}
