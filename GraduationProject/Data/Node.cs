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

        public List<Node<T>> List { get; set; }

        public UserControl View { get; set; }

        public Node<T> Parent { get; set; }

        public Node(T data, UserControl view, Node<T> parent)
        {
            Data = data;
            View = view;
            Parent = parent;
            List = new List<Node<T>>();
        }
        public void Add(T start, T finish, UserControl view)
        {
            var node = new Node<T>(finish, view, this);
            if (Data.CompareTo(start) == 0)
            {
                List.Add(node);
                if (view.DataContext is TransformerViewModel)
                {
                    var contextButton = View.DataContext as ButtonViewModel;
                    var transformer = view.DataContext as TransformerViewModel;
                    var parent = Parent;
                    var pp = parent;

                    contextButton.P2List.Add(transformer.K, transformer.P1);
                    contextButton.P2 = contextButton.P2List.Values.Sum();
                    contextButton.Wp2List.Add(transformer.K, transformer.Wp1);
                    contextButton.Wp2 = contextButton.Wp2List.Values.Sum();

                    contextButton.Q2List.Add(transformer.K, transformer.Q1);
                    contextButton.Q2 = contextButton.Q2List.Values.Sum();
                    contextButton.Wq2List.Add(transformer.K, transformer.Wq1);
                    contextButton.Wq2 = contextButton.Wq2List.Values.Sum();

                    var dP = (contextButton.P2 * contextButton.P2 + contextButton.Q2 * contextButton.Q2) * contextButton.Length * contextButton.R0 / (10.5f * 10.5f * 1000);
                    var dQ = (contextButton.P2 * contextButton.P2 + contextButton.Q2 * contextButton.Q2) * contextButton.Length * contextButton.X0 / (10.5f * 10.5f * 1000);

                    contextButton.P1List = new Dictionary<int, double>();
                    contextButton.Q1List = new Dictionary<int, double>();
                    contextButton.Wp1List = new Dictionary<int, double>();
                    contextButton.Wq1List = new Dictionary<int, double>();

                    foreach (var i in contextButton.P2List)
                    {
                        var percent = i.Value / contextButton.P2;
                        var dPpercent = dP * percent;
                        contextButton.P1List.Add(i.Key, i.Value + dPpercent);
                    }
                    foreach (var i in contextButton.Q2List)
                    {
                        var percent = i.Value / contextButton.Q2;
                        var dQpercent = dQ * percent;
                        contextButton.Q1List.Add(i.Key, i.Value + dQpercent);
                    }

                    var K2f = Math.Pow((0.16 / transformer.Kz) + 0.82, 2);
                    var DWp = ((contextButton.Wp2 * contextButton.Wp2 + contextButton.Wq2 * contextButton.Wq2) * contextButton.R0 * contextButton.Length * K2f) / (10.5f * 10.5f * 1000 * GlobalGrid.T);
                    var DWq = ((contextButton.Wp2 * contextButton.Wp2 + contextButton.Wq2 * contextButton.Wq2) * contextButton.X0 * contextButton.Length * K2f) / (10.5f * 10.5f * 1000 * GlobalGrid.T);

                    foreach (var i in contextButton.Wp2List)
                    {
                        var percent = i.Value / contextButton.Wp2;
                        var dWPpercent = DWp * percent;
                        contextButton.Wp1List.Add(i.Key, i.Value + dWPpercent);
                    }
                    foreach (var i in contextButton.Wq2List)
                    {
                        var percent = i.Value / contextButton.Wq2;
                        var dWQpercent = DWq * percent;
                        contextButton.Wq1List.Add(i.Key, i.Value + dWQpercent);
                    }

                    contextButton.Wp1 = contextButton.Wp1List.Values.Sum();
                    contextButton.Wq1 = contextButton.Wq1List.Values.Sum();

                    contextButton.P1 = contextButton.P1List.Values.Sum();
                    contextButton.Q1 = contextButton.Q1List.Values.Sum();

                    var oldParent = contextButton;

                    while (parent != null)
                    {
                        var contextButtonParent = parent.View.DataContext as ButtonViewModel;
                        contextButtonParent.Wp2 = oldParent.Wp1;
                        contextButtonParent.Wq2 = oldParent.Wq1;
                        pp = parent;

                        contextButtonParent.P2List.Add(transformer.K, oldParent.P1List.Where(x => x.Key == transformer.K).FirstOrDefault().Value);
                        contextButtonParent.P2 = contextButtonParent.P2List.Values.Sum();

                        contextButtonParent.Q2List.Add(transformer.K, oldParent.Q1List.Where(x => x.Key == transformer.K).FirstOrDefault().Value);
                        contextButtonParent.Q2 = contextButtonParent.Q2List.Values.Sum();

                        contextButtonParent.Wp2List.Add(transformer.K, oldParent.Wp1List.Where(x => x.Key == transformer.K).FirstOrDefault().Value);
                        contextButtonParent.Wp2 = contextButtonParent.Wp2List.Values.Sum();

                        contextButtonParent.Wq2List.Add(transformer.K, oldParent.Wq1List.Where(x => x.Key == transformer.K).FirstOrDefault().Value);
                        contextButtonParent.Wq2 = contextButtonParent.Wq2List.Values.Sum();

                        dP = (contextButtonParent.P2 * contextButtonParent.P2 + contextButtonParent.Q2 * contextButtonParent.Q2) * contextButtonParent.Length * contextButtonParent.R0 / (10.5f * 10.5f * 1000);
                        dQ = (contextButtonParent.P2 * contextButtonParent.P2 + contextButtonParent.Q2 * contextButtonParent.Q2) * contextButtonParent.Length * contextButtonParent.X0 / (10.5f * 10.5f * 1000);

                        contextButtonParent.P1List = new Dictionary<int, double>();
                        contextButtonParent.Q1List = new Dictionary<int, double>();
                        contextButtonParent.Wp1List = new Dictionary<int, double>();
                        contextButtonParent.Wq1List = new Dictionary<int, double>();

                        foreach (var i in contextButtonParent.P2List)
                        {
                            var percent = i.Value / contextButtonParent.P2;
                            var dPpercent = dP * percent;
                            contextButtonParent.P1List.Add(i.Key, i.Value + dPpercent);
                        }
                        foreach (var i in contextButtonParent.Q2List)
                        {
                            var percent = i.Value / contextButtonParent.Q2;
                            var dQpercent = dQ * percent;
                            contextButtonParent.Q1List.Add(i.Key, i.Value + dQpercent);
                        }

                        DWp = ((contextButtonParent.Wp2 * contextButtonParent.Wp2 + contextButtonParent.Wq2 * contextButtonParent.Wq2) * contextButtonParent.R0 * contextButtonParent.Length * K2f) / (10.5f * 10.5f * 1000 * GlobalGrid.T);
                        DWq = ((contextButtonParent.Wp2 * contextButtonParent.Wp2 + contextButtonParent.Wq2 * contextButtonParent.Wq2) * contextButtonParent.X0 * contextButtonParent.Length * K2f) / (10.5f * 10.5f * 1000 * GlobalGrid.T);

                        foreach (var i in contextButtonParent.Wp2List)
                        {
                            var percent = i.Value / contextButtonParent.Wp2;
                            var dWPpercent = DWp * percent;
                            contextButtonParent.Wp1List.Add(i.Key, i.Value + dWPpercent);
                        }
                        foreach (var i in contextButtonParent.Wq2List)
                        {
                            var percent = i.Value / contextButtonParent.Wq2;
                            var dWQpercent = DWq * percent;
                            contextButtonParent.Wq1List.Add(i.Key, i.Value + dWQpercent);
                        }

                        contextButtonParent.Wp1 = contextButtonParent.Wp1List.Values.Sum();
                        contextButtonParent.Wq1 = contextButtonParent.Wq1List.Values.Sum();

                        contextButtonParent.P1 = contextButtonParent.P1List.Values.Sum();
                        contextButtonParent.Q1 = contextButtonParent.Q1List.Values.Sum();

                        oldParent = contextButtonParent;

                        parent = pp.Parent;
                    }
                    pp.Volt((float)GlobalGrid.U);
                }
            }
            else
            {
                foreach (var i in List)
                {
                    i.Add(start, finish, view);
                }
            }
        }

        /// <summary>
        /// Такая форма добавления необходима для изменения коэффициента загрузки без повторного добавления в дерево
        /// </summary>
        /// <param name="view"></param>
        public void Add(Node<T> node)
        {
            if (node.View.DataContext is TransformerViewModel)
            {
                var contextButton = node.Parent.View.DataContext as ButtonViewModel;
                var transformer = node.View.DataContext as TransformerViewModel;
                var parent = Parent.Parent;
                var pp = parent;

                contextButton.P2List.Add(transformer.K, transformer.P1);
                contextButton.P2 = contextButton.P2List.Values.Sum();
                contextButton.Wp2List.Add(transformer.K, transformer.Wp1);
                contextButton.Wp2 = contextButton.Wp2List.Values.Sum();

                contextButton.Q2List.Add(transformer.K, transformer.Q1);
                contextButton.Q2 = contextButton.Q2List.Values.Sum();
                contextButton.Wq2List.Add(transformer.K, transformer.Wq1);
                contextButton.Wq2 = contextButton.Wq2List.Values.Sum();

                var dP = (contextButton.P2 * contextButton.P2 + contextButton.Q2 * contextButton.Q2) * contextButton.Length * contextButton.R0 / (10.5f * 10.5f * 1000);
                var dQ = (contextButton.P2 * contextButton.P2 + contextButton.Q2 * contextButton.Q2) * contextButton.Length * contextButton.X0 / (10.5f * 10.5f * 1000);

                contextButton.P1List = new Dictionary<int, double>();
                contextButton.Q1List = new Dictionary<int, double>();
                contextButton.Wp1List = new Dictionary<int, double>();
                contextButton.Wq1List = new Dictionary<int, double>();

                foreach (var i in contextButton.P2List)
                {
                    var percent = i.Value / contextButton.P2;
                    var dPpercent = dP * percent;
                    contextButton.P1List.Add(i.Key, i.Value + dPpercent);
                }
                foreach (var i in contextButton.Q2List)
                {
                    var percent = i.Value / contextButton.Q2;
                    var dQpercent = dQ * percent;
                    contextButton.Q1List.Add(i.Key, i.Value + dQpercent);
                }

                var K2f = Math.Pow((0.16 / transformer.Kz) + 0.82, 2);
                var DWp = ((contextButton.Wp2 * contextButton.Wp2 + contextButton.Wq2 * contextButton.Wq2) * contextButton.R0 * contextButton.Length * K2f) / (10.5f * 10.5f * 1000 * GlobalGrid.T);
                var DWq = ((contextButton.Wp2 * contextButton.Wp2 + contextButton.Wq2 * contextButton.Wq2) * contextButton.X0 * contextButton.Length * K2f) / (10.5f * 10.5f * 1000 * GlobalGrid.T);

                foreach (var i in contextButton.Wp2List)
                {
                    var percent = i.Value / contextButton.Wp2;
                    var dWPpercent = DWp * percent;
                    contextButton.Wp1List.Add(i.Key, i.Value + dWPpercent);
                }
                foreach (var i in contextButton.Wq2List)
                {
                    var percent = i.Value / contextButton.Wq2;
                    var dWQpercent = DWq * percent;
                    contextButton.Wq1List.Add(i.Key, i.Value + dWQpercent);
                }

                contextButton.Wp1 = contextButton.Wp1List.Values.Sum();
                contextButton.Wq1 = contextButton.Wq1List.Values.Sum();

                contextButton.P1 = contextButton.P1List.Values.Sum();
                contextButton.Q1 = contextButton.Q1List.Values.Sum();

                var oldParent = contextButton;

                while (parent != null)
                {
                    var contextButtonParent = parent.View.DataContext as ButtonViewModel;
                    contextButtonParent.Wp2 = oldParent.Wp1;
                    contextButtonParent.Wq2 = oldParent.Wq1;
                    pp = parent;

                    contextButtonParent.P2List.Add(transformer.K, oldParent.P1List.Where(x => x.Key == transformer.K).FirstOrDefault().Value);
                    contextButtonParent.P2 = contextButtonParent.P2List.Values.Sum();

                    contextButtonParent.Q2List.Add(transformer.K, oldParent.Q1List.Where(x => x.Key == transformer.K).FirstOrDefault().Value);
                    contextButtonParent.Q2 = contextButtonParent.Q2List.Values.Sum();

                    contextButtonParent.Wp2List.Add(transformer.K, oldParent.Wp1List.Where(x => x.Key == transformer.K).FirstOrDefault().Value);
                    contextButtonParent.Wp2 = contextButtonParent.Wp2List.Values.Sum();

                    contextButtonParent.Wq2List.Add(transformer.K, oldParent.Wq1List.Where(x => x.Key == transformer.K).FirstOrDefault().Value);
                    contextButtonParent.Wq2 = contextButtonParent.Wq2List.Values.Sum();

                    dP = (contextButtonParent.P2 * contextButtonParent.P2 + contextButtonParent.Q2 * contextButtonParent.Q2) * contextButtonParent.Length * contextButtonParent.R0 / (10.5f * 10.5f * 1000);
                    dQ = (contextButtonParent.P2 * contextButtonParent.P2 + contextButtonParent.Q2 * contextButtonParent.Q2) * contextButtonParent.Length * contextButtonParent.X0 / (10.5f * 10.5f * 1000);

                    contextButtonParent.P1List = new Dictionary<int, double>();
                    contextButtonParent.Q1List = new Dictionary<int, double>();
                    contextButtonParent.Wp1List = new Dictionary<int, double>();
                    contextButtonParent.Wq1List = new Dictionary<int, double>();

                    foreach (var i in contextButtonParent.P2List)
                    {
                        var percent = i.Value / contextButtonParent.P2;
                        var dPpercent = dP * percent;
                        contextButtonParent.P1List.Add(i.Key, i.Value + dPpercent);
                    }
                    foreach (var i in contextButtonParent.Q2List)
                    {
                        var percent = i.Value / contextButtonParent.Q2;
                        var dQpercent = dQ * percent;
                        contextButtonParent.Q1List.Add(i.Key, i.Value + dQpercent);
                    }

                    DWp = ((contextButtonParent.Wp2 * contextButtonParent.Wp2 + contextButtonParent.Wq2 * contextButtonParent.Wq2) * contextButtonParent.R0 * contextButtonParent.Length * K2f) / (10.5f * 10.5f * 1000 * GlobalGrid.T);
                    DWq = ((contextButtonParent.Wp2 * contextButtonParent.Wp2 + contextButtonParent.Wq2 * contextButtonParent.Wq2) * contextButtonParent.X0 * contextButtonParent.Length * K2f) / (10.5f * 10.5f * 1000 * GlobalGrid.T);

                    foreach (var i in contextButtonParent.Wp2List)
                    {
                        var percent = i.Value / contextButtonParent.Wp2;
                        var dWPpercent = DWp * percent;
                        contextButtonParent.Wp1List.Add(i.Key, i.Value + dWPpercent);
                    }
                    foreach (var i in contextButtonParent.Wq2List)
                    {
                        var percent = i.Value / contextButtonParent.Wq2;
                        var dWQpercent = DWq * percent;
                        contextButtonParent.Wq1List.Add(i.Key, i.Value + dWQpercent);
                    }

                    contextButtonParent.Wp1 = contextButtonParent.Wp1List.Values.Sum();
                    contextButtonParent.Wq1 = contextButtonParent.Wq1List.Values.Sum();

                    contextButtonParent.P1 = contextButtonParent.P1List.Values.Sum();
                    contextButtonParent.Q1 = contextButtonParent.Q1List.Values.Sum();

                    oldParent = contextButtonParent;

                    parent = pp.Parent;
                }
                pp.Volt((float)GlobalGrid.U);
            }
        }
        public void Volt(float u)
        {
            ButtonViewModel item = View.DataContext as ButtonViewModel;
            if (item != null)
            {
                var rl = item.R0 * item.Length;
                var xl = item.X0 * item.Length;
                var dU = (rl * item.P1 + xl * item.P1) / ((float)GlobalGrid.U * 1000);
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
        public void Delete(UserControl view)
        {
            if (view.DataContext is TransformerViewModel)
            {
                var contextButton = Parent.View.DataContext as ButtonViewModel;
                var transformer = view.DataContext as TransformerViewModel;

                var parent = Parent.Parent;
                var pp = parent;

                contextButton.P2List.Remove(transformer.K);
                contextButton.P2 = contextButton.P2List.Values.Sum();
                contextButton.Wp2List.Remove(transformer.K);
                contextButton.Wp2 = contextButton.Wp2List.Values.Sum();

                contextButton.Q2List.Remove(transformer.K);
                contextButton.Q2 = contextButton.Q2List.Values.Sum();
                contextButton.Wq2List.Remove(transformer.K);
                contextButton.Wq2 = contextButton.Wq2List.Values.Sum();

                var dP = (contextButton.P2 * contextButton.P2 + contextButton.Q2 * contextButton.Q2) * contextButton.Length * contextButton.R0 / (10.5f * 10.5f * 1000);
                var dQ = (contextButton.P2 * contextButton.P2 + contextButton.Q2 * contextButton.Q2) * contextButton.Length * contextButton.X0 / (10.5f * 10.5f * 1000);

                contextButton.P1List = new Dictionary<int, double>();
                contextButton.Q1List = new Dictionary<int, double>();
                contextButton.Wp1List = new Dictionary<int, double>();
                contextButton.Wq1List = new Dictionary<int, double>();

                foreach (var i in contextButton.P2List)
                {
                    var percent = i.Value / contextButton.P2;
                    var dPpercent = dP * percent;
                    contextButton.P1List.Add(i.Key, i.Value + dPpercent);
                }
                foreach (var i in contextButton.Q2List)
                {
                    var percent = i.Value / contextButton.Q2;
                    var dQpercent = dQ * percent;
                    contextButton.Q1List.Add(i.Key, i.Value + dQpercent);
                }

                var K2f = Math.Pow((0.16 / transformer.Kz) + 0.82, 2);
                var DWp = ((contextButton.Wp2 * contextButton.Wp2 + contextButton.Wq2 * contextButton.Wq2) * contextButton.R0 * contextButton.Length * K2f) / (10.5f * 10.5f * 1000 * GlobalGrid.T);
                var DWq = ((contextButton.Wp2 * contextButton.Wp2 + contextButton.Wq2 * contextButton.Wq2) * contextButton.X0 * contextButton.Length * K2f) / (10.5f * 10.5f * 1000 * GlobalGrid.T);

                foreach (var i in contextButton.Wp2List)
                {
                    var percent = i.Value / contextButton.Wp2;
                    var dWPpercent = DWp * percent;
                    contextButton.Wp1List.Add(i.Key, i.Value + dWPpercent);
                }
                foreach (var i in contextButton.Wq2List)
                {
                    var percent = i.Value / contextButton.Wq2;
                    var dWQpercent = DWq * percent;
                    contextButton.Wq1List.Add(i.Key, i.Value + dWQpercent);
                }

                contextButton.Wp1 = contextButton.Wp1List.Values.Sum();
                contextButton.Wq1 = contextButton.Wq1List.Values.Sum();

                contextButton.P1 = contextButton.P1List.Values.Sum();
                contextButton.Q1 = contextButton.Q1List.Values.Sum();

                var oldParent = contextButton;

                while (parent != null)
                {
                    var contextButtonParent = parent.View.DataContext as ButtonViewModel;
                    contextButtonParent.Wp2 = oldParent.Wp1;
                    contextButtonParent.Wq2 = oldParent.Wq1;
                    pp = parent;

                    if (oldParent.P1List.Count > 0) contextButtonParent.P2List.Remove(transformer.K);
                    else contextButtonParent.P2List.Remove(transformer.K);
                    contextButtonParent.P2 = contextButtonParent.P2List.Values.Sum();

                    if (oldParent.Q1List.Count > 0) contextButtonParent.Q2List.Remove(transformer.K);
                    else contextButtonParent.Q2List.Remove(transformer.K);
                    contextButtonParent.Q2 = contextButtonParent.Q2List.Values.Sum();

                    if (oldParent.Wp1List.Count > 0) contextButtonParent.Wp2List.Remove(transformer.K);
                    else contextButtonParent.Wp2List.Remove(transformer.K);
                    contextButtonParent.Wp2 = contextButtonParent.Wp2List.Values.Sum();

                    if (oldParent.Wq1List.Count > 0) contextButtonParent.Wq2List.Remove(transformer.K);
                    else contextButtonParent.Wq2List.Remove(transformer.K);
                    contextButtonParent.Wq2 = contextButtonParent.Wq2List.Values.Sum();

                    dP = (contextButtonParent.P2 * contextButtonParent.P2 + contextButtonParent.Q2 * contextButtonParent.Q2) * contextButtonParent.Length * contextButtonParent.R0 / (10.5f * 10.5f * 1000);
                    dQ = (contextButtonParent.P2 * contextButtonParent.P2 + contextButtonParent.Q2 * contextButtonParent.Q2) * contextButtonParent.Length * contextButtonParent.X0 / (10.5f * 10.5f * 1000);

                    contextButtonParent.P1List = new Dictionary<int, double>();
                    contextButtonParent.Q1List = new Dictionary<int, double>();
                    contextButtonParent.Wp1List = new Dictionary<int, double>();
                    contextButtonParent.Wq1List = new Dictionary<int, double>();

                    foreach (var i in contextButtonParent.P2List)
                    {
                        var percent = i.Value / contextButtonParent.P2;
                        var dPpercent = dP * percent;
                        contextButtonParent.P1List.Add(i.Key, i.Value + dPpercent);
                    }
                    foreach (var i in contextButtonParent.Q2List)
                    {
                        var percent = i.Value / contextButtonParent.Q2;
                        var dQpercent = dQ * percent;
                        contextButtonParent.Q1List.Add(i.Key, i.Value + dQpercent);
                    }

                    DWp = ((contextButtonParent.Wp2 * contextButtonParent.Wp2 + contextButtonParent.Wq2 * contextButtonParent.Wq2) * contextButtonParent.R0 * contextButtonParent.Length * K2f) / (10.5f * 10.5f * 1000 * GlobalGrid.T);
                    DWq = ((contextButtonParent.Wp2 * contextButtonParent.Wp2 + contextButtonParent.Wq2 * contextButtonParent.Wq2) * contextButtonParent.X0 * contextButtonParent.Length * K2f) / (10.5f * 10.5f * 1000 * GlobalGrid.T);

                    foreach (var i in contextButtonParent.Wp2List)
                    {
                        var percent = i.Value / contextButtonParent.Wp2;
                        var dWPpercent = DWp * percent;
                        contextButtonParent.Wp1List.Add(i.Key, i.Value + dWPpercent);
                    }
                    foreach (var i in contextButtonParent.Wq2List)
                    {
                        var percent = i.Value / contextButtonParent.Wq2;
                        var dWQpercent = DWq * percent;
                        contextButtonParent.Wq1List.Add(i.Key, i.Value + dWQpercent);
                    }

                    contextButtonParent.Wp1 = contextButtonParent.Wp1List.Values.Sum();
                    contextButtonParent.Wq1 = contextButtonParent.Wq1List.Values.Sum();

                    contextButtonParent.P1 = contextButtonParent.P1List.Values.Sum();
                    contextButtonParent.Q1 = contextButtonParent.Q1List.Values.Sum();

                    oldParent = contextButtonParent;

                    parent = pp.Parent;
                }
                pp.Volt((float)GlobalGrid.U);
            }
        }
        public Dictionary<int, double> GetdWxx(Dictionary<int, double> dictionary)
        {
            foreach (var i in List)
            {
                if (i.View.DataContext is TransformerViewModel transformer)
                {
                    var line = i.Parent.View.DataContext as ButtonViewModel;
                    var dPxx = transformer.dPxx * Math.Pow(line.U2 / GlobalGrid.U, 2);
                    var dWxx = dPxx * GlobalGrid.T;
                    if (!dictionary.Keys.Contains(transformer.K))
                    {
                        dictionary.Add(transformer.K, dWxx);
                    }
                }
                else
                {
                    if (i.List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.GetdWxx(dictionary);
                        }
                    }
                }

            }
            return dictionary;
        }
        /// <summary>
        /// Нагрузочные потери всех трансформаторов
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns>Словарь трансформаторов, где: 
        /// Key: номер конца трансформатора
        /// Value: Значение нагрузочных потерь данного трансформатора
        /// </returns>
        public Dictionary<int, double> GetdWnt(Dictionary<int, double> dictionary)
        {
            foreach (var i in List)
            {
                if (i.View.DataContext is TransformerViewModel transformer)
                {
                    var line = i.Parent.View.DataContext as ButtonViewModel;
                    var tg = transformer.Wq1/transformer.Wp1;
                    var dWnt = ((transformer.Wp1* transformer.Wp1*(1+tg*tg))/(line.U2*line.U2*GlobalGrid.T))*transformer.R*transformer.K2f;
                    if (!dictionary.Keys.Contains(transformer.K))
                    {
                        dictionary.Add(transformer.K, dWnt);
                    }
                }
                else
                {
                    if (i.List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.GetdWnt(dictionary);
                        }
                    }
                }

            }
            return dictionary;
        }
        /// <summary>
        /// Нагрузочные потери всех линий
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns>Словарь линий, где: 
        /// Key: номер конца линии
        /// Value: Значение нагрузочных потерь данной линии
        /// </returns>
        public Dictionary<int, double> GetdWnl(Dictionary<int, double> dictionary)
        {
            foreach (var i in List)
            {
                if (i.View.DataContext is ButtonViewModel line)
                {
                    var tg = line.Wq1 / line.Wp1;
                    var dWnl = ((line.Wp1 * line.Wp1 * (1 + tg * tg)) / (line.U2 * line.U2 * GlobalGrid.T)) * line.R0*line.Length * 1;//Вместо 1 нужно вставить K2f для линии
                    if (!dictionary.Keys.Contains(line.K))
                    {
                        dictionary.Add(line.K, dWnl);
                    }
                }
                else
                {
                    if (i.List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.GetdWnl(dictionary);
                        }
                    }
                }

            }
            return dictionary;
        }
        /// <summary>
        /// Изменяет коэффициент загрузки всех трансформаторов
        /// </summary>
        /// <param name="newKz">Новый коэффициент загрузки трансформаторов</param>
        public void ChangedKz(double newKz)
        {
            foreach (var i in List)
            {
                if (i.View.DataContext is TransformerViewModel transformer)
                {
                    if (transformer.Kz != newKz) transformer.Kz = newKz;
                }
                else
                {
                    if (i.List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.ChangedKz(newKz);
                        }
                    }
                }

            }
        }
        /// <summary>
        /// Сумма длин всех линий
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns>Словарь линий, где: 
        /// Key: номер конца линии
        /// Value: Значение длины линии
        /// </returns>
        public Dictionary<int, double> GetSumL(Dictionary<int, double> dictionary)
        {
            foreach (var i in List)
            {
                if (i.View.DataContext is ButtonViewModel line)
                {
                    if (!dictionary.Keys.Contains(line.K))
                    {
                        dictionary.Add(line.K, line.Length);
                    }
                }
                else
                {
                    if (i.List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.GetSumL(dictionary);
                        }
                    }
                }

            }
            return dictionary;
        }
        /// <summary>
        /// Сумма нагрузок всех трансформаторов
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns>Словарь трансформаторов, где: 
        /// Key: номер конца трансформатора
        /// Value: Значение нагрузки трансформатора
        /// </returns>
        public Dictionary<int, double> GetSumT(Dictionary<int, double> dictionary)
        {
            foreach (var i in List)
            {
                if (i.View.DataContext is TransformerViewModel transformer)
                {
                    if (!dictionary.Keys.Contains(transformer.K))
                    {
                        dictionary.Add(transformer.K, transformer.Sj);
                    }
                }
                else
                {
                    if (i.List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.GetSumT(dictionary);
                        }
                    }
                }

            }
            return dictionary;
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
