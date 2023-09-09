using System;

namespace calculator.Exceptions
{
    public class CityNotFoundException : ArgumentException
    {
        public CityNotFoundException(string message) : base(message)
        {
            
        }
    }
}