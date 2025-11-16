
using Sharenv.Domain.Exceptions;

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

        /// <summary>
        /// Throws if any errors exists
        /// </summary>
        /// <exception cref="SharenvException"></exception>
        public void ThrowIfError()
        {
            if (!IsSucceded)
            {
                throw new SharenvException(FormatErrors());
            }
        }

        /// <summary>
        /// Copy result to target result
        /// </summary>
        /// <param name="target"></param>
        public void CopyTo(Result target)
        {
            target.Exception = Exception;
            target.Errors.ForEach(e => target.AddError(e));
        }
    }

    public class Result<T> : Result
    {
        /// <summary>
        /// Gets or sets value
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Throw message exception if any or return result
        /// </summary>
        public T ValueOrException 
        { 
            get 
            {
                if (!IsSucceded)
                {
                    throw new SharenvMessageException(FormatErrors());
                }

                return Value;
            } 
        }

        /// <summary>
        /// Throw message exception if any, check value if null throw eception or return value
        /// </summary>
        public T ValueOrExceptionWithNullCheck
        {
            get
            {
                if(ValueOrException == null)
                {
                    throw new SharenvException("Value cannot be null");
                }

                return Value;
            }
        }

        /// <summary>
        /// Copy values and errors
        /// </summary>
        /// <param name="target"></param>
        public void CopyToWithValue(Result<T> target) 
        {
            this.CopyTo(target);
            target.Value = Value;
        }
    }
}
