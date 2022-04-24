using GraduationProject.Data;
using GraduationProject.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace GraduationProject.ViewModel
{
    public class BSKViewModel : INotifyPropertyChanged
    {
        private int n;
        private int k;
        private double voltage1;
        private double voltage2;
        private double maxPlusVoltage;
        private double sBSK;

        public int N
        {
            get => n;
            set
            {
                n = value;
                OnPropertyChanged(nameof(N));
            }
        }
        public int K
        {
            get => k;
            set
            {
                k = value;
                OnPropertyChanged(nameof(K));
            }
        }
        public double Voltage1
        {
            get => voltage1;
            set
            {
                voltage1 = value;
                OnPropertyChanged(nameof(Voltage1));
            }
        }
        public double Voltage2
        {
            get => voltage2;
            set
            {
                voltage2 = value;
                OnPropertyChanged(nameof(Voltage2));
            }
        }
        public double MaxPlusVoltage
        {
            get => maxPlusVoltage;
            set
            {
                maxPlusVoltage = value;
                OnPropertyChanged(nameof(MaxPlusVoltage));
            }
        }
        public double SBSK 
        {
            get => sBSK;
            set
            {
                sBSK = value;
                OnPropertyChanged(nameof(SBSK));
            }
        }
        public BSKViewModel()
        {
            var list = GlobalGrid.GetInstance().Tree.GetElements();
            foreach (var i in list)
            {
                if (i.View.DataContext is LineViewModel line)
                {
                    var deltaUPercent = ((GlobalGrid.U - line.U2) / GlobalGrid.U) * 100;
                    if (deltaUPercent >= 10)
                    {
                        N = line.N;
                        K = line.K;
                        Voltage1 = Math.Round(line.U1, 5);
                        Voltage2 = Math.Round(line.U2, 5);
                        MaxPlusVoltage = Math.Round(GlobalGrid.U - line.U2, 5);
                        SBSK = Math.Round((MaxPlusVoltage * GlobalGrid.U - line.P1*line.R)/line.X, 5);
                        return;
                    }
                }
            }
        }
        public ICommand Close => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.StaticGrid.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.StaticGrid.Children[i].GetType().Name;
                if (childTypeName == nameof(BSKView))
                {
                    window.StaticGrid.Children.RemoveAt(i);
                }
            }
        });
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
