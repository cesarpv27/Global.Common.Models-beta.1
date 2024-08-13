
namespace Global.Common.Models
{
    /// <summary>
    /// Factory class for creating instances of <see cref="GlobalValueResponse{T, TEx}"/>.
    /// </summary>
    public static class GlobalValueResponseFactory
    {
        /// <summary>
        /// Creates a new instance of the <see cref="GlobalValueResponse{T, TEx}"/> class with the specified <paramref name="value"/> 
        /// and the status set to <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="value">The value associated with the response.</param>
        /// <returns>An instance of <see cref="GlobalValueResponse{T, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        public static GlobalValueResponse<T, TEx> CreateSuccessful<T, TEx>(T value) where TEx : Exception
        {
            return new GlobalValueResponse<T, TEx>(ResponseStatus.Success, value);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalValueResponse{T, TEx}"/> class with the specified <paramref name="value"/> 
        /// and the status set to <see cref="ResponseStatus.Warning"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="value">The value associated with the response.</param>
        /// <returns>An instance of <see cref="GlobalValueResponse{T, TEx}"/>.</returns>
        public static GlobalValueResponse<T, TEx> CreateWarning<T, TEx>(T? value) where TEx : Exception
        {
            return new GlobalValueResponse<T, TEx>(ResponseStatus.Warning, value);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalValueResponse{T, TEx}"/> class with the status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <returns>An instance of <see cref="GlobalValueResponse{T, TEx}"/>.</returns>
        public static GlobalValueResponse<T, TEx> CreateFailure<T, TEx>() where TEx : Exception
        {
            return new GlobalValueResponse<T, TEx>(ResponseStatus.Failure, default);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalValueResponse{T, TEx}"/> class with the specified <paramref name="exception"/> 
        /// and the status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="exception">The exception associated with the response.</param>
        /// <returns>An instance of <see cref="GlobalValueResponse{T, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public static GlobalValueResponse<T, TEx> CreateFailure<T, TEx>(TEx exception) where TEx : Exception
        {
            return new GlobalValueResponse<T, TEx>(exception);
        }

        #region MapFromFailure methods

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalValueResponse{T, TEx}"/> class from the data in <paramref name="globalResponse"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// If the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="GlobalValueResponse{T, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static GlobalValueResponse<T, TEx> MapFromFailure<T, TEx>(IGlobalResponse globalResponse) where TEx : Exception
        {
            return new GlobalValueResponse<T, TEx> (globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalValueResponse{T, TEx}"/> class from the data in <paramref name="globalResponse"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// If the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="GlobalValueResponse{T, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static GlobalValueResponse<T, TEx> MapFromFailure<T, TEx>(IGlobalResponse<TEx> globalResponse) where TEx : Exception
        {
            return new GlobalValueResponse<T, TEx> (globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalValueResponse{T, TEx}"/> class from the data in <paramref name="globalValueResponse"/>. 
        /// The <paramref name="globalValueResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalValueResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// If the Status in <paramref name="globalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="TIn">The type of value associated with the ingoing response.</typeparam>
        /// <typeparam name="TOut">The type of value associated with the outgoing response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalValueResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="GlobalValueResponse{TOut, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalValueResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the Status in <paramref name="globalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static GlobalValueResponse<TOut, TEx> MapFromFailure<TIn, TOut, TEx>(IGlobalValueResponse<TIn, TEx> globalValueResponse) where TEx : Exception
        {
            return MapFromFailure<TOut, TEx>(globalValueResponse);
        }

        #endregion
    }
}
