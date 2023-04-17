namespace Email.OTP.Application
{
    public static class Extensions
    {
        /// <summary>
        /// ToSgt
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTimeOffset ToSgt(this DateTime date)
        {
            return new DateTimeOffset(date,
                         new TimeSpan(8, 0, 0));
        }
    }
}
