
namespace System
{
    public class NotFoundException : ArgumentException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, string parameterName)
            : base(message, parameterName)
        {
        }
    }
}
