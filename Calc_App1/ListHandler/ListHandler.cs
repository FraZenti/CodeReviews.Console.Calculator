//this class simply saves the whole operation as a string in the list - as said before it was just for training
namespace ListHandler
{
    public static class listHandler
    {
        public static string writeList(int counter, double cleanNum1, double cleanNum2, string operatorSign, double result)
        {
            string calculation = operatorSign switch
            {
                "+" => $"operation {counter} => {cleanNum1} + {cleanNum2} = {result:0.##}",
                "-" => $"operation {counter} => {cleanNum1} - {cleanNum2} = {result:0.##}",
                "*" => $"operation {counter} => {cleanNum1} * {cleanNum2} = {result:0.##}",
                "/" => $"operation {counter} => {cleanNum1} / {cleanNum2} = {result:0.##}",
                "√" => $"operation {counter} => √{cleanNum1} = {result:0.##}",
                "^" => $"operation {counter} => {cleanNum1}^{cleanNum2} = {result:0.##}",
                "10^" => $"operation {counter} => 10^{cleanNum1} = {result:0.##}",
                "trig" => $"operation {counter} => trig({cleanNum1}) = {result:0.##}",
                _ => $"operation {counter} => {cleanNum1} ? {cleanNum2} = {result:0.##}",
            };
            return calculation;
        }
    }
}


