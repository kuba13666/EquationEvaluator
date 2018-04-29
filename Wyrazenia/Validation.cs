using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wyrazenia
{
    class Validation
    {
        public static bool Validate(string exp, List<List<string>> table)
        {

            return ParenthesisCheck(exp) && CharactersOrderAndRangeCheck(exp, ValidInputsRange(table));
        }
        public static List<int> ValidInputsRange(List<List<string>> table)
        {
            List<int> range = new List<int>();
            //first element representing characters posible to input due to number of rows
            range.Add(65 + table.Count - 1);
            //second element representing characters posible to input due to number of columns
            range.Add(table[0].Count);
            return range;
        }
        public static bool CharactersOrderAndRangeCheck(string exp, List<int> range)
        {
            var letterLimit = Char.ConvertFromUtf32(range[0]).ToLower();
            exp = exp.Replace("(", "").Replace(")", "");
            Match match = Regex.Match(exp, $@"([a-{letterLimit}][0-{range[1]}]+[\/\*\+\-])*([a-{letterLimit}][0-{range[1]}])?",
                        RegexOptions.IgnoreCase);

            return match.Value == exp;
        }
        public static bool ParenthesisCheck(string exp)
        {
            Stack<char> stack = new Stack<char>();
            foreach (var character in exp)
            {
                if (character == '(')
                    stack.Push(character);
                else if (character == ')')
                {
                    try
                    {
                        stack.Pop();
                    }
                    catch
                    {
                        Console.WriteLine("Invalid expresion due to paranthesis order");
                        return false;
                    }
                }
            }
            if (stack.Count > 0)
            {
                Console.WriteLine("Invalid expresion due to paranthesis order");
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool isOperator(char c)
        {
            if (c == '+' || c == '-'
                    || c == '*' || c == '/'
                    || c == '^')
            {
                return true;
            }
            return false;
        }
    }
}
