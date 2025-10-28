
namespace Sharenv.Application.Models
{
    public class Result
    {
        /// <summary>
        /// Gets errors container
        /// </summary>
        public List<string> Errors { get; } = new List<string>();

        /// <summary>
        /// Gets or sets exception
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets is operation failed or not
        /// </summary>
        public bool IsSucceded { get { return Errors.Count > 0; } }

        /// <summary>
        /// Add exception to container. It clears before errors
        /// </summary>
        /// <param name="ex"></param>
        public void AddException(Exception ex)
        {
            Errors.Clear();
            Errors.Add(ex.Message);
            Exception = ex;
        }

        /// <summary>
        /// Add error to result
        /// </summary>
        /// <param name="err"></param>
        public void AddError(string err)
        {
            Errors.Add(err);
        }

        /// <summary>
        /// Clear errors
        /// </summary>
        public void Clear()
        {
            Errors.Clear();
        }

        /// <summary>
        /// Get formatted error mesage
        /// </summary>
        /// <param name="sep"></param>
        /// <returns></returns>
        public string FormatErrors(string sep = ", ")
        {
            return string.Join(sep, Errors);
        }
    }

    public class Result<T> : Result
    {
        /// <summary>
        /// Gets or sets value
        /// </summary>
        public T Value { get; set; }
    }
}
