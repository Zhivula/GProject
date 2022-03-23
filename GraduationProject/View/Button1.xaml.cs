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
    [Serializable]
    public partial class Button1 : UserControl, INotifyPropertyChanged
    {
        public MainWindow window;

        public Button1(string brand, double length, double r0, double x0)
        {
            window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            RenderTransform = new RotateTransform() { Angle = 0 };
            InitializeComponent();
            DataContext = new ButtonViewModel(brand, length, r0, x0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.curr = this;
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            window.InfoPanel.Children.Clear();
            window.InfoPanel.Children.Add(new PanelLineView(this));
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var x = Canvas.GetLeft(this);
            var y = Canvas.GetTop(this);

            if (((RotateTransform)RenderTransform).Angle == 270)
            {
                ((RotateTransform)RenderTransform).Angle = 0;
            }
            else ((RotateTransform)RenderTransform).Angle +=90;
            
            if(((RotateTransform)RenderTransform).Angle == 180)
            {
                RotateTransform rotate = new RotateTransform(-180);
                K.LayoutTransform = rotate;
                N.LayoutTransform = rotate;
                Length.LayoutTransform = rotate;
                Brand.VerticalAlignment = VerticalAlignment.Top;
                Brand.LayoutTransform = rotate;
            }
            if (((RotateTransform)RenderTransform).Angle == 0)
            {
                RotateTransform rotate = new RotateTransform();
                K.LayoutTransform = rotate;
                N.LayoutTransform = rotate;
                Length.LayoutTransform = rotate;
                Brand.VerticalAlignment = VerticalAlignment.Bottom;
                Brand.LayoutTransform = rotate;
            }
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
            //MessageBox.Show("X= " + Canvas.GetLeft(this) + "   Y= " + Canvas.GetTop(this));

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
