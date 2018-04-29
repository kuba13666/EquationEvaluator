using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyrazenia
{
    class Node
    {
        public string value;
        public Node left;
        public Node right;

        public Node(string item)
        {
            value = item;
            left = right = null;
        }
    }
}
