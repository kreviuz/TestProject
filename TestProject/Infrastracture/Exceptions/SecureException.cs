﻿namespace TestProject.Infrastracture.Exceptions
{
    public class SecureException : Exception
    {
        public SecureException(string? message) : base(message)
        {
        }
    }
}
