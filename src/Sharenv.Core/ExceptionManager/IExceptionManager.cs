
namespace Sharenv
{
    public interface IExceptionManager
    {
        /// <summary>
        /// Logs an exception with optional custom context.
        /// </summary>
        public void HandleException(Exception ex, string? customMessage = null);

        /// <summary>
        /// Logs and handles a critical error, often used for system-level failures.
        /// </summary>
        public void HandleCriticalException(Exception ex);
    }
}
