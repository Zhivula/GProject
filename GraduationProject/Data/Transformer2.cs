using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraduationProject.Data
{
    class Transformer2 : Button, IElement, INotifyPropertyChanged
    {
        private bool flag;
        private string brand;
        private int snom;
        public int N { get; set; }
        public int K { get; set; }
        
        public int Snom
        {
            get => snom;
            set
            {
                snom = value;
                OnPropertyChanged(nameof(Snom));
            }

        }
        public string Brand
        {
            get => brand;
            set
            {
                brand = value;
                OnPropertyChanged(nameof(Brand));
            }

        }
        public bool Flag
        {
            get => flag;
            set
            {
                flag = value;
                OnPropertyChanged(nameof(Flag));
            }

        }

        public Transformer2()
        {
            var grid = new Grid();

            var el = new Ellipse();
            el.Width = 30;
            el.Height = 30;
            el.StrokeThickness = 3;
            el.Fill = new SolidColorBrush(Colors.Transparent);
            el.Stroke = new SolidColorBrush(Colors.Gray);
            el.RenderTransform = new TranslateTransform(-10, 0);
            var el1 = new Ellipse();
            el1.Width = 30;
            el1.Height = 30;
            el1.StrokeThickness = 3;
            el1.Fill = new SolidColorBrush(Colors.Transparent);
            el1.Stroke = new SolidColorBrush(Colors.Gray);
            el1.RenderTransform = new TranslateTransform(10, 0);

            var line1 = new Rectangle();
            line1.Fill = new SolidColorBrush(Colors.Gray);
            line1.Width = 25;
            line1.Height = 5;
            
            line1.RenderTransform = new TranslateTransform(-37.5, 0);

            var line2 = new Rectangle();
            line2.Fill = new SolidColorBrush(Colors.Gray);
            line2.Width = 25;
            line2.Height = 5;
            
            line2.RenderTransform = new TranslateTransform(37.5, 0);

            var brand = new TextBlock() { FontSize = 15, Foreground = new SolidColorBrush(Colors.Gray), RenderTransform = new TranslateTransform(0, -20), HorizontalAlignment = HorizontalAlignment.Center };

            grid.Children.Add(el);
            grid.Children.Add(el1);
            grid.Children.Add(brand);
            grid.Children.Add(line1);
            grid.Children.Add(line2);

            Background = new SolidColorBrush(Colors.Transparent);
            RenderTransform = new TranslateTransform(0, 0);
            Height = 50;
            Width = 100;
            BorderThickness = new Thickness(0);

            Content = grid;
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
