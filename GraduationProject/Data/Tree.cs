using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GraduationProject.Data
{
    public class Tree<T> where T: IComparable
    {
        public Node<T> Root { get; private set; }
         
        public int Count { get; private set; }

        public Tree()
        {

        }
        public void Add(T start, T finish, Point point, UserControl view)
        {
            var node = new Node<T>(finish, point, view);
       
            if(Root == null)
            {
                Root = node;
                Count = 1;
                return;
            }
            if (Root.Data.CompareTo(start) == 0)
            {
                Count++;
                Root.List.Add(node);
            }
            if(Root.Data.CompareTo(start) == -1)
            {
                //var r = Root.Find(start);
                Count++;
                Root.Add(start, finish, point, view);
            }  
        }
        public void Delete(T data)
        {
            if (Root.Find(data).List != null)
            {
                Root.Find(data).List = null;
                Count = Foreach().Count;
            }
        }
        /// <summary>
        /// Префиксный обход дерева
        /// </summary>
        public List<Node<T>> Foreach()
        {
            var list = new List<Node<T>>();
            if (Root != null && Root.List.Count > 0)
            {
                Root.GetNode(list);
            }
            return list;
        }
    }
}