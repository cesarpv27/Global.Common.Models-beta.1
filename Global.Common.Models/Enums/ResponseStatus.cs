
namespace Global.Common.Models
{
    /// <summary>
    /// Represents the status of a response.
    /// </summary>
    public enum ResponseStatus
    {
        /// <summary>
        /// Indicates a successful response.
        /// </summary>
        Success = 200,

        /// <summary>
        /// Indicates a successful response but with warnings.
        /// </summary>
        Warning = 300,

        /// <summary>
        /// Indicates a failure response.
        /// </summary>
        Failure = 500,
    }
}
