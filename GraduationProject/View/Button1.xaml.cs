using GraduationProject.View;
using GraduationProject.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraduationProject.View
{
    /// <summary>
    /// Логика взаимодействия для Button1.xaml
    /// </summary>
    public partial class Button1 : UserControl, INotifyPropertyChanged
    {
        public MainWindow window;

        public Button1(string brand, float length)
        {
            window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            //RenderTransformOrigin = new Point(0.5, 0.5);
            RenderTransform = new RotateTransform() { Angle = 0 };
            InitializeComponent();
            DataContext = new ButtonViewModel(brand, length);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.curr = this;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var x = Canvas.GetLeft(this);
            var y = Canvas.GetTop(this);
            //RenderTransformOrigin = new Point(0, 0);
            //((RotateTransform)RenderTransform).CenterX = 50;
            //((RotateTransform)RenderTransform).CenterY = 25;
            ((RotateTransform)RenderTransform).Angle +=90;
            

            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);

            //var point = Mouse.GetPosition(window.GridChange);
            //var xp = point.X - (point.X % 10);
            //var yp = point.Y - (point.Y % 10);

            //oldX = x;
            //oldY = y;

            //if (canResize)
            //{
            //    window.GridChange.MouseMove -= MouseEvent;
            //    canResize = false;
            //    Canvas.SetLeft(this, xp);
            //    Canvas.SetTop(this, yp - 20);
            //}
            //else
            //{
            //    window.GridChange.MouseMove += MouseEvent;
            //    canResize = true;
            //}
        }
        //private void MouseEvent(object sender, MouseEventArgs e)
        //{
        //    var point = Mouse.GetPosition(window.GridChange);
        //    var x = point.X - (point.X % 10);
        //    var y = point.Y - (point.Y % 10);

        //    RenderTransformOrigin = new Point(0.5,0.5);
        //    RenderTransform = new RotateTransform() { Angle =  90};
        //}
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
