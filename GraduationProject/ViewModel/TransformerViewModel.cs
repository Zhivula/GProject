using GraduationProject.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GraduationProject.ViewModel
{
    class TransformerViewModel : INotifyPropertyChanged
    {
        private int n;
        private int k;
        private bool flag;
        private string brand;
        private double p;
        private double q;
        private double s;
        private double kz;
        private Visibility visibility;
        private Transformer transformer;

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }
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
        public bool Flag
        {
            get => flag;
            set
            {
                flag = value;
                OnPropertyChanged(nameof(Flag));
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
        public double P
        {
            get => p;
            set
            {
                p = value;
                OnPropertyChanged(nameof(P));
            }
        }
        public double Q
        {
            get => q;
            set
            {
                q = value;
                OnPropertyChanged(nameof(Q));
            }
        }
        public double S
        {
            get => s;
            set
            {
                s = value;
                OnPropertyChanged(nameof(S));
            }
        }
        public double Kz
        {
            get => kz;
            set
            {
                kz = value;
                OnPropertyChanged(nameof(Kz));
                double cos = 0.8;
                S = transformer.Snom * Kz;
                P = S * cos;
                Q = S * Math.Sqrt(1 - cos * cos);
            }
        }
        public TransformerViewModel(Transformer transformer)
        {
            this.transformer = transformer;
            Kz = 1;
            double cos = 0.8;
            S = transformer.Snom * Kz;
            P = S * cos;
            Q = S * Math.Sqrt(1 - cos*cos);
            Brand = transformer.Brand;
        }
        //public ICommand DoubleClick => new DelegateCommand(o =>
        //{
        //    Visibility = Visibility.Visible;
        //});
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
