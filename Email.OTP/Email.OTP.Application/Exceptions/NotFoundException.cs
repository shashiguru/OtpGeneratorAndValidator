namespace Email.OTP.Application.Exceptions
{
    /// <summary>
    /// NotFoundException
    /// </summary>
    public class NotFoundException: Exception
    {
        public NotFoundException()
        {

        }

        public NotFoundException(string message)
            :base(message)
        {

        }
    }
}
