namespace Email.OTP.Application.Exceptions
{
    /// <summary>
    /// DisabledException
    /// </summary>
    public class DisabledException: Exception
    {
        public DisabledException()
        {

        }

        public DisabledException(string message)
            : base(message)
        {

        }
    }
}
