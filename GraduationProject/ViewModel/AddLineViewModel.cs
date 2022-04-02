using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using GraduationProject.Data;
using GraduationProject.DataBase;
using GraduationProject.View;

namespace GraduationProject.ViewModel
{
    class AddLineViewModel : INotifyPropertyChanged
    {
        MainWindow window;
        public ObservableCollection<string> SourceWireBrand { get; set; }
        private string lineLength;
        private string selectedWireBrand;
        private double r0;
        private double x0;

        public double R0
        {
            get => r0;
            set
            {
                r0 = value;
                OnPropertyChanged(nameof(R0));
            }
        }
        public double X0
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
                //if(value == SourceWireBrand[0])
                //{
                //    R0 = 0.777f;
                //    X0 = 0.403f;
                //}
                //if (value == SourceWireBrand[1])
                //{
                //    R0 = 0.42859f;
                //    X0 = 0.408f;
                //}
                //if (value == SourceWireBrand[2])
                //{
                //    R0 = 0.30599f;
                //    X0 = 0.397f;
                //}
                //if (value == SourceWireBrand[3])
                //{
                //    R0 = 0.24917f;
                //    X0 = 0.391f;
                //}
                //if (value == SourceWireBrand[4])
                //{
                //    R0 = 0.19798f;
                //    X0 = 0.358f;
                //}
                //if (value == SourceWireBrand[5])
                //{
                //    R0 = 0.1206f;
                //    X0 = 0.333f;
                //}
                using (var context = new MyDbContext())
                {
                    if(context.Lines.Count() > 0)
                    {
                        var item = context.Lines.Where(x => x.Brand == selectedWireBrand).Single();
                        R0 = item.R0;
                        X0 = item.X0;
                    }
                }
            }
        }

        public AddLineViewModel()
        {
            window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            var list = new List<string>();
            using (var context = new MyDbContext())
            {
                if (context.Lines.Count() > 0) list = context.Lines.Select(x => x.Brand).ToList();
            }

            SourceWireBrand = new ObservableCollection<string>();
            foreach (var i in list)
            {
                SourceWireBrand.Add(i);
            }
            SelectedWireBrand = SourceWireBrand.FirstOrDefault();


            //SourceWireBrand = new ObservableCollection<string>() { "АС-35", "АС-70" , "АС-95" , "АС-120" , "АС-150" , "АС-240" };
            //SelectedWireBrand = SourceWireBrand.First();
            LineLength = "1";
        }
        public ICommand AddLineCommand => new DelegateCommand(o =>
        {
            var global = GlobalGrid.GetInstance();
            var line = new Button1(SelectedWireBrand,double.Parse(LineLength), R0, X0) { Height = 50, Width = 100 };
            OnPropertyChanged(nameof(line));
            Close();
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
            for (int i = window.StaticGrid.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.StaticGrid.Children[i].GetType().Name;
                if (childTypeName == "AddLineView")
                {
                    window.StaticGrid.Children.RemoveAt(i);
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