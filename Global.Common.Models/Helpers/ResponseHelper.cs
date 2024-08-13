
namespace Global.Common.Models.Helpers
{
    /// <summary>
    /// Helper class for response-related operations.
    /// </summary>
    public class ResponseHelper
    {
        /// <summary>
        /// Creates the default dictionary key for storing the exception message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The default dictionary key for storing the exception message.</returns>
        public static string CreateDefaultExceptionMessageKey(Exception exception)
        {
            return $"{exception.GetType().Name}{GlobalResponseConstants.DefaultExceptionMessageKeySuffix}";
        }

        /// <summary>
        /// Creates the default dictionary key for storing the exception stack trace.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The default dictionary key for storing the exception stack trace.</returns>
        public static string CreateDefaultExceptionStackTraceKey(Exception exception)
        {
            return $"{exception.GetType().Name}{GlobalResponseConstants.DefaultStackTraceKeySuffix}";
        }
    }
}
