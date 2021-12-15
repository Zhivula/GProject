using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using GraduationProject.Data;
using GraduationProject.View;

namespace GraduationProject.ViewModel
{
    class AddLineViewModel : INotifyPropertyChanged
    {
        MainWindow window;
        public ObservableCollection<string> SourceWireBrand { get; set; }
        private string lineLength;
        private string selectedWireBrand;
        private float r0;
        private float x0;

        public float R0
        {
            get => r0;
            set
            {
                r0 = value;
                OnPropertyChanged(nameof(R0));
            }
        }
        public float X0
        {
            get => x0;
            set
            {
                x0 = value;
                OnPropertyChanged(nameof(X0));
            }
        }
        public string LineLength
        {
            get => lineLength;
            set
            {
                lineLength = value;
                OnPropertyChanged(nameof(LineLength));
            }
        }
        public string SelectedWireBrand
        {
            get => selectedWireBrand;
            set
            {
                selectedWireBrand = value;
                OnPropertyChanged(nameof(SelectedWireBrand));
                if(value == SourceWireBrand[0])
                {
                    R0 = 0.777f;
                    X0 = 0.403f;
                }
                if (value == SourceWireBrand[1])
                {
                    R0 = 0.42859f;
                    X0 = 0.408f;
                }
                if (value == SourceWireBrand[2])
                {
                    R0 = 0.30599f;
                    X0 = 0.397f;
                }
                if (value == SourceWireBrand[3])
                {
                    R0 = 0.24917f;
                    X0 = 0.391f;
                }
                if (value == SourceWireBrand[4])
                {
                    R0 = 0.19798f;
                    X0 = 0.358f;
                }
                if (value == SourceWireBrand[5])
                {
                    R0 = 0.1206f;
                    X0 = 0.333f;
                }
            }
        }

        public AddLineViewModel()
        {
            window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            SourceWireBrand = new ObservableCollection<string>() { "АС-35", "АС-70" , "АС-95" , "АС-120" , "АС-150" , "АС-240" };
            SelectedWireBrand = SourceWireBrand.First();
            LineLength = "1";
        }
        public ICommand AddLineCommand => new DelegateCommand(o =>
        {
            var global = GlobalGrid.GetInstance();
            var line = new Button1(SelectedWireBrand,float.Parse(LineLength)) { Height = 50, Width = 100 };
            //line.UTextBlock.Text = "0";
            //line.R0 = R0;
            //line.X0 = X0;
            //line.Length = float.Parse(LineLength);
            OnPropertyChanged(nameof(line));
            Close();
            //line.MainButton.Click += new RoutedEventHandler(MouseEvent);
            window.GridChange.Children.Add(line);
            window.curr = line;

            global.Lines.Add(line);
        });
        //private void MouseEvent(object sender, RoutedEventArgs e)
        //{
        //    window.curr = sender;
        //}
        public ICommand CloseWindow => new DelegateCommand(o =>
        {
            Close();
        });
        private void Close()
        {
            for (int i = window.GridChange.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.GridChange.Children[i].GetType().Name;
                if (childTypeName == "AddLineView")
                {
                    window.GridChange.Children.RemoveAt(i);
                }
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