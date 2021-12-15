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
        public T Data { get; private set; }

        public Point Point { get; set; }

        public float U { get; set; }

        public List<Node<T>> List { get; set; }

        public UserControl View { get; set; }

        public Node(T data, Point point, UserControl view)
        {
            Data = data;
            Point = point;
            View = view;
            List = new List<Node<T>>();
        }
        public void Add(T start, T finish, Point point, UserControl view)
        {
            var node = new Node<T>(finish, point, view);
            if (Data.CompareTo(start) == 0)
            {
                List.Add(node);
                if (view.DataContext is TransformerViewModel)
                {
                    var context = this.View.DataContext as ButtonViewModel;
                    context.P1 = 400;
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
        public Node<T> Find(T start)
        {
            Node<T> node = null;
            foreach (var item in List)
            {
                if (item.Data.CompareTo(start) == 0)
                {
                    node = this;
                    return node;
                }
                else item.Find(start);
            }
            return node;
        }
        public void Delete(T start, T finish)
        {
            if (Data.CompareTo(start) == 0)
            {
                var element = List.Where(x => x.Data.CompareTo(finish) == 0).ToList().First();
                List[List.IndexOf(element)] = null;
            }
            else
            {
                foreach(var item in List)
                {
                    Delete(Data, finish);
                }
            }
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
