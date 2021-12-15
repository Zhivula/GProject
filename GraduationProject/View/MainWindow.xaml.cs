using GraduationProject.Data;
using GraduationProject.View;
using GraduationProject.ViewModel;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;


namespace GraduationProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public object curr = null;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            GenerateMap();

            var grid = new Grid() { Background = new SolidColorBrush(Colors.Red)};
            var el = new Ellipse();
            el.Width = 50;
            el.Height = 50;
            el.StrokeThickness = 3;
            el.Fill = new SolidColorBrush(Colors.Transparent);
            el.Stroke = new SolidColorBrush(Colors.Black);

            var line = new Rectangle();
            line.Fill = new SolidColorBrush(Colors.Black);
            line.Width = 10;
            line.Height = 5;

            //grid.Children.Add(el);

            Canvas.SetLeft(el, 50);
            Canvas.SetTop(el, 100);
            GridChange.Children.Add(el);

            Canvas.SetLeft(line, 100);
            Canvas.SetTop(line, 122.5);
            GridChange.Children.Add(line);

            GridChange.MouseMove += new MouseEventHandler(MouseEvent);
            //GridChange.MouseRightButtonUp += new MouseButtonEventHandler(GridChange_MouseRightButtonUp);


            //Tree<int> tree = new Tree<int>();
            //tree.Add(0,1, new Point(100,100));
            //tree.Add(1, 2, new Point(100, 100));
            //tree.Add(2, 3, new Point(100, 100));
            //tree.Add(2, 4, new Point(100, 100));
            //tree.Add(4, 5, new Point(100, 100));
            //tree.Add(4, 6, new Point(100, 100));
            //var list = tree.Foreach();

        }
        private void MouseEvent(object sender, MouseEventArgs e)
        {
            if(curr != null)
            {
                var point = Mouse.GetPosition(GridChange);
                var x = point.X - (point.X % 10);
                var y = point.Y - (point.Y % 10);
                if(curr is Button1 line)
                {

                    if (((RotateTransform)line.RenderTransform).Angle == 90 || ((RotateTransform)line.RenderTransform).Angle == 270)
                    {
                        Canvas.SetLeft(line, x+20);
                        Canvas.SetTop(line, y-50);
                    }
                    else
                    {
                        Canvas.SetLeft(line, x - 50);
                        Canvas.SetTop(line, y - 20);
                    }
                }
                if (curr is TransformerView transformer)
                {
                    Canvas.SetLeft(transformer, x - 50);
                    Canvas.SetTop(transformer, y - 20);
                }
            }
        }
        private void GenerateMap()
        {
            for(var i = 0; i < 120; i++)
            {
                GridChange.Children.Add(new System.Windows.Shapes.Line() {X1=i*10,X2=i*10,Y1=0,Y2=1200, Stroke = Brushes.Gray}); 
            }
            for (var i = 0; i < 120; i++)
            {
                GridChange.Children.Add(new System.Windows.Shapes.Line() { X1 = 0, X2 = 1200, Y1 = i * 10, Y2 = i * 10, Stroke = Brushes.Gray });
            }
        }
        private void GridChange_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var global = GlobalGrid.GetInstance();
            if (curr is UserControl activeLine)
            {
                
                var DataContextLine = activeLine.DataContext as ButtonViewModel;
                var active_left = Canvas.GetLeft(activeLine);
                var active_top = Canvas.GetTop(activeLine);

                if (global.Lines.Count >= 0)
                {
                    foreach (var item in global.Lines)
                    {
                        var itemDataContext = item.DataContext as ButtonViewModel;
                        double another_left = 0;
                        double another_top = 0;
                        if (((RotateTransform)item.RenderTransform).Angle == 0)
                        {
                            another_left = Canvas.GetLeft(item) + 100;
                            another_top = Canvas.GetTop(item);
                        }
                        if (((RotateTransform)item.RenderTransform).Angle == 90)
                        {
                            another_left = Canvas.GetLeft(item) + 100;
                            another_top = Canvas.GetTop(item);
                        }
                        //MessageBox.Show("X1: " + another_left + "X2: " + active_left + "Y1:" + another_top + "Y2:" + active_top);
                        if (active_left == 110 && active_top == 100 && (DataContextLine.Flag == false))
                        {
                            //Специальный случай для первой линии, чтобы она подключилась к источнику

                            UpDateNK(ref DataContextLine, 0, 1);
                            DataContextLine.Flag = true;
                            global.Tree.Add(0,1,new Point(110,100), item);
                        }

                        if (((active_left == another_left && active_top == another_top) || (active_left-20 == another_left && active_top-20 == another_top)) && (itemDataContext.Flag == true))
                        {
                            //Есть общаая точка с другой линией и та линия имеет питание

                            UpDateNK(ref DataContextLine, itemDataContext.K, global.Tree.Count);
                            DataContextLine.Flag = true;
                            global.Tree.Add(DataContextLine.N, DataContextLine.K, new Point(another_left + 100, another_top), item);
                            UpDateNK(ref DataContextLine, itemDataContext.K, global.Tree.Count);
                        }

                        //if ((DataContextLine.Flag == true) && (active_left != another_left && active_top != another_top))//|| (active_left - 20 != another_left && active_top - 20 != another_top)
                        //{
                        //    //Была в цепи с питанием, но сейчас отключается от цепи 
                        //    var node = global.Tree.Root.Find(DataContextLine.N);
                        //    global.Tree.Delete(DataContextLine.N);
                        ////    var index = global.Tree.Foreach().IndexOf(node);
                        ////    var count = global.Tree.Count - index;
                        ////    for (var ii = global.Tree.Count - 1; ii >= index; ii--)
                        ////    {
                        ////        var context = (global.Tree.Foreach()[ii].View as Button1).DataContext as ButtonViewModel;
                        ////        context.K = 0;
                        ////        //global.Chain[i].KTextBlock.Text = global.Chain[i].K.ToString();
                        ////        context.N = 0;
                        ////        //global.Chain[i].NTextBlock.Text = global.Chain[i].N.ToString();
                        ////        //OnPropertyChanged(nameof(activeLine.NTextBlock));
                        ////        //OnPropertyChanged(nameof(activeLine.KTextBlock));
                        ////        context.Flag = false;
                        ////        /*global.Tree.Remove(global.Chain[ii])*/;
                        ////    }
                        //    break;
                        //}
                    }
                }
            }
            //if (curr is Transformer activeTransformer)
            //{
            //    MessageBox.Show("Добавляю трансформатор");
            //    var active_left = Canvas.GetLeft(activeTransformer);
            //    var active_top = Canvas.GetTop(activeTransformer);
            //    var another_left = Canvas.GetLeft(global.Chain.Last()) + 100;
            //    var another_top = Canvas.GetTop(global.Chain.Last());

            //    var U = 10.5f;

            //    if (active_left == another_left && active_top == another_top)
            //    {
            //        for (var i = 0; i < global.Chain.Count; i++)
            //        {
            //            MessageBox.Show("В цикле итерация: " + i);
            //            var Pj = activeTransformer.Snom * 0.8f;
            //            var Qj = activeTransformer.Snom * (float)Math.Sqrt(1 - 0.8f * 0.8f);
            //            //var rj = (1.97f * 10.5f * 10.5f) / (activeTransformer.Snom * activeTransformer.Snom);
            //            //var zj = (4.5f * 10.5f * 10.5f) / (100 * activeTransformer.Snom);
            //            //var xj = (float)Math.Sqrt(zj * zj - rj * rj);
            //            //var dU = (Qj * xj + rj * Pj) / (10.5f * 1000);
            //            //global.Chain[i].UTextBlock.Text = (U - dU).ToString();
            //            //var item = global.Chain[i];
            //            //OnPropertyChanged(nameof(item));
            //            //U -= dU;

            //            var item = global.Chain[i];
            //            var rl = item.R0 * item.Length;
            //            var xl = item.X0 * item.Length;
            //            var dU = (rl * Pj + xl * Qj) / (10.5f * 1000);
            //            item.UTextBlock.Text = (U - dU).ToString();   
            //            OnPropertyChanged(nameof(item));
            //            U -= dU;
            //        }

            //    }
            //    else
            //    {
            //        for (var i = 0; i < global.Chain.Count; i++)
            //        {
            //            var item = global.Chain[i];
            //            item.UTextBlock.Text = 0.ToString();
            //            OnPropertyChanged(nameof(item));
            //        }
            //    }
            //}















            //if (global.Lines.Count() > global.CountNodeInChain+1)
            //{
            //    for (var i = 0; i < global.CountNodeInChain; i++)
            //    {
            //        LineGrid activeLine = curr as LineGrid;
            //        LineGrid otherLine = global.Lines[i] as LineGrid;
            //        var active_left = Canvas.GetLeft(activeLine);
            //        var active_top = Canvas.GetTop(activeLine);

            //        var another_left = Canvas.GetLeft(otherLine) + 100;
            //        var another_top = Canvas.GetTop(otherLine);

            //        if ((activeLine != null) && (otherLine.Equals(activeLine) == false))
            //        {
            //            if (active_left == another_left && active_top == another_top && (otherLine.Flag == true))
            //            {
            //                UpDateNK(ref activeLine, otherLine.K, otherLine.K+1);

            //                activeLine.Flag = true;
            //                global.Lines.Add(activeLine);

            //                global.CountNodeInChain += 1;

            //                MessageBox.Show(global.CountNode.ToString());

            //                MessageBox.Show("Прошло.");
            //            }
            //            else if ((active_left == 50) && (active_top == 100) && (activeLine.Flag == false))
            //            {
            //                activeLine.K = global.CountNode;
            //                activeLine.KTextBlock.Text = activeLine.K.ToString();
            //                activeLine.N = 0;
            //                activeLine.NTextBlock.Text = activeLine.N.ToString();
            //                OnPropertyChanged(nameof(activeLine.NTextBlock));
            //                OnPropertyChanged(nameof(activeLine.KTextBlock));

            //                activeLine.Flag = true;
            //                MessageBox.Show("Питание есть.");
            //                global.Lines.Add(activeLine);
            //            }
            //            else if (activeLine.Flag == true & (active_left != another_left && active_top != another_top))
            //            {
            //                activeLine.Flag = false;
            //                activeLine.K = 0;
            //                activeLine.KTextBlock.Text = activeLine.K.ToString();
            //                activeLine.N = 0;
            //                activeLine.NTextBlock.Text = activeLine.N.ToString();
            //                OnPropertyChanged(nameof(activeLine.NTextBlock));
            //                OnPropertyChanged(nameof(activeLine.KTextBlock));

            //                global.Lines.Remove(activeLine);
            //            }
            //            else MessageBox.Show("Конец.");
            //        }
            //        else if ((global.Lines.Count() == 1) && active_left == 50 && active_top == 100 && (activeLine.Flag == false))
            //        {
            //            activeLine.K = global.CountNode;
            //            activeLine.KTextBlock.Text = activeLine.K.ToString();
            //            activeLine.N = 0;
            //            activeLine.NTextBlock.Text = activeLine.N.ToString();
            //            OnPropertyChanged(nameof(activeLine.NTextBlock));
            //            OnPropertyChanged(nameof(activeLine.KTextBlock));

            //            activeLine.Flag = true;
            //            MessageBox.Show("Питание есть.");
            //            global.Lines.Add(activeLine);
            //        }
            //    }
            //}
            //else
            //{
            //    for (var i = 0; i < global.Lines.Count(); i++)
            //    {
            //        LineGrid activeLine = curr as LineGrid;
            //        LineGrid otherLine = global.Lines[i] as LineGrid;
            //        var active_left = Canvas.GetLeft(activeLine);
            //        var active_top = Canvas.GetTop(activeLine);

            //        var another_left = Canvas.GetLeft(otherLine) + 100;
            //        var another_top = Canvas.GetTop(otherLine);

            //        if ((activeLine != null) && (otherLine.Equals(activeLine) == false))
            //        {
            //            if (active_left == another_left && active_top == another_top && (otherLine.Flag == true))
            //            {
            //                activeLine.K = otherLine.K + 1;
            //                activeLine.KTextBlock.Text = activeLine.K.ToString();
            //                activeLine.N = otherLine.K;
            //                activeLine.NTextBlock.Text = activeLine.N.ToString();

            //                activeLine.Flag = true;
            //                global.Lines.Add(activeLine);

            //                global.CountNodeInChain += 1;

            //                OnPropertyChanged(nameof(activeLine.NTextBlock));
            //                OnPropertyChanged(nameof(activeLine.KTextBlock));

            //                MessageBox.Show(global.CountNode.ToString());

            //                MessageBox.Show("Прошло.");
            //            }
            //            else if ((active_left == 50) && (active_top == 100) && (activeLine.Flag == false))
            //            {
            //                UpDateNK(ref activeLine, 0, global.CountNode);

            //                global.CountNodeInChain += 1;

            //                activeLine.Flag = true;
            //                MessageBox.Show("50 100 false");
            //                global.Lines.Add(activeLine);
            //            }
            //            else if (activeLine.Flag == true & (active_left != another_left && active_top != another_top))
            //            {
            //                activeLine.Flag = false;
            //                UpDateNK(ref activeLine, 0, 0);

            //                var index = global.Lines.IndexOf(activeLine);
            //                for (var t = 0; t < global.Lines.Count() - index - 1; t++)
            //                {
            //                    global.Lines.Remove(global.Lines[t]);
            //                    global.CountNodeInChain -= 1;
            //                }
            //                global.CountNodeInChain -= 1;
            //                global.Lines.Remove(activeLine);
            //                MessageBox.Show("Удаление после true");
            //            }
            //        }
            //        else if ((global.Lines.Count() == 1) && active_left == 50 && active_top == 100 && (activeLine.Flag == false))
            //        {
            //            UpDateNK(ref activeLine, 0, global.CountNode);

            //            global.CountNodeInChain += 1;
            //            activeLine.Flag = true;
            //            MessageBox.Show("Count=1; 50 100 false");
            //            global.Lines.Add(activeLine);
            //        }
            //    }
            //}

            curr = null;
        }
        private void UpDateNK(ref ButtonViewModel activeLine, int N, int K)
        {
            activeLine.K = K;
            //activeLine.KTextBlock.Text = activeLine.K.ToString();
            activeLine.N = N;
            //activeLine.NTextBlock.Text = activeLine.N.ToString();
            //OnPropertyChanged(nameof(activeLine.NTextBlock));
            //OnPropertyChanged(nameof(activeLine.KTextBlock));
        }
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}