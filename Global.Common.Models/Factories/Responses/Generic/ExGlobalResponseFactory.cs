
namespace Global.Common.Models
{
    /// <summary>
    /// Factory class for creating instances of <see cref="ExGlobalResponse"/>.
    /// </summary>
    public static class ExGlobalResponseFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="ExGlobalResponse"/> with the status set to <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <returns>An instance of <see cref="ExGlobalResponse"/>.</returns>
        public static ExGlobalResponse CreateSuccessful()
        {
            return new ExGlobalResponse(ResponseStatus.Success);
        }

        /// <summary>
        /// Creates an instance of <see cref="ExGlobalResponse"/> with the status set to <see cref="ResponseStatus.Warning"/>.
        /// </summary>
        /// <returns>An instance of <see cref="ExGlobalResponse"/>.</returns>
        public static ExGlobalResponse CreateWarning()
        {
            return new ExGlobalResponse(ResponseStatus.Warning);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ExGlobalResponse"/> class with the status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <returns>An instance of <see cref="ExGlobalResponse"/>.</returns>
        public static ExGlobalResponse CreateFailure()
        {
            return new ExGlobalResponse(ResponseStatus.Failure);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ExGlobalResponse"/> class with the specified <paramref name="exception"/> 
        /// and the status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <param name="exception">The exception associated with the response.</param>
        /// <returns>An instance of <see cref="ExGlobalResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> is null.</exception>
        public static ExGlobalResponse CreateFailure(Exception exception)
        {
            return new ExGlobalResponse(exception);
        }

        #region CreateFrom methods

        /// <summary>
        /// Creates a new instance of the <see cref="ExGlobalResponse"/> class using the status and messages from the <paramref name="globalResponse"/> specified.
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="ExGlobalResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        public static ExGlobalResponse CreateFrom(IGlobalResponse globalResponse)
        {
            return new ExGlobalResponse(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ExGlobalResponse"/> class from the specified <paramref name="globalResponse"/>.
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="ExGlobalResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        public static ExGlobalResponse CreateFrom(IGlobalResponse<Exception> globalResponse)
        {
            return new ExGlobalResponse(globalResponse);
        }

        #endregion
    }
}
