using System;

namespace BasicCalculator
{
    class Program
    {
        const string operator_Symbol = "OPERATORSYMBOL";
        const string operand_1 = "OPERAND1";
        const string operand_2 = "OPERAND2";

        static void Main(string[] args)
        {
            int operand1, operand2;
            int result = 0;
            char operatorSymbol;

            Console.WriteLine("Basic Calculator");
            Console.WriteLine("----------------");
            Console.WriteLine();

            try
            {

                Console.WriteLine("Main method - Begin");
                Console.WriteLine();

                Console.WriteLine("Please enter a whole number value for the first operand");
                operand1 = int.Parse(Console.ReadLine());

                Console.WriteLine("Please enter a whole number value for the second operand");
                operand2 = int.Parse(Console.ReadLine());

                Console.WriteLine("Please enter a valid operator symbol ('+','-','*','/')");
                operatorSymbol = char.Parse(Console.ReadLine());

                result = Calculate(operand1, operand2, operatorSymbol);

                Console.WriteLine();

                Console.WriteLine($"{operand1} {operatorSymbol} {operand2} = {result}");


            }
            catch (CalculationResultOverflowException ex)
            {
                Logger.Log(ex, LogType.Verbose);

                WriteExceptionMessageToScreen($"The result of this calculation is greater or smaller than the allowable range for an integer value (i.e. between {Int32.MinValue} and {Int32.MaxValue})");
            }
            catch (OverflowException ex)
            {
                Logger.Log(ex, LogType.Verbose);
                WriteExceptionMessageToScreen($"You entered a value for one of the operands that is too great or too small for an integer value (i.e. between {Int32.MinValue} and {Int32.MaxValue})");

            }
            catch (FormatException ex)
            {
                Logger.Log(ex, LogType.Verbose);
                WriteExceptionMessageToScreen("Invalid input");

            }
            catch (ArgumentException ex)
            {
                Logger.Log(ex, LogType.Verbose);
                WriteArgumentExceptionToScreen(ex);
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("Main method - End");
            }

            Console.ReadKey();

        }

        private static int Calculate(int operand1, int operand2, char operatorSymbol)
        {
            int result = 0;
            try
            {
                switch (operatorSymbol)
                {
                    case '+':
                        checked
                        {
                            result = operand1 + operand2;
                        }
                        break;
                    case '-':
                        checked
                        {
                            result = operand1 - operand2;
                        }
                        break;
                    case '*':
                        checked
                        {
                            result = operand1 * operand2;
                        }
                        break;
                    case '/':
                        checked
                        {
                            result = operand1 / operand2;
                        }
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
            catch (OverflowException ex)
            {
                throw new CalculationResultOverflowException(ex.Message, ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new ArgumentException($"{nameof(operatorSymbol)} is invalid", operator_Symbol, ex);
            }
            catch (DivideByZeroException ex)
            {
                throw new ArgumentException($"{nameof(operand2)} cannot be 0 when executing a divide operation", operand_2, ex);
            }

            return result;
        }
        private static void WriteArgumentExceptionToScreen(ArgumentException ex)
        {
            string errorMessage = null;

            if (ex?.ParamName.ToUpper() == operator_Symbol)
            {
                errorMessage = "An invalid operator symbol was entered. Only one of the following operator symbols is valid, '+','-','*','/'";
            }
            else if (ex?.ParamName.ToUpper() == operand_2)
            {
                errorMessage = "A value of 0 was entered for the second operand. This value cannot be 0 when executing a divide operation";
            }

            WriteExceptionMessageToScreen(errorMessage);

        }
        private static void WriteExceptionMessageToScreen(string message)
        {
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ResetColor();

        }

    }
}
