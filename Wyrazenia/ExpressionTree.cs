using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyrazenia
{
    class ExpressionTree
    {
        static bool isOperator(char c)
        {
            if (c == '+' || c == '-'
                    || c == '*' || c == '/'
                    || c == '^')
            {
                return true;
            }
            return false;
        }

        public static Node ConstructTree(string postfix)
        {
            Stack<Node> st = new Stack<Node>();
            Node t, t1, t2;
            StringBuilder temp = new StringBuilder();

            // Traverse through every character of
            // input expression
            for (int i = 0; i < postfix.Length; i++)
            {
                //conditions to include multiple digits numbers separeted by space
                if (!isOperator(postfix[i]) && postfix[i + 1] != ' ')
                {
                    temp.Append(postfix[i]);
                }
                else if (!isOperator(postfix[i]) && postfix[i + 1] == ' ')
                {
                    temp.Append(postfix[i]);
                    t = new Node(temp.ToString());
                    st.Push(t);
                    temp.Clear();
                }
                else if (postfix[i] == ' ')
                {
                    continue;
                }
                else
                {
                    t = new Node(postfix[i].ToString());


                    t1 = st.Pop();

                    t2 = st.Pop();


                    t.right = t1;
                    t.left = t2;

                    st.Push(t);
                }
            }

            t = st.Peek();
            st.Pop();

            return t;
        }


        public static float EvalTree(Node root)
        {

            if (root == null)
            {
                return 0;
            }

            if (root.left == null || root.right == null)
            {
                return float.Parse(root.value.Replace(".", ","));

            }

            float leftValue = EvalTree(root.left);


            float rightValue = EvalTree(root.right);


            if (root.value.Equals("+"))
                return leftValue + rightValue;

            if (root.value.Equals("-"))
                return leftValue - rightValue;

            if (root.value.Equals("*"))
            {
                return leftValue * rightValue;
            }


            return leftValue / rightValue;
        }
    }
}
