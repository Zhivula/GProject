using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraduationProject.Data
{
    public class LineGrid : Button, IElement, INotifyPropertyChanged
    {
        private bool canResize;
        private int n;
        private int k;
        private TextBlock u;
        private bool flag;
        private float length;
        private float r0;
        private float x0;

        public float X0
        {
            get => x0;
            set
            {
                x0 = value;
                OnPropertyChanged(nameof(X0));
            }
        }
        public float R0
        {
            get => r0;
            set
            {
                r0 = value;
                OnPropertyChanged(nameof(R0));
            }
        }
        public int N
        {
            get => n;
            set
            {
                n = value;
                OnPropertyChanged(nameof(N));
                OnPropertyChanged(nameof(NTextBlock));
            }
        }
        public int K
        {
            get => k;
            set
            {
                k = value;
                OnPropertyChanged(nameof(K));
                OnPropertyChanged(nameof(KTextBlock));
            }
        }
        public string Brand { get; set; }

        public bool Flag
        {
            get => flag;
            set
            {
                flag = value;
                OnPropertyChanged(nameof(Flag));
            }

        }
        public float Length
        {
            get => length;
            set
            {
                length = value;
                OnPropertyChanged(nameof(Length));
            }
        }
        public TextBlock KTextBlock { get; set; }
        public TextBlock NTextBlock { get; set; }
        public TextBlock UTextBlock
        {
            get => u;
            set
            {
                u = value;
                OnPropertyChanged(nameof(UTextBlock));
            }
        }

        public LineGrid(string brandText, float length)
        {
            var grid = new Grid();
            var panel = new StackPanel() { Orientation = Orientation.Horizontal};
            var text = new TextBlock() { Text = length + " км.", FontSize = 15, Foreground = new SolidColorBrush(Colors.Gray), RenderTransform = new TranslateTransform(0, -10), HorizontalAlignment = HorizontalAlignment.Center };

            NTextBlock = new TextBlock() { Text = N.ToString(), FontSize = 15, Foreground = new SolidColorBrush(Colors.Gray), RenderTransform = new TranslateTransform(0, -30), HorizontalAlignment = HorizontalAlignment.Left };
            KTextBlock = new TextBlock() { Text = K.ToString(), FontSize = 15, Foreground = new SolidColorBrush(Colors.Gray), RenderTransform = new TranslateTransform(0, -30), HorizontalAlignment = HorizontalAlignment.Right };
            UTextBlock = new TextBlock() { Text = "0", FontWeight = FontWeights.Bold, FontSize = 15, Foreground = new SolidColorBrush(Colors.Red), RenderTransform = new TranslateTransform(0, -50), HorizontalAlignment = HorizontalAlignment.Right };

            var brand = new TextBlock() { Text = brandText, FontSize = 15, Foreground = new SolidColorBrush(Colors.Gray), RenderTransform = new TranslateTransform(0, 10), HorizontalAlignment = HorizontalAlignment.Center };
            var rect = new Rectangle() { Fill = new SolidColorBrush(Colors.Black), Height = 5 };

            var buttonCircle1 = new Button() { Content = new Ellipse() { Height = 5, Width = 5 }, HorizontalAlignment = HorizontalAlignment.Left, RenderTransform = new TranslateTransform(-15, 0) };
            var buttonCircle2 = new Button() { Content = new Ellipse() { Height = 5, Width = 5 }, HorizontalAlignment = HorizontalAlignment.Right, RenderTransform = new TranslateTransform(15, 0) };

            buttonCircle1.Click += buttonCircle1_Click;

            grid.Children.Add(text);
            grid.Children.Add(rect);
            grid.Children.Add(brand);
            grid.Children.Add(NTextBlock);
            grid.Children.Add(KTextBlock);
            grid.Children.Add(UTextBlock);
            grid.Children.Add(buttonCircle1);
            grid.Children.Add(buttonCircle2);

            Background = new SolidColorBrush(Colors.Transparent);
            RenderTransform = new TranslateTransform(0, 0);
            Height = 50;
            Width = 100;
            BorderThickness = new Thickness(0);
            Content = grid;

            canResize = false;
        }
        private void buttonCircle1_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var x = Canvas.GetLeft(button);
            var y = Canvas.GetTop(button);
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            var point = Mouse.GetPosition(window.GridChange);
            var xp = point.X - (point.X % 10);
            var yp = point.Y - (point.Y % 10);

            if (canResize)
            {
                OnPropertyChanged(nameof(Width));
                OnPropertyChanged(nameof(Content));
                Canvas.SetLeft(button, xp-50);
                Canvas.SetTop(button, yp - 20);
                canResize = false;
            }
            else
            {
                this.Width += x-xp-50;
                OnPropertyChanged(nameof(Width));
                OnPropertyChanged(nameof(Content));
                Canvas.SetLeft(button, xp-50);
                Canvas.SetTop(button, yp - 20);
                canResize = true;
            }            
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
