
namespace Global.Common.Models
{
    /// <inheritdoc cref="IExGlobalValueResponse{T}"/>
    public class ExGlobalValueResponse<T> : GlobalValueResponse<T, Exception>, IExGlobalValueResponse<T>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExGlobalValueResponse{T}"/> class.
        /// </summary>
        protected ExGlobalValueResponse() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExGlobalValueResponse{T}"/> class with the specified <paramref name="status"/>.
        /// </summary>
        protected ExGlobalValueResponse(ResponseStatus status) : base(status) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExGlobalValueResponse{T}"/> class with the specified <paramref name="exception"/> 
        /// and the status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <param name="exception">The exception associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public ExGlobalValueResponse(Exception exception) : base(exception) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExGlobalValueResponse{T}"/> class with the specified <paramref name="status"/> 
        /// and <paramref name="value"/>.
        /// If the <paramref name="status"/> is <see cref="ResponseStatus.Success"/>, 
        /// then an <paramref name="value"/> must be specified.
        /// If the <paramref name="status"/> is <see cref="ResponseStatus.Success"/> 
        /// and the <paramref name="value"/> was not specified, an <see cref="ArgumentNullException"/> will be thrown.
        /// If the <paramref name="status"/> is <see cref="ResponseStatus.Failure"/>, 
        /// then an <paramref name="value"/> must be null.
        /// If the <paramref name="status"/> is <see cref="ResponseStatus.Failure"/> and the <paramref name="value"/> was specified, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="status">The status of the response.</param>
        /// <param name="value">The value associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="status"/> is <see cref="ResponseStatus.Success"/> 
        /// and the <paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="status"/> is <see cref="ResponseStatus.Failure"/> 
        /// and the <paramref name="value"/> was specified.</exception>
        public ExGlobalValueResponse(ResponseStatus status, T? value) : base(status, value) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExGlobalValueResponse{T}"/> class from the data in <paramref name="globalValueResponse"/>. 
        /// The new instance contains the same status, value, exception and messages of the <paramref name="globalValueResponse"/>, 
        /// but the message keys may have been modified with suffix.
        /// </summary>
        /// <param name="globalValueResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalValueResponse"/> is null.</exception>
        protected internal ExGlobalValueResponse(IGlobalValueResponse<T, Exception> globalValueResponse) : this()
        {
            AssertHelper.AssertNotNullOrThrow(globalValueResponse, nameof(globalValueResponse));

            Status = globalValueResponse.Status;
            Value = globalValueResponse.Value;
            Exception = globalValueResponse.Exception;

            AddMessagesFrom(globalValueResponse, KeyExistAction.Rename);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExGlobalValueResponse{T}"/> class from the data in <paramref name="globalResponse"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// If the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="globalResponse">The response from where the instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        protected internal ExGlobalValueResponse(IGlobalResponse globalResponse) : base(globalResponse) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExGlobalValueResponse{T}"/> class from the data in <paramref name="globalResponse"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// If the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        protected internal ExGlobalValueResponse(IGlobalResponse<Exception> globalResponse) : base(globalResponse) { }

        #endregion
    }
}
