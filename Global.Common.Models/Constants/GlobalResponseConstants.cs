
namespace Global.Common.Models.Constants
{
    /// <summary>
    /// Constants related to responses.
    /// </summary>
    public static class GlobalResponseConstants
    {
        /// <summary>
        /// Gets the default dictionary key for storing the default response stack trace.
        /// </summary>
        public static string ResponseStackTraceKey => $"Response{DefaultStackTraceKeySuffix}";

        /// <summary>
        /// Gets the default amount of messages.
        /// </summary>
        public static int DefaultMessageAmount => 1;

        /// <summary>
        /// Gets the default dictionary key suffix for the exception message.
        /// </summary>
        public static string DefaultExceptionMessageKeySuffix => "_Message";

        /// <summary>
        /// Gets the default dictionary key suffix for the exception stack trace.
        /// </summary>
        public static string DefaultStackTraceKeySuffix => "_StackTrace";

        /// <summary>
        /// Gets the default dictionary key suffix for the object type message.
        /// </summary>
        public static string DefaultTypeMessageKeySuffix => "_Type";

        /// <summary>
        /// Gets the default message when exception stack trace is undefined.
        /// </summary>
        public static string DefaultExceptionStackTraceUndefinedMessage => "The stack trace of the exception is undefined";

        /// <summary>
        /// Gets the default message when type full name is undefined.
        /// </summary>
        public static string DefaultTypeFullNameUndefinedMessage => "The full name of the type is undefined";

        /// <summary>
        /// Gets the default message when value to string operation is undefined.
        /// </summary>
        public static string DefaultValueToStringUndefinedMessage => "The value to string operation is undefined";

        /// <summary>
        /// Generates a message indicating that the status is Failure and a value is specified.
        /// </summary>
        /// <param name="statusParamName">The parameter name for the status.</param>
        /// <param name="valueParamName">The parameter name for the value.</param>
        /// <returns>A message indicating that the status is Failure and a value is specified.</returns>
        public static string ResponseStatusFailureAndValueSpecified(string statusParamName, string valueParamName)
        {
            return $"The {statusParamName.WrapInSingleQuotationMarks()} is {nameof(ResponseStatus.Failure).WrapInSingleQuotationMarks()} and the {valueParamName.WrapInSingleQuotationMarks()} is specified.";
        }

        /// <summary>
        /// Generates a message indicating that the Status in the response is not Failure.
        /// </summary>
        /// <param name="responseParamName">The parameter name for the response.</param>
        /// <returns>A message indicating that the Status in the response is not Failure.</returns>
        public static string ResponseStatusIsNotFailure(string responseParamName)
        {
            return $"The Status in the {responseParamName} is not {Enum.GetName(typeof(ResponseStatus), ResponseStatus.Failure)}";
        }
    }
}
