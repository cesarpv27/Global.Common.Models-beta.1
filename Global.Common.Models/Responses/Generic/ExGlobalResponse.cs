
namespace Global.Common.Models
{
    /// <inheritdoc cref="IExGlobalResponse"/>
    public class ExGlobalResponse : GlobalResponse<Exception>, IExGlobalResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExGlobalResponse"/> class.
        /// </summary>
        protected ExGlobalResponse() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExGlobalResponse"/>  class with the specified <paramref name="status"/>.
        /// </summary>
        /// <param name="status">The status of the response.</param>
        public ExGlobalResponse(ResponseStatus status) : base(status) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExGlobalResponse"/> class with the specified <paramref name="exception"/> 
        /// and the status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <param name="exception">The exception associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public ExGlobalResponse(Exception exception) : base(exception) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExGlobalResponse"/> class using the status and messages from the <paramref name="globalResponse"/> specified.
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/> specified, but the message keys may have been modified with suffix.
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        protected internal ExGlobalResponse(IGlobalResponse globalResponse) : base(globalResponse) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExGlobalResponse"/> class using the status, exception 
        /// and messages from the <paramref name="globalResponse"/> specified.
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        protected internal ExGlobalResponse(IGlobalResponse<Exception> globalResponse) : base(globalResponse)
        {
            Exception = globalResponse.Exception;
        }

        #endregion
    }
}
