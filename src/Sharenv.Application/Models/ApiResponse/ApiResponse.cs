namespace Sharenv.Application.Models
{
    public class ApiResponse
    {
        /// <summary>
        /// Gets or sets success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Create success response
        /// </summary>
        /// <returns></returns>
        public static ApiResponse SuccessResponse()
        {
            return new ApiResponse() { Success = true };
        }

        /// <summary>
        /// Create success response with message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResponse SuccessResponse(string message)
        {
            return new ApiResponse() { Success = true , Message = message};
        }

        /// <summary>
        /// Create error response with message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResponse ErrorResponse(string message)
        {
            return new ApiResponse() { Success = false, Message = message };
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        /// <summary>
        /// Gets or sets data
        /// </summary>
        T Data { get; set; }

        /// <summary>
        /// Create success response with data
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static ApiResponse<T> SuccessResponse(T Data)
        {
            return new ApiResponse<T> { Success = true, Data = Data };
        }

        /// <summary>
        /// Create success reponse with data and message
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResponse<T> SuccessResponse(T Data, string message)
        {
            return new ApiResponse<T> { Success = true, Data = Data, Message = message };
        }

        /// <summary>
        /// Create error reponse with message
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResponse<T> ErrorResponse(T Data, string message)
        {
            return new ApiResponse<T> { Success = true, Data = Data, Message = message };
        }
    }
}
