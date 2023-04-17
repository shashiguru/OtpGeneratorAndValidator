namespace Email.OTP.WebAPI.Helpers
{
    public static class Constants
    {
        //Action name
        public const string Generate = "Generate";
        public const string Validate = "Validate";

        //Description
        public const string GenerateAnOtpForAnEmail = "Generate an OTP for an Email";
        public const string ValidateAnOtp = "Validate entered OTP";

        //Validator
        public const string OtpIsEmpty = "OTP cannot be empty";
        public const string OtpIsNumeric = "OTP should be numeric";
        public const string EmailIsEmpty = "Email cannot be empty";
        public const string EmailIsInvalid = "Email is invalid or should be from dso.org.sg";

        //Error
        public const string InternalServerError = "Internal server error, please contact administration.";
    }
}
