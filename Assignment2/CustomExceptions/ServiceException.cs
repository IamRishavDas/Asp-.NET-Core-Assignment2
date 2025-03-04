namespace Assignment2.CustomExceptions
{
    public class ServiceException: Exception
    {

        private readonly string _exceptionMsg;
        public bool _isSucced { get; }

        public ServiceException(string exceptionMsg, bool isSucced): base(exceptionMsg)
        {
            _exceptionMsg = exceptionMsg;
            _isSucced = isSucced;
        }

        public ServiceException(string exceptionMsg, bool isSucced, Exception innerException) : base(exceptionMsg, innerException)
        {
            _exceptionMsg = exceptionMsg;
            _isSucced = isSucced;
        }

    }
}
