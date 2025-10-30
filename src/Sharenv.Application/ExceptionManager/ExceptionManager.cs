using Microsoft.Extensions.Logging;
using Sharenv.Domain.Exceptions;

namespace Sharenv.Application
{
    public class ExceptionManager : IExceptionManager
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public ExceptionManager()
        {

        }

        /// <summary>
        /// Handle exception via domain rules
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="customMessage"></param>
        public void HandleException(Exception ex, string? customMessage = null)
        {
            var exceptionType = ex.GetType();
            var message = customMessage ?? ex.Message;

            if (exceptionType == typeof(SharenvMessageException))
            {
                Core.DefaultLogger.LogTrace(ex, message);
            }
            else if (exceptionType == typeof(SharenvCriticalException))
            {
                HandleCriticalException(ex);
            }
            else
            {
                Core.DefaultLogger.LogError(ex, message);
            }
        }

        /// <summary>
        /// Handle critical exception
        /// </summary>
        /// <param name="ex"></param>
        public void HandleCriticalException(Exception ex)
        {
            Core.DefaultLogger.LogCritical(ex, ex.Message);
            // Can send notification
        }
    }
}
