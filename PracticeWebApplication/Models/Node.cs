using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeConsoleApp.Models
{
    public class Node
    {
        public Node? LeftNode {  get; set; }
        public Node? RightNode { get; set; } 
        public char Symbol { get; set; }
        public Node (char symbol)
        {
            Symbol = symbol;
        }
        public void Insert(Node node)
        {
            if (node.Symbol < Symbol)
            {
                if (LeftNode == null)
                {
                    LeftNode = node;
                }
                else
                {
                    LeftNode.Insert(node);
                }
            }
            else
            {
                if (RightNode == null)
                {
                    RightNode = node;
                }
                else
                {
                    RightNode.Insert(node);
                }
            }
        }
        public List<char> Transform(List<char> elements = null)
        {
            if (elements == null)
            {
                elements = new List<char>();
            }

            if (LeftNode != null)
            {
                LeftNode.Transform(elements);
            }

            elements.Add(Symbol);

            if (RightNode != null)
            {
                RightNode.Transform(elements);
            }

            return elements;
        }
    }
}
