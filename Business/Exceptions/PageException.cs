﻿using System;

namespace Business.Exceptions
{
    public class PageException : Exception
    {
        public Exception EncapsulatedException { get; private set; }

        public PageException(Exception exception) : base(exception.Message)
        {
            EncapsulatedException = exception;
        }
    }
}