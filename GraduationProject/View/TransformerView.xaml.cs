using GraduationProject.DataBase;
using GraduationProject.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для TransformerView.xaml
    /// </summary>
    [Serializable]
    public partial class TransformerView : UserControl
    {
        public MainWindow window;
        public TransformerView(Transformer tr, double s = 0, double cosfi = 0)
        {
            window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            RenderTransform = new RotateTransform() { Angle = 0 };
            InitializeComponent();
            DataContext = new TransformerViewModel(tr, s, cosfi);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.curr = this;
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            window.InfoPanel.Children.Clear();
            window.InfoPanel.Children.Add(new PanelTransformerView(this));
        }
        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    Dop.Visibility = Visibility.Hidden;
        //}
        private void RotateTransformer(object sender, RoutedEventArgs e)
        {
            var x = Canvas.GetLeft(this);
            var y = Canvas.GetTop(this);
            if (((RotateTransform)RenderTransform).Angle == 270)
            {
                ((RotateTransform)RenderTransform).Angle = 0;
            }
            else ((RotateTransform)RenderTransform).Angle += 90;

            if (((RotateTransform)RenderTransform).Angle == 180)
            {
                RotateTransform rotate = new RotateTransform(-180);
                K.LayoutTransform = rotate;
                Brand.VerticalAlignment = VerticalAlignment.Top;
                Brand.LayoutTransform = rotate;
            }
            if (((RotateTransform)RenderTransform).Angle == 0)
            {
                RotateTransform rotate = new RotateTransform();
                K.LayoutTransform = rotate;
                Brand.VerticalAlignment = VerticalAlignment.Top;
                Brand.LayoutTransform = rotate;
            }

            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }
        //private void Button_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    window.InfoPanel.Children.Add(new PanelLineView());
        //}

        //private void Button_MouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    Dop.Visibility = Visibility.Visible;
        //}
    }
}
