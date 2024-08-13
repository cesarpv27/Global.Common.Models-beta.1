
namespace Global.Common.Models
{
    /// <summary>
    /// Factory for creating instances of <see cref="ExGlobalValueResponse{T}"/>.
    /// </summary>
    public static class ExGlobalValueResponseFactory
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ExGlobalValueResponse{T}"/> class with the specified <paramref name="value"/> 
        /// and the status set to <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="value">The value associated with the response.</param>
        /// <returns>An instance of <see cref="ExGlobalValueResponse{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        public static ExGlobalValueResponse<T> CreateSuccessful<T>(T value)
        {
            return new ExGlobalValueResponse<T>(ResponseStatus.Success, value);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ExGlobalValueResponse{T}"/> class with the specified <paramref name="value"/> 
        /// and the status set to <see cref="ResponseStatus.Warning"/>.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="value">The value associated with the response.</param>
        /// <returns>An instance of <see cref="ExGlobalValueResponse{T}"/>.</returns>
        public static ExGlobalValueResponse<T> CreateWarning<T>(T? value)
        {
            return new ExGlobalValueResponse<T>(ResponseStatus.Warning, value);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ExGlobalValueResponse{T}"/> class with the status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <returns>An instance of <see cref="ExGlobalValueResponse{T}"/>.</returns>
        public static ExGlobalValueResponse<T> CreateFailure<T>()
        {
            return new ExGlobalValueResponse<T>(ResponseStatus.Failure, default);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ExGlobalValueResponse{T}"/> class with the specified <paramref name="exception"/> 
        /// and the status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="exception">The exception associated with the response.</param>
        /// <returns>An instance of <see cref="ExGlobalValueResponse{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public static ExGlobalValueResponse<T> CreateFailure<T>(Exception exception)
        {
            return new ExGlobalValueResponse<T>(exception);
        }

        #region CreateFrom methods

        /// <summary>
        /// Creates a new instance of the <see cref="ExGlobalValueResponse{T}"/> class from the data in <paramref name="globalValueResponse"/>. 
        /// The new instance contains the same status, value, exception and messages of the <paramref name="globalValueResponse"/>, 
        /// but the message keys may have been modified with suffix.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <param name="globalValueResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="GlobalResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalValueResponse"/> is null.</exception>
        public static ExGlobalValueResponse<T> CreateFrom<T>(IGlobalValueResponse<T, Exception> globalValueResponse)
        {
            return new ExGlobalValueResponse<T>(globalValueResponse);
        }

        #endregion

        #region MapFromFailure methods

        /// <summary>
        /// Creates a new instance of the <see cref="ExGlobalValueResponse{T}"/> class from the data in <paramref name="globalResponse"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// If the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="ExGlobalValueResponse{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static ExGlobalValueResponse<T> MapFromFailure<T>(IGlobalResponse globalResponse)
        {
            return new ExGlobalValueResponse<T>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ExGlobalValueResponse{T}"/> class from the data in <paramref name="globalResponse"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// If the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="ExGlobalValueResponse{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static ExGlobalValueResponse<T> MapFromFailure<T>(IGlobalResponse<Exception> globalResponse)
        {
            return new ExGlobalValueResponse<T>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ExGlobalValueResponse{T}"/> class from the data in <paramref name="globalValueResponse"/>. 
        /// The <paramref name="globalValueResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalValueResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// If the Status in <paramref name="globalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="TIn">The type of value associated with the ingoing response.</typeparam>
        /// <typeparam name="TOut">The type of value associated with the outgoing response.</typeparam>
        /// <param name="globalValueResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="ExGlobalValueResponse{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalValueResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the Status in <paramref name="globalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static ExGlobalValueResponse<TOut> MapFromFailure<TIn, TOut>(IGlobalValueResponse<TIn, Exception> globalValueResponse)
        {
            return MapFromFailure<TOut>(globalValueResponse);
        }

        #endregion
    }
}
