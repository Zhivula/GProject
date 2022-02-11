using GraduationProject.ViewModel;
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
            if(Root == null)
            {
                var node = new Node<T>(finish, point, view, null);
                Root = node;
                Count = 1;
                return;
            }
            if (Root.Data.CompareTo(start) == 0)
            {
                var node = new Node<T>(finish, point, view, Root);
                Count++;
                Root.List.Add(node);
            }
            if(Root.Data.CompareTo(start) == -1)
            {
                Count++;
                Root.Add(start, finish, point, view);
            }  
        }
        public Node<T> Find(T start, T finish)
        {
            if (Root != null)
            {
                return Root.Find(start, finish);
            }
            else return null;
        }
        public void Delete(Node<T> node)
        {
            if (node.View.DataContext is TransformerViewModel)
            {
                var contextButton = node.Parent.View.DataContext as ButtonViewModel;
                var transformer = node.View.DataContext as TransformerViewModel;
                GlobalGrid.GetInstance().BoxK.Add(transformer.K);
                transformer.N = 0;
                transformer.K = 0;
                var parent = node.Parent;
                var pp = parent;
                contextButton.Q2 -= transformer.Q;
                contextButton.P2 -= transformer.P;

                var dP = (transformer.P * transformer.P + transformer.Q * transformer.Q) * contextButton.Length * contextButton.R0 / (10.5f * 10.5f * 1000);
                var dQ = (transformer.P * transformer.P + transformer.Q * transformer.Q) * contextButton.Length * contextButton.X0 / (10.5f * 10.5f * 1000);
                contextButton.P1  -= (transformer.P +  dP);
                contextButton.Q1 -= (transformer.Q + dQ);

                var oldParent = contextButton;
                bool flag = false;
                while (parent != null)
                {
                    var contextButtonParent = parent.View.DataContext as ButtonViewModel;
                    pp = parent;
                    if (parent.List.Count > 1 && flag == false)
                    {
                        contextButtonParent.P2 -= oldParent.P1;
                        contextButtonParent.Q2 -= oldParent.Q1;
                        flag = true;
                    }
                    else
                    {
                        contextButtonParent.P2 = oldParent.P1;
                        contextButtonParent.Q2 = oldParent.Q1;
                    }
                    oldParent = contextButtonParent;
                    dP = (contextButtonParent.P2 * contextButtonParent.P2 + contextButtonParent.Q2 * contextButtonParent.Q2) * contextButtonParent.Length * contextButtonParent.R0 / (10.5f * 10.5f * 1000);
                    dQ = (contextButtonParent.P2 * contextButtonParent.P2 + contextButtonParent.Q2 * contextButtonParent.Q2) * contextButtonParent.Length * contextButtonParent.X0 / (10.5f * 10.5f * 1000);
                    contextButtonParent.P1 = contextButtonParent.P2 + dP;
                    contextButtonParent.Q1 = contextButtonParent.Q2 + dQ;
                    parent = pp.Parent;
                }
                pp.Volt(10.5f);
            }
        }
        public void Volt()
        {
            if(Root != null)
            {
                Root.Volt(10.5f);
            }
        }
        /// <summary>
        /// Обход дерева
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