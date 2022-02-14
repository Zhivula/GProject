using GraduationProject.Data;
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
        private double p1;
        private double p2;
        private double q1;
        private double q2;
        private double sj;
        private double wp1;
        private double wp2;
        private double wq1;
        private double wq2;
        private double kz;
        private double r;
        private double x;
        private double dPj;
        private double dQj;
        private double dWp;
        private double dWq;
        public double Tnb;

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
        public double P1
        {
            get => p1;
            set
            {
                p1 = value;
                OnPropertyChanged(nameof(P1));
            }
        }
        public double P2
        {
            get => p2;
            set
            {
                p2 = value;
                OnPropertyChanged(nameof(P2));
            }
        }
        public double Q1
        {
            get => q1;
            set
            {
                q1 = value;
                OnPropertyChanged(nameof(Q1));
            }
        }
        public double Q2
        {
            get => q2;
            set
            {
                q2 = value;
                OnPropertyChanged(nameof(Q2));
            }
        }
        public double Sj
        {
            get => sj;
            set
            {
                sj = value;
                OnPropertyChanged(nameof(Sj));
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
                Sj = transformer.Snom * Kz;
                P2 = Sj * cos;
                Q2 = Sj * Math.Sqrt(1 - cos * cos);

                dPj = (P2 * P2 + Q2 * Q2) * R / (10.5f * 10.5f * 1000);
                dQj = (P2 * P2 + Q2 * Q2) * X / (10.5f * 10.5f * 1000);

                P1 = P2 + dPj + transformer.Pxx;
                Q1 = Q2 + dQj + transformer.Qxx;
            }
        }
        public double R
        {
            get => r;
            set
            {
                r = value;
                OnPropertyChanged(nameof(R));
            }
        }
        public double X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged(nameof(X));
            }
        }
        public double DPj
        {
            get => dPj;
            set
            {
                dPj = value;
                OnPropertyChanged(nameof(DPj));
            }
        }
        public double DQj
        {
            get => dQj;
            set
            {
                dQj = value;
                OnPropertyChanged(nameof(DQj));
            }
        }
        public double Wp1
        {
            get => wp1;
            set
            {
                wp1 = value;
                OnPropertyChanged(nameof(Wp1));
            }
        }
        public double Wp2
        {
            get => wp2;
            set
            {
                wp2 = value;
                OnPropertyChanged(nameof(Wp2));
            }
        }
        public double Wq1
        {
            get => wq1;
            set
            {
                wq1 = value;
                OnPropertyChanged(nameof(Wq1));
            }
        }
        public double Wq2
        {
            get => wq2;
            set
            {
                wq2 = value;
                OnPropertyChanged(nameof(Wq2));
            }
        }
        public double DWp
        {
            get => dWp;
            set
            {
                dWp = value;
                OnPropertyChanged(nameof(DWp));
            }
        }
        public double DWq
        {
            get => dWq;
            set
            {
                dWq = value;
                OnPropertyChanged(nameof(DWq));
            }
        }
        public TransformerViewModel(Transformer transformer)
        {
            this.transformer = transformer;
            Kz = 1;
            double cos = 0.8;

            R = transformer.R;
            X = transformer.X;

            Sj = transformer.Snom * Kz;
            P2 = Sj * cos;
            Q2 = Sj * Math.Sqrt(1 - cos*cos);

            DPj = (P2 * P2 + Q2 * Q2) * R/ (10.5f * 10.5f * 1000);
            DQj = (P2 * P2 + Q2 * Q2) * X/ (10.5f * 10.5f * 1000);

            P1 = P2 + DQj + transformer.Pxx;
            Q1 = Q2 + DQj + transformer.Qxx;

            Wp2 = transformer.Tnb * P2;
            Wq2 = transformer.Tnb * Q2;

            var K2f = Math.Pow((0.16 / Kz) + 0.82, 2);
            DWp = ((Wp2 * Wp2 + Wq2 * Wq2) * R * K2f) / (10.5f * 10.5f * 1000 * GlobalGrid.T);
            DWq = ((Wp2 * Wp2 + Wq2 * Wq2) * X * K2f) / (10.5f * 10.5f * 1000 * GlobalGrid.T);

            Wp1 = Wp2 + DPj*GlobalGrid.T + DWp;
            Wq1 = Wq2 + DQj * GlobalGrid.T + DWq;

            Brand = transformer.Brand;
            Tnb = transformer.Tnb;
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
