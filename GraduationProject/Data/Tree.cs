﻿using GraduationProject.ViewModel;
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
                node.Delete(node.View);
                var index = node.Parent.List.IndexOf(node);
                var transformer = node.View.DataContext as TransformerViewModel;
                GlobalGrid.GetInstance().BoxK.Add(transformer.K);
                transformer.N = 0;
                transformer.K = 0;
                node.Delete(node.View);
                node.Parent.List.Remove(node.Parent.List[index]);
            }
            //Здесь нужно "глубокое удаление"
            if (node.View.DataContext is ButtonViewModel)
            {
                var contextButton = node.Parent.View.DataContext as ButtonViewModel;
                var line = node.View.DataContext as ButtonViewModel;
                GlobalGrid.GetInstance().BoxK.Add(line.K);
                line.N = 0;
                line.K = 0;
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