using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02Mykhailenko.ViewModels
{
    class EmailException : Exception
    {
        public string Value { get; }
        public EmailException(string message, string value) : base(message)
        {
            Value = value;
        }
    }
}
