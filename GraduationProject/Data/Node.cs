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
    public class Node<T> where T : IComparable
    {
        public T Data { get; set; }

        public Point Point { get; set; }

        public float U { get; set; }

        public List<Node<T>> List { get; set; }

        public UserControl View { get; set; }

        public Node<T> Parent { get; set; }

        public Node(T data, Point point, UserControl view, Node<T> parent)
        {
            Data = data;
            Point = point;
            View = view;
            Parent = parent;
            List = new List<Node<T>>();
        }
        public void Add(T start, T finish, Point point, UserControl view)
        {
            var node = new Node<T>(finish, point, view, this);
            if (Data.CompareTo(start) == 0)
            {
                List.Add(node);
                if (view.DataContext is TransformerViewModel)
                {
                    var contextButton = View.DataContext as ButtonViewModel;
                    var transformer = view.DataContext as TransformerViewModel;
                    var parent = Parent;
                    var pp = parent;
                    contextButton.Q2 = transformer.Q;
                    contextButton.P2 = transformer.P;

                    var dP = (contextButton.P2 * contextButton.P2 + contextButton.Q2 * contextButton.Q2) * contextButton.Length * contextButton.R0 / (10.5f * 10.5f * 1000);
                    var dQ = (contextButton.P2 * contextButton.P2 + contextButton.Q2 * contextButton.Q2) * contextButton.Length * contextButton.X0 / (10.5f * 10.5f * 1000);
                    contextButton.P1 = contextButton.P2 + dP;
                    contextButton.Q1 = contextButton.Q2 + dQ;

                    var oldParent = contextButton;
                    bool flag = false;
                    while (parent != null)
                    {
                        var contextButtonParent = parent.View.DataContext as ButtonViewModel;
                        pp = parent;
                        if (contextButtonParent.P2 != 0 && flag == false)
                        {
                            contextButtonParent.P2 += oldParent.P1;
                            contextButtonParent.Q2 += oldParent.Q1;
                            flag = true;
                        }
                        else
                        {
                            contextButtonParent.P2 = oldParent.P1;
                            contextButtonParent.Q2 = oldParent.Q1;
                        }
                        oldParent = contextButtonParent;
                        //contextButtonParent.U1 = 10.5f;
                        dP = (contextButtonParent.P2 * contextButtonParent.P2 + contextButtonParent.Q2 * contextButtonParent.Q2) * contextButtonParent.Length*contextButtonParent.R0 / (10.5f * 10.5f * 1000);
                        dQ = (contextButtonParent.P2 * contextButtonParent.P2 + contextButtonParent.Q2 * contextButtonParent.Q2) * contextButtonParent.Length * contextButtonParent.X0 / (10.5f * 10.5f * 1000);
                        contextButtonParent.P1 = contextButtonParent.P2 + dP;
                        contextButtonParent.Q1 = contextButtonParent.Q2 + dQ;
                        parent = pp.Parent;
                    }
                    pp.Volt(10.5f);
                }
            }
            else
            {
                foreach (var i in List)
                {
                    i.Add(start, finish, point, view);
                }
            }
        }
        public void Volt(float u)
        {
            ButtonViewModel item = View.DataContext as ButtonViewModel;
            if(item != null)
            {
                var rl = item.R0 * item.Length;
                var xl = item.X0 * item.Length;
                var dU = (rl * item.P1 + xl * item.P1) / (10.5f * 1000);
                item.U1 = u;
                u -= (float)dU; 
                item.U2 = u;
                foreach (var i in List)
                {
                    i.Volt(u);
                }
            }
        }
        public Node<T> Find(T start, T finish)
        {
            Node<T> node = null;
            foreach (var item in List)
            {
                if (item.Data.CompareTo(finish) == 0)
                {
                    node = item;
                    return node;
                }
                else if (item.List.Count() != 0)
                {
                    node = item.Find(start, finish);
                }
                else node = null;
            }
            return node;
        }
        //public void Delete(T start, T finish)
        //{
        //    if (Data.CompareTo(start) == 0)
        //    {
        //        var element = List.Where(x => x.Data.CompareTo(finish) == 0).ToList().First();
        //        List[List.IndexOf(element)] = null;
        //    }
        //    else
        //    {
        //        foreach(var item in List)
        //        {
        //            Delete(Data, finish);
        //        }
        //    }
        //}
        public void Delete(Node<T> node)
        {

        }
        public List<Node<T>> GetNode(List<Node<T>> list)
        {
            if(!list.Contains(this)) list.Add(this);
            foreach(var item in List)
            {
                item.GetNode(list);
            }
            return list;
        }
        public int CompareTo(object obj)
        {
            if(obj is Node<T> item)
            {
                return Data.CompareTo(item);
            }
            else
            {
                MessageBox.Show("Не совпадение типов");
                return -1;
            }
        }
        public int CompareTo(T other)
        {
            return Data.CompareTo(other);
        }
    }
}
