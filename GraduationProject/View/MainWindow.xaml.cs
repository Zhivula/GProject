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

            //var grid = new Grid() { Background = new SolidColorBrush(Colors.Red)};
            //var el = new Ellipse();
            //el.Width = 50;
            //el.Height = 50;
            //el.StrokeThickness = 3;
            //el.Fill = new SolidColorBrush(Colors.Transparent);
            //el.Stroke = new SolidColorBrush(Colors.Black);

            //var text = new TextBlock()
            //{
            //    Text = "~",
            //    FontSize = 70,
            //    Foreground = new SolidColorBrush(Colors.Black),
            //    FontWeight = FontWeights.Light
            //};

            //var line = new Rectangle();
            //line.Fill = new SolidColorBrush(Colors.Black);
            //line.Width = 10;
            //line.Height = 5;

            ////grid.Children.Add(el);

            //Canvas.SetLeft(el, 50);
            //Canvas.SetTop(el, 100);
            //GridChange.Children.Add(el);

            //Canvas.SetLeft(text, 52);
            //Canvas.SetTop(text, 70);
            //GridChange.Children.Add(text);

            //Canvas.SetLeft(line, 100);
            //Canvas.SetTop(line, 122.5);
            //GridChange.Children.Add(line);

            GridChange.MouseMove += new MouseEventHandler(MouseEvent);
            //GridChange.MouseRightButtonUp += new MouseButtonEventHandler(GridChange_MouseRightButtonUp);

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
                    if (((RotateTransform)transformer.RenderTransform).Angle == 90 || ((RotateTransform)transformer.RenderTransform).Angle == 270)
                    {
                        Canvas.SetLeft(transformer, x + 20);
                        Canvas.SetTop(transformer, y - 50);
                    }
                    else
                    {
                        Canvas.SetLeft(transformer, x - 50);
                        Canvas.SetTop(transformer, y - 20);
                    }
                }
                if (curr is SourceView source)
                {
                    Canvas.SetLeft(source, x - 20);
                    Canvas.SetTop(source, y - 50);
                }
            }
        }
        private void GenerateMap()
        {
            for(var i = 0; i < 120; i++)
            {
                GridChange.Children.Add(new Line() {X1=i*10,X2=i*10,Y1=0,Y2=1200, Stroke = Brushes.Gray}); 
            }
            for (var i = 0; i < 120; i++)
            {
                GridChange.Children.Add(new Line() { X1 = 0, X2 = 1200, Y1 = i * 10, Y2 = i * 10, Stroke = Brushes.Gray });
            }
        }
        private void GridChange_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var global = GlobalGrid.GetInstance();
            if (curr is Button1 activeLine)
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
                        //Ниже описываются различные варианты положений элементов относительно друг друга
                        //предыдуший элемент расположен горизонтально: 0 градусов поворот
                        //активный элемент расположен вертикально: 270 или 90 градусов поворот
                        if (((RotateTransform)item.RenderTransform).Angle == 0)
                        {
                            another_left = Canvas.GetLeft(item) + 90;
                            another_top = Canvas.GetTop(item);

                            if (((RotateTransform)activeLine.RenderTransform).Angle == 270)
                            {
                                another_left -= 20;
                                another_top += 30;
                            }
                            if (((RotateTransform)activeLine.RenderTransform).Angle == 90)
                            {
                                another_left += 30;
                                another_top += 20; 
                            }
                        }
                        //предыдуший элемент расположен вертикально: 90 градусов поворот
                        //активный элемент расположен горизонтально: 0 или 180 градусов поворот
                        if (((RotateTransform)item.RenderTransform).Angle == 90)
                        {
                            another_left = Canvas.GetLeft(item);
                            another_top = Canvas.GetTop(item)+90;

                            if (((RotateTransform)activeLine.RenderTransform).Angle == 0)
                            {
                                another_left -= 30;
                                another_top -= 20;
                            }
                            if (((RotateTransform)activeLine.RenderTransform).Angle == 180)
                            {
                                another_left -= 20;
                                another_top += 30;
                            }
                        }
                        //предыдуший элемент расположен горизонтально: 180 градусов поворот
                        //активный элемент расположен вертикально: 270 или 90 градусов поворот
                        if (((RotateTransform)item.RenderTransform).Angle == 180)
                        {
                            another_left = Canvas.GetLeft(item) - 90;
                            another_top = Canvas.GetTop(item);

                            if (((RotateTransform)activeLine.RenderTransform).Angle == 270)
                            {
                                another_left -= 20;
                                another_top += 30;
                            }
                            if (((RotateTransform)activeLine.RenderTransform).Angle == 90)
                            {
                                another_left += 20;
                                another_top -= 30;
                            }
                        }
                        //предыдуший элемент расположен вертикально: 270 градусов поворот
                        //активный элемент расположен горизонтально: 0 или 180 градусов поворот
                        if (((RotateTransform)item.RenderTransform).Angle == 270)
                        {
                            another_left = Canvas.GetLeft(item);
                            another_top = Canvas.GetTop(item) - 90;

                            if (((RotateTransform)activeLine.RenderTransform).Angle == 0)
                            {
                                another_left += 20;
                                another_top -= 30;
                            }
                            if (((RotateTransform)activeLine.RenderTransform).Angle == 180)
                            {
                                another_left += 20;
                                another_top += 30;
                            }
                        }
                        //MessageBox.Show("X1: " + another_left + "X2: " + active_left + "Y1:" + another_top + "Y2:" + active_top);

                        //Специальный случай для первой линии, чтобы она подключилась к источнику
                        var X = Canvas.GetLeft(global.Source)+60;
                        var Y = Canvas.GetTop(global.Source)+20;

                        if (active_left == X && active_top == Y && (DataContextLine.Flag == false))
                        {
                            DataContextLine.N = 0;
                            DataContextLine.K = 1;
                            DataContextLine.Flag = true;
                            global.Tree.Add(0,1, activeLine);
                        }

                        //Есть общаая точка с другой линией и та линия имеет питание
                        if ((active_left == another_left && active_top == another_top) && (itemDataContext.Flag == true))
                        {
                            DataContextLine.K = global.Tree.Count+1;
                            DataContextLine.N = itemDataContext.K;
                            DataContextLine.Flag = true;
                            global.Tree.Add(DataContextLine.N, DataContextLine.K, activeLine);
                            continue;
                        }
                        //Реализовать DELETE
                    }
                }
            }
            if (curr is TransformerView activeTransformer)
            {
                var context = activeTransformer.DataContext as TransformerViewModel;
                var active_left = Canvas.GetLeft(activeTransformer);
                var active_top = Canvas.GetTop(activeTransformer);
                if (global.Lines.Count >= 0)
                {
                    foreach (var item in global.Lines)
                    {
                        var itemDataContext = item.DataContext as ButtonViewModel;

                        //var another_left = Canvas.GetLeft(item) + 100;
                        //var another_top = Canvas.GetTop(item);

                        double another_left = 0;
                        double another_top = 0;
                        //Ниже описываются различные варианты положений элементов относительно друг друга
                        //предыдуший элемент расположен горизонтально: 0 градусов поворот
                        //активный элемент расположен вертикально: 270 или 90 градусов поворот
                        if (((RotateTransform)item.RenderTransform).Angle == 0)
                        {
                            another_left = Canvas.GetLeft(item) + 90;
                            another_top = Canvas.GetTop(item);

                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 270)
                            {
                                another_left -= 20;
                                another_top += 30;
                            }
                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 90)
                            {
                                another_left += 30;
                                another_top += 20;
                            }
                        }
                        //предыдуший элемент расположен вертикально: 90 градусов поворот
                        //активный элемент расположен горизонтально: 0 или 180 градусов поворот
                        if (((RotateTransform)item.RenderTransform).Angle == 90)
                        {
                            another_left = Canvas.GetLeft(item);
                            another_top = Canvas.GetTop(item) + 90;

                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 0)
                            {
                                another_left -= 30;
                                another_top -= 20;
                            }
                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 180)
                            {
                                another_left -= 20;
                                another_top += 30;
                            }
                        }
                        //предыдуший элемент расположен горизонтально: 180 градусов поворот
                        //активный элемент расположен вертикально: 270 или 90 градусов поворот
                        if (((RotateTransform)item.RenderTransform).Angle == 180)
                        {
                            another_left = Canvas.GetLeft(item) - 90;
                            another_top = Canvas.GetTop(item);

                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 270)
                            {
                                another_left -= 20;
                                another_top += 30;
                            }
                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 90)
                            {
                                another_left += 20;
                                another_top -= 30;
                            }
                        }
                        //предыдуший элемент расположен вертикально: 270 градусов поворот
                        //активный элемент расположен горизонтально: 0 или 180 градусов поворот
                        if (((RotateTransform)item.RenderTransform).Angle == 270)
                        {
                            another_left = Canvas.GetLeft(item);
                            another_top = Canvas.GetTop(item) - 90;

                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 0)
                            {
                                another_left += 20;
                                another_top -= 30;
                            }
                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 180)
                            {
                                another_left += 20;
                                another_top += 30;
                            }
                        }

                        //Реализовать DELETE
                        if (context.Flag == true && (active_left != another_left || active_top != another_top))
                        {
                            context.Flag = false;
                            var currentNode = global.Tree.Find(context.N, context.K);
                            global.Tree.Delete(currentNode);
                            
                        }
                        if (active_left == another_left && active_top == another_top && (itemDataContext.Flag == true))
                        {
                            if (GlobalGrid.GetInstance().BoxK.Count > 0)
                            {
                                context.K = global.BoxK.First();
                                global.BoxK.Remove(global.BoxK.First());
                            }
                            else
                            {
                                context.K = global.Tree.Count + 1;
                            }
                            context.N = itemDataContext.K;
                            global.Tree.Add(context.N, context.K, activeTransformer);
                            context.Flag = true;
                        }
                    }
                }
            }
            curr = null;
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