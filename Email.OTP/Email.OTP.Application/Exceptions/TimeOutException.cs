namespace Email.OTP.Application.Exceptions
{
    /// <summary>
    /// TimeOutException
    /// </summary>
    public class TimeOutException: Exception
    {
        public TimeOutException()
        {

        }

        public TimeOutException(string message)
            : base(message)
        {

        }
    }
}
