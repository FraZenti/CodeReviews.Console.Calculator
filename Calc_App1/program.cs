/*
V Functionality to count the amount of times the calculator was used

V store a list with latest ccalculations, give user ability to delete that list

V allow user to use the results in the list to perform new calculations

add extra calculations: 
V square root
Vtaking the power
V 10x
*/


using System.Text.RegularExpressions;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;

            int counter = 0;

            string? input = "";
            //lists for operations and results, second one used ti complete the " allow user to use the results in the list to perform new calculations" task
            List<string> calculations = new List<string>();
            List<double> calculationsResults = new List<double>();

            CalculatorLibrary.Calculator calculator = new CalculatorLibrary.Calculator();

            while (!endApp)
            {
                input = "";

                double cleanNum1 = 0;
                double cleanNum2 = 0;
                double result = 0;
                //this code repeats as long as the user doesnt want to do a calculation
                do
                {
                    Console.WriteLine("\nPress '1' to see the history of calculations.");
                    Console.WriteLine("Press '2' to check how many times the calculator was used.");
                    Console.WriteLine("Press '3' to do a calculation with older results.");
                    Console.WriteLine("Press anything else to continue.\n");
                    input = Console.ReadLine();
                    if (input?.Trim() == "1")
                    {
                        listPrinter(calculations, calculationsResults);
                    }
                    else if (input?.Trim() == "2")
                    {
                        Console.WriteLine($"\nThe calculator was used {counter} times\n");
                    }
                } while (input?.Trim() == "1" || input?.Trim() == "2");

                Console.WriteLine("Choose an operator:");
                Console.WriteLine("\ta: addition");
                Console.WriteLine("\tb: subtraction");
                Console.WriteLine("\tc: multiplication");
                Console.WriteLine("\td: division");
                Console.WriteLine("\te: square root");
                Console.WriteLine("\tf: power");
                Console.WriteLine("\tg: 10^x");

                string? op = Console.ReadLine();

                if (op == null || !Regex.IsMatch(op, @"^[abcdefgh]$"))
                {
                    Console.WriteLine("Please enter a valid operator.");
                }
                else
                {
                    //this method handles the numbers input, there are 4 returns, 2 for not using new numbers and 2 for using them
                    (cleanNum1, cleanNum2) = askNumbers(op, calculationsResults, input);

                    try
                    {
                        //this class handles the calculation - from a to d was in the microsoft tutorial, the others are added for the challenge
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in an error");
                        }
                        else
                        {
                            //switch to save the string of the operation, so I can save the whole operation in the list without using the word itself or the input letter of the operation
                            string operatorSign = op switch
                            {
                                "a" => "+",
                                "b" => "-",
                                "c" => "*",
                                "d" => "/",
                                "e" => "√",
                                "f" => "^",
                                "g" => "10^",
                                _ => "?"
                            };
                            Console.WriteLine($"Your result: {result:0.##}\n");
                            //this class saves the whole operation in the list, Im not familiar with the terminal commands and creating classes in VSC so i did it for training
                            //it is kinda useless and I could have used a method probably. Just wanted to exercise tbh
                            calculations.Add(ListHandler.listHandler.writeList(counter, cleanNum1, cleanNum2, operatorSign, result));
                            calculationsResults.Add(result);
                            counter++;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An exception occurred: " + e.Message);
                    }
                }

                Console.WriteLine("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;
            }

            calculator.Finish();
        }

    //this method handles the input for the numbers
        static (double, double) askNumbers(string? op, List<double> calculationsResults, string? input)
        {
            string? numInput1;
            string? numInput2;

            double cleanNum1 = 0;
            double cleanNum2 = 0;

            bool usingOldResults = false;
            bool validIndex = false;

            if (input?.Trim() == "3")
                usingOldResults = true;

            if (op == "e" || op == "g" || op == "h")
            {
                //if user wants to use an old result and old results are available - for operations with 1 number
                if (usingOldResults && calculationsResults.Count > 0)
                {

                    do
                    {
                        int counter = 0;
                        foreach (double result in calculationsResults)
                        {
                            Console.WriteLine($"{counter}: {result}");
                            counter++;
                        }

                        Console.WriteLine("Please enter the index of the result you want to use:");
                        string? index1 = Console.ReadLine();
                        if (double.TryParse(index1, out double index))
                        {
                            if (index < 0 || index >= calculationsResults.Count)
                            {
                                Console.WriteLine("Invalid index, please enter a valid index.");
                            }
                            else
                            {
                                cleanNum1 = calculationsResults[(int)index];
                                return (cleanNum1, 0);
                            }
                        }
                    } while (!validIndex);

                }
                //if user wants to use an old result but no operations were made before, the user get asked to insert them manually
                else if (usingOldResults && calculationsResults.Count == 0)
                {
                    Console.WriteLine("Not enough results available, please enter new numbers manually");
                    usingOldResults = false;
                }
                //to insert new numbers manually
                if (!usingOldResults)
                {
                    Console.WriteLine("Write n1:");
                    numInput1 = Console.ReadLine();

                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.WriteLine("Please enter a valid number for n1:");
                        numInput1 = Console.ReadLine();
                    }
                    return (cleanNum1, 0);
                }
                return (cleanNum1, 0);
            }
            //this is for operations with 2 numbers, same logic as before
            else
            {
                if (usingOldResults && calculationsResults.Count >= 2)
                {

                    int inputCounter = 0;
                    do
                    {
                        int counter = 0;
                        if (inputCounter == 0)
                        {
                            Console.WriteLine("Please enter the index of the first result you want to use:");
                        }
                        else if (inputCounter == 1)
                        {
                            Console.WriteLine("Please enter the index of the second result you want to use:");
                        }
                        foreach (double result in calculationsResults)
                        {
                            Console.WriteLine($"{counter}: {result}");
                            counter++;
                        }

                        Console.WriteLine("Please enter the index of the result you want to use:");
                        string? index = Console.ReadLine();
                        if (double.TryParse(index, out double resultIndex))
                        {
                            if (resultIndex < 0 || resultIndex >= calculationsResults.Count)
                            {
                                Console.WriteLine("Invalid index, please enter a valid index.");
                            }
                            else
                            {
                                if (inputCounter == 0)
                                {
                                    cleanNum1 = calculationsResults[(int)resultIndex];
                                }
                                else if (inputCounter == 1)
                                {
                                    cleanNum2 = calculationsResults[(int)resultIndex];
                                }
                                inputCounter++;
                                if (inputCounter == 2)
                                {
                                    return (cleanNum1, cleanNum2);
                                }
                            }
                        }
                    } while (!validIndex);

                }
                else if (usingOldResults && calculationsResults.Count < 2)
                {
                    Console.WriteLine("Not enough results available, please enter new numbers manually");
                    usingOldResults = false;
                }
                if (!usingOldResults)
                {
                    Console.WriteLine("Write n1:");
                    numInput1 = Console.ReadLine();

                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.WriteLine("Please enter a valid number for n1:");
                        numInput1 = Console.ReadLine();
                    }

                    Console.WriteLine("Write n2:");
                    numInput2 = Console.ReadLine();

                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.WriteLine("Please enter a valid number for n2:");
                        numInput2 = Console.ReadLine();
                    }
                    return (cleanNum1, cleanNum2);
                }
                return (cleanNum1, cleanNum2);
            }
        }
    //method to print the history of calculations, if user inputs "1" in the menu
        static void listPrinter(List<string> calculations, List<double> calculationsResults)
        {
            bool wantToDelete = false;
            do
            {
                if (calculations.Count == 0)
                {
                    Console.WriteLine("No calculations have been made yet.");
                    return;
                }
                else
                {
                    Console.WriteLine("History of calculations:");
                    int index = 0;
                    foreach (var calculation in calculations)
                    {
                        Console.WriteLine($"{index}: {calculation}");
                        index++;
                    }
                }
                //here is the logic to allow the user to delete operations from the list - the respective result gets deleted from the second list too
                Console.WriteLine("If you wish to delete anything from the list please enter the index of the calculation you want to delete, or press any other key to continue.");
                string? input = Console.ReadLine();
                if (wantToDelete = int.TryParse(input, out int indexToDelete))
                {
                    if (indexToDelete < 0 || indexToDelete >= calculations.Count)
                    {
                        Console.WriteLine("Invalid Index. No calculation was deleted.");
                    }
                    else
                    {
                        calculations.RemoveAt(indexToDelete);
                        calculationsResults.RemoveAt(indexToDelete);
                        Console.WriteLine("Calculation deleted.");
                        Console.WriteLine("\nDo you wish to delete more calculations? Press 'y' to delete more");
                        string? deleteMore = Console.ReadLine();
                        if (deleteMore?.Trim() == "y")
                        {
                            wantToDelete = true;
                        }
                        else
                        {
                            wantToDelete = false;
                        }
                    }
                }
            } while (wantToDelete);
            return;
        }
    }
}