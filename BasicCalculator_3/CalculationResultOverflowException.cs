using System;
using System.Collections.Generic;
using System.Text;

namespace BasicCalculator
{
    public class CalculationResultOverflowException : OverflowException
    {
        public CalculationResultOverflowException()
        { 
        }

        public CalculationResultOverflowException(string message):base(message)
        { 
        
        }

        public CalculationResultOverflowException(string message, Exception innerException):base(message,innerException)
        { 
        }

    }
}
