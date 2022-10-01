namespace RTGS.TechTest.Api.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string validationError) : base(validationError)
        {

        }
    }
}
