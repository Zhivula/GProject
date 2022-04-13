using GraduationProject.Model;
using GraduationProject.View;
using GraduationProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GraduationProject.Data
{
    [Serializable]
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
                if (view.DataContext is LineViewModel context)
                {
                    var parentPosition = ((RotateTransform)View.RenderTransform).Angle;
                    var childPosition = ((RotateTransform)view.RenderTransform).Angle;

                    var parent = View as LineView;
                    var child = view as LineView;

                    if (parentPosition == 0 & childPosition == 270)
                    {
                        parent.K.Margin = new Thickness(0,0,-12,0);
                    }
                    if (parentPosition == 90 & childPosition == 0)
                    {
                        RotateTransform rotate = new RotateTransform(-90);

                        parent.K.LayoutTransform = rotate;

                        parent.K.Margin = new Thickness(-30, -30, 8, 30);
                    }
                    else if (parentPosition == 90)
                    {
                        var list = List.Where(x => {
                            var item = x.View as LineView;
                            var angle = ((RotateTransform)item.RenderTransform).Angle;
                            if (angle == 0) return true;
                            else return false;
                        }).ToList();
                        if (list.Count == 0)
                        {
                            RotateTransform rotate = new RotateTransform(-90);

                            parent.K.LayoutTransform = rotate;

                            parent.K.Margin = new Thickness(-30, -30, -5, 30);
                        }
                    }
                    if (parentPosition == 180 & childPosition == 270)
                    {
                        parent.K.Margin = new Thickness(0, 0, 12, 0);
                    }



                    if (parentPosition == 270 & childPosition == 270)
                    {
                        RotateTransform rotate = new RotateTransform(-270);

                        parent.K.LayoutTransform = rotate;

                        parent.K.Margin = new Thickness(0, 33, -20, 0);
                    }
                    else if (parentPosition == 270)
                    {
                        var list = List.Where(x =>
                        {
                            var item = x.View as LineView;
                            var angle = ((RotateTransform)item.RenderTransform).Angle;
                            if (angle == 270) return true;
                            else return false;
                        }).ToList();
                        if (list.Count == 0)
                        {
                            RotateTransform rotate = new RotateTransform(-270);

                            parent.K.LayoutTransform = rotate;

                            parent.K.Margin = new Thickness(0, 20, -20, 0);
                        }
                    }




                }
                if (view.DataContext is TransformerViewModel)
                {
                    var contextButton = View.DataContext as LineViewModel;
                    var transformer = view.DataContext as TransformerViewModel;
                    var parent = Parent;
                    var pp = parent;

                    contextButton.P2List.Add(transformer.K, transformer.P1);
                    contextButton.P2 = contextButton.P2List.Values.Sum();
                    contextButton.Wp2 = contextButton.Wp2List.Values.Sum();

                    contextButton.Q2List.Add(transformer.K, transformer.Q1);
                    contextButton.Q2 = contextButton.Q2List.Values.Sum();
                    contextButton.Wq2 = contextButton.Wq2List.Values.Sum();

                    contextButton.I2 = transformer.I;

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
                        var contextButtonParent = parent.View.DataContext as LineViewModel;
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
                var contextButton = node.Parent.View.DataContext as LineViewModel;
                var transformer = node.View.DataContext as TransformerViewModel;
                var parent = Parent.Parent;
                var pp = parent;

                contextButton.P2List.Add(transformer.K, transformer.P1);
                contextButton.P2 = contextButton.P2List.Values.Sum();
                contextButton.Wp2 = contextButton.Wp2List.Values.Sum();

                contextButton.Q2List.Add(transformer.K, transformer.Q1);
                contextButton.Q2 = contextButton.Q2List.Values.Sum();
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
                    var contextButtonParent = parent.View.DataContext as LineViewModel;
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
            LineViewModel item = View.DataContext as LineViewModel;
            if (item != null)
            {
                item.R = item.R0 * item.Length;
                item.X = item.X0 * item.Length;
                item.DU = (item.R * item.P1 + item.X * item.P1) / ((float)GlobalGrid.U * 1000);
                item.U1 = u;
                u -= (float)item.DU;
                item.U2 = u;
                item.I1 = Math.Sqrt(Math.Pow(item.P1,2) + Math.Pow(item.Q1, 2)) / (Math.Sqrt(3) * 10);
                item.I2 = Math.Sqrt(Math.Pow(item.P2, 2) + Math.Pow(item.Q2, 2)) / (Math.Sqrt(3) * 10);

                var deltaUPercent = ((GlobalGrid.U - item.U2) / GlobalGrid.U) * 100;

                var kzLine = item.I2 / item.Idop;

                if (kzLine >= 1) 
                {
                    item.Opacity = 1;
                    item.Color = Colors.Red;

                    DoubleAnimation buttonAnimation = new DoubleAnimation();
                    buttonAnimation.From = 0.3;
                    buttonAnimation.To = 1;
                    buttonAnimation.RepeatBehavior = RepeatBehavior.Forever;
                    buttonAnimation.AutoReverse = true;
                    buttonAnimation.Duration = new TimeSpan(0,0,2);
                    var b = View as LineView;
                    b.BorderGradient.BeginAnimation(Border.OpacityProperty, buttonAnimation);
                }
                if (kzLine >= 0.75 && kzLine < 1)
                {
                    item.Opacity = 1;
                    item.Color = Colors.Yellow;

                    DoubleAnimation buttonAnimation = new DoubleAnimation();
                    buttonAnimation.From = 0.3;
                    buttonAnimation.To = 1;
                    buttonAnimation.RepeatBehavior = RepeatBehavior.Forever;
                    buttonAnimation.AutoReverse = true;
                    buttonAnimation.Duration = new TimeSpan(0, 0, 2);
                    var b = View as LineView;
                    b.BorderGradient.BeginAnimation(Border.OpacityProperty, buttonAnimation);
                }
                if (deltaUPercent >= 10)
                {
                    item.Opacity = 1;
                    item.ColorNode = new SolidColorBrush(Colors.Red);

                    //DoubleAnimation buttonAnimation = new DoubleAnimation();
                    //buttonAnimation.From = 0.3;
                    //buttonAnimation.To = 1;
                    //buttonAnimation.RepeatBehavior = RepeatBehavior.Forever;
                    //buttonAnimation.AutoReverse = true;
                    //buttonAnimation.Duration = new TimeSpan(0, 0, 2);
                    //var b = View as LineView;
                    //b.EllipseBorderGradient.BeginAnimation(Border.OpacityProperty, buttonAnimation);
                }
                if (deltaUPercent >= 5 && deltaUPercent < 10)
                {
                    item.Opacity = 1;
                    item.ColorNode = new SolidColorBrush(Colors.Yellow);

                    //DoubleAnimation buttonAnimation = new DoubleAnimation();
                    //buttonAnimation.From = 0.3;
                    //buttonAnimation.To = 1;
                    //buttonAnimation.RepeatBehavior = RepeatBehavior.Forever;
                    //buttonAnimation.AutoReverse = true;
                    //buttonAnimation.Duration = new TimeSpan(0, 0, 2);
                    //var b = View as LineView;
                    //b.EllipseBorderGradient.BeginAnimation(Border.OpacityProperty, buttonAnimation);
                }
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
                var contextButton = Parent.View.DataContext as LineViewModel;
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
                    var contextButtonParent = parent.View.DataContext as LineViewModel;
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
                    var line = i.Parent.View.DataContext as LineViewModel;
                    var dPxx = transformer.Pxx * Math.Pow(line.U2 / GlobalGrid.U, 2);
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
        public List<Node<T>> GetTransformer(List<Node<T>> list)
        {
            foreach (var i in List)
            {
                if (i.View.DataContext is TransformerViewModel transformer)
                {
                    if (!list.Contains(i))
                    {
                        list.Add(i);
                    }
                }
                else
                {
                    if (i.List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.GetTransformer(list);
                        }
                    }
                }
            }
            return list;
        } 
        public List<Node<T>> GetElements(ref List<Node<T>> list)
        {

            if (!list.Contains(this))
            {
                list.Add(this);
            }
            if (List.Count > 0)
            {
                foreach (var item in List)
                {
                    item.GetElements(ref list);
                }
            }
            return list;
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
                if (i.View.DataContext is LineViewModel line)
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
        //public TreeSerializable<T> GetElements(TreeSerializable<T> tree)
        //{
        //    foreach (var i in List)
        //    {
        //        if (!tree.Contains(i))
        //        {
        //            tree.Add(i);
        //        }
        //        else
        //        {
        //            if (i.List.Count > 0)
        //            {
        //                foreach (var item in List)
        //                {
        //                    item.GetElements(tree);
        //                }
        //            }
        //        }
        //    }
        //    return tree;
        //}
        public void AddToSerializable(ref TreeSerializable treeSerializable)
        {
            if (View.DataContext is LineViewModel contextLine)
            {
                var angle = ((RotateTransform)View.RenderTransform).Angle;
                var x = Canvas.GetLeft(View);
                var y = Canvas.GetTop(View);
                var node = new NodeSerializable(contextLine.K, contextLine, null, angle, x, y);
                if (!treeSerializable.Contains(node))
                {
                    var model = new LineModel()
                    {
                        K = contextLine.K,
                        N = contextLine.N,
                        Brand = contextLine.Brand,
                        L = contextLine.Length,
                        R0 = contextLine.R0,
                        X0 = contextLine.X0,
                        Idop = contextLine.Idop
                    };
                    treeSerializable.Add(contextLine.N, contextLine.K, model, angle, x, y);
                }
            }
            if (View.DataContext is TransformerViewModel contextTransformer)
            {
                var angle = ((RotateTransform)View.RenderTransform).Angle;
                var x = Canvas.GetLeft(View);
                var y = Canvas.GetTop(View);
                var node = new NodeSerializable(contextTransformer.K, contextTransformer, null, angle, x, y);
                if (!treeSerializable.Contains(node))
                {
                    var model = new TransformerModel()
                    {
                        K = contextTransformer.K,
                        N = contextTransformer.N,
                        Brand = contextTransformer.Brand,
                        Ixx = contextTransformer.Ixx,
                        Pkz = contextTransformer.Pkz,
                        Pxx = contextTransformer.Pxx,
                        Qxx = contextTransformer.Qxx,
                        R = contextTransformer.R,
                        X = contextTransformer.X,
                        Snom = contextTransformer.Snom,
                        Ukz = contextTransformer.Ukz
                    };
                    treeSerializable.Add(contextTransformer.N, contextTransformer.K, model, angle, x, y);
                }
            }
            if (List.Count > 0)
            {
                foreach (var i in List)
                {
                    i.AddToSerializable(ref treeSerializable);
                }
            }
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
