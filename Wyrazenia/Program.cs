using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyrazenia
{
    class Program
    {
        static void Main(string[] args)
        {
            var tableOnlyNumbers = new List<List<int>>();
            string path = @"C:\Users\home\Desktop\Zadanie\numbers.txt";
            string[] readText = File.ReadAllLines(path);
            bool validation = false;
            string inputEquasion = "";

            Console.WriteLine($"Loaded numbers are \n {readText[0]} \n {readText[1]}");


            // converting input to table with numeric values in cells
            var readTextConvertedToTable = Helper.ConvertingInput(readText);
            Helper.ConvertingTableToNumbersOnly(readTextConvertedToTable);

            // validation
            while (!validation)
            {
                inputEquasion = Console.ReadLine();
                validation = Validation.Validate(inputEquasion, readTextConvertedToTable);
                if (!validation)
                {
                    Console.WriteLine("Please enter new equasion");
                }
            };


            // converting input to form that can be understood by expresionTree builder
            inputEquasion = inputEquasion.Replace(" ", string.Empty);
            var numericInput = Helper.ConvertEquationsToNumericEquations(inputEquasion, readTextConvertedToTable);
            var postfix = PostfixConverter.infixToPostfix(numericInput);

            // building and evaluatig expression tree
            var tree = ExpressionTree.ConstructTree(postfix);
            var eval = ExpressionTree.EvalTree(tree);

            Console.WriteLine($"Your result is {eval}");
            Console.ReadLine();
        }
    }
}
