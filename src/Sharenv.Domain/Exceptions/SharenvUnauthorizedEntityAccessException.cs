namespace Sharenv.Domain.Exceptions
{
    public class SharenvUnauthorizedEntityAccessException : Exception
    {
        public SharenvUnauthorizedEntityAccessException(string message = "You don't have access to this entity") : base(message)
        {
              
        }
    }
}
