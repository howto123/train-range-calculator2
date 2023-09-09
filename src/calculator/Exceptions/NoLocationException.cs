using System;

namespace calculator.Exceptions
{
    public class NoLocationException : ArgumentException
    {
        public NoLocationException(string message) : base(message)
        {
            
        }
    }
}