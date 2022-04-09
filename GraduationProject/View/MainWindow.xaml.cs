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

            GridChange.MouseMove += new MouseEventHandler(MouseEvent);
        }
        public void MouseEvent(object sender, MouseEventArgs e)
        {
            if(curr != null)
            {
                var point = Mouse.GetPosition(GridChange);
                var x = point.X - (point.X % 10);
                var y = point.Y - (point.Y % 10);
                if(curr is Button1 line)
                {

                    if (((RotateTransform)line.RenderTransform).Angle == 90)
                    {
                        Canvas.SetLeft(line, x+20);
                        Canvas.SetTop(line, y-50);
                    }
                    else if (((RotateTransform)line.RenderTransform).Angle == 180)
                    {
                        Canvas.SetLeft(line, x + 50);
                        Canvas.SetTop(line, y + 30);
                    }
                    else if (((RotateTransform)line.RenderTransform).Angle == 270)
                    {
                        Canvas.SetLeft(line, x - 20);
                        Canvas.SetTop(line, y + 50);
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
        ///Подправить
        private void GenerateMap()
        {
            var width = GridChange.Width;
            var height = GridChange.Height;

            var count = Math.Round(width);

            count = 1200;

            for (var i = 1; i < count; i++)
            {
                GridChange.Children.Add(new Line() { X1 = i * 10, X2 = i * 10, Y1 = 0, Y2 = count*10, Stroke = Brushes.Gray });
                GridChange.Children.Add(new Line() { X1 = 0, X2 = count*10, Y1 = i * 10, Y2 = i * 10, Stroke = Brushes.Gray });
            }
        }
        public void GridChange_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var context = DataContext as MainWindowViewModel;
            context.Model.MainClick(this);
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