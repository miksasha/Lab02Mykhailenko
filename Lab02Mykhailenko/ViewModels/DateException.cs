using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02Mykhailenko.ViewModels
{
    class DateException : Exception
    {
        public DateTime Value { get; }
        public DateException(string message, DateTime value) : base(message)
        {
            Value = value;
        }
    }
}
