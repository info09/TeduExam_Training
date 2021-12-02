using System;

namespace Examination.Domain.Exceptions
{
    public class ExamDomainException : Exception
    {
        public ExamDomainException()
        {

        }

        public ExamDomainException(string message) : base(message)
        {

        }

        public ExamDomainException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
