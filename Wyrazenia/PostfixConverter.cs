using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyrazenia
{
    class PostfixConverter
    {
        static int Prec(char ch)
        {
            switch (ch)
            {
                case '+':
                case '-':
                    return 1;

                case '*':
                case '/':
                    return 2;

                case '^':
                    return 3;
            }
            return -1;
        }

        public static string infixToPostfix(String exp)
        {
            // initializing empty String for result
            string result = "";

            // initializing empty stack
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < exp.Length; i++)
            {
                char c = exp[i];

                // If the scanned character is an operand, add it to output.
                if (Char.IsNumber(c))
                {
                    try
                    {
                        if (Char.IsNumber(exp[i + 1]) || exp[i + 1] == '.')
                        {
                            result += c;
                        }
                        else
                        {
                            result += c + " ";
                        }
                    }
                    catch
                    {
                        result += c + " ";
                    }

                }
                else if (c == '.')
                    result += c;
                else if (c == '(')
                    stack.Push(c);
                else if (c == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                        result += stack.Pop();

                    if (stack.Count > 0 && stack.Peek() != '(')
                        return "Invalid Expression"; // invalid expression                
                    else
                        stack.Pop();
                }
                else // an operator is encountered
                {
                    while (stack.Count > 0 && Prec(c) <= Prec(stack.Peek()))
                        result += stack.Pop();
                    stack.Push(c);
                }
            }
            while (stack.Count > 0)
                result += stack.Pop();

            return result;
        }
    }
}
