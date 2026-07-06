using System.Diagnostics;
using Newtonsoft.Json;
//this class handles all the operations, from a to d was implemented in the tutorial, so not many modifications were made in this file
namespace CalculatorLibrary
{
    public class Calculator
    {
        private readonly JsonTextWriter? writer;

        public Calculator()
        {
            try
            {
                var logFile = new StreamWriter("CalculatorLog.json", append: true);
                writer = new JsonTextWriter(logFile)
                {
                    Formatting = Formatting.Indented
                };

                writer.WriteStartObject();
                writer.WritePropertyName("Operations");
                writer.WriteStartArray();
            }
            catch
            {
                writer = null;
            }
        }

        public double DoOperation(double n1, double n2, string op)
        {
            double result = double.NaN;

            if (writer == null)
            {
                return result;
            }

            writer.WriteStartObject();
            writer.WritePropertyName("Operand 1");
            writer.WriteValue(n1);
            writer.WritePropertyName("Operand 2");
            writer.WriteValue(n2);
            writer.WritePropertyName("Operation");

            switch (op)
            {
                case "a":
                    result = n1 + n2;
                    writer.WriteValue("Addition");
                    break;
                case "b":
                    result = n1 - n2;
                    writer.WriteValue("Subtraction");
                    break;
                case "c":
                    result = n1 * n2;
                    writer.WriteValue("Multiplication");
                    break;
                case "d":
                    writer.WriteValue("Division");
                    if (n2 != 0)
                    {
                        result = n1 / n2;
                    }
                    break;
                case "e":
                    writer.WriteValue("Square Root");
                    if (n1 >= 0)
                    {
                        result = Math.Sqrt(n1);

                    }
                    break;
                case "f":
                    writer.WriteValue("Power");
                    result = Math.Pow(n1, n2);
                    break;
                case "g":
                    writer.WriteValue("10^x");
                    result = Math.Pow(10, n1);
                    break;
                case "h":
                    writer.WriteValue("Trigonometry");
                    // need to ask user which trig function
                    break;
                default:
                    writer.WriteValue("Invalid Operation");
                    break;
            }

            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            return result;
        }

        public void Finish()
        {
            if (writer != null)
            {
                writer.WriteEndArray();
                writer.WriteEndObject();
                writer.Close();
            }
        }
    }
}
