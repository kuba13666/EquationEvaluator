using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyrazenia
{
    class Helper
    {
        public static List<int> ConvertCharToIndex(char[] expression)
        {
            return new List<int>() { char.ToUpper(expression[0]) - 65, (int)Char.GetNumericValue(expression[1]) };
        }

        public static List<List<string>> ConvertingInput(string[] readText)
        {
            var tempExpression = new StringBuilder();
            List<string> tempConvertedText = new List<string>();
            List<List<string>> result = new List<List<string>>();
            for (int i = 0; i < readText.Length; i++)
            {
                for (int j = 0; j < readText[i].Length; j++)
                {
                    if (readText[i][j] != '|' && readText[i][j] != ';')
                    {
                        tempExpression.Append(readText[i][j]);
                    }
                    else
                    {
                        tempConvertedText.Add(tempExpression.ToString());
                        tempExpression.Clear();
                    }
                }
                result.Add(new List<string>());
                foreach (var item in tempConvertedText)
                {
                    result[i].Add(item);
                }
                tempConvertedText.Clear();
            }
            return result;
        }

        public static void ConvertingTableToNumbersOnly(List<List<string>> table)
        {
            float number;
            for (var i = 0; i < table.Count; i++)
            {
                for (var j = 0; j < table[i].Count; j++)
                {
                    var convertedCell = table[i][j].Replace(".", ",");
                    bool parseBolleanResult = Single.TryParse(convertedCell, out number);
                    if (parseBolleanResult)
                    {
                        table[i][j].Equals(number);
                    }
                    else
                    {
                        var numericExpression = ConvertEquationsToNumericEquations(table[i][j], table);
                        var numericExpresionInPostfix = PostfixConverter.infixToPostfix(numericExpression);
                        var tree = ExpressionTree.ConstructTree(numericExpresionInPostfix);
                        var resultOfExpression = ExpressionTree.EvalTree(tree);
                        table[i][j].Equals(resultOfExpression);
                    }
                }
            }
        }

        public static bool isOperator(char c)
        {
            if (c == '+' || c == '-'
                    || c == '*' || c == '/'
                    || c == '^' || c == '(' || c == ')')
            {
                return true;
            }
            return false;
        }

        public static string ConvertEquationsToNumericEquations(string mixedExpression, List<List<string>> table)
        {
            char[] singleExpression = new char[2];
            int flag = 0;
            StringBuilder tempNumericExpression = new StringBuilder();
            float number;
            foreach (var item in mixedExpression)
            {
                if (!isOperator(item) && flag < 2)
                {
                    if (Char.IsDigit(item) && flag == 0)
                    {
                        tempNumericExpression.Append(item);
                    }
                    singleExpression[flag] = item;
                    flag += 1;
                }
                if (!isOperator(item) && flag == 2)
                {
                    var index = ConvertCharToIndex(singleExpression);
                    if (!Single.TryParse(table[index[0]][index[1] - 1].Replace(".", ","), out number))
                    {
                        tempNumericExpression.Append(ConvertEquationsToNumericEquations(table[index[0]][index[1] - 1].ToString(), table));
                    }
                    else
                    {
                        tempNumericExpression.Append(table[index[0]][index[1] - 1]);
                    }
                }
                if (isOperator(item))
                {
                    flag = 0;
                    tempNumericExpression.Append(item);
                }
            }
            return tempNumericExpression.ToString();
        }

        //public static string ConvertEquationsToNumericEquations(string mixedExpression, List<List<float>> table)
        //{
        //    char[] singleExpression = new char[2];
        //    int flag = 0;
        //    StringBuilder tempNumericExpression = new StringBuilder();
        //    float number;
        //    foreach (var item in mixedExpression)
        //    {
        //        if (!isOperator(item) && flag < 2)
        //        {
        //            if (float.TryParse(item.ToString(), out number) && flag == 0)
        //            {
        //                tempNumericExpression.Append(item);
        //            }
        //            singleExpression[flag] = item;
        //            flag += 1;
        //        }
        //        if (!isOperator(item) && flag == 2)
        //        {
        //            var index = ConvertCharToIndex(singleExpression);

        //            tempNumericExpression.Append(table[index[0]][index[1] - 1]);

        //        }
        //        if (isOperator(item))
        //        {
        //            flag = 0;
        //            tempNumericExpression.Append(item);
        //        }
        //    }
        //    return tempNumericExpression.ToString();
        //}
    }
}
