using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramAPBD01.Exceptions
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message) { }
    }
}
