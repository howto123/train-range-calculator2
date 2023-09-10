


using System;
namespace calculator.Exceptions;

public class MissingSecretException : Exception
{
    public MissingSecretException(string msg) : base(msg)
    {
        
    }
}