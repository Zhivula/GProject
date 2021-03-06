using GraduationProject.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.ViewModel
{
    class PanelTransformerViewModel : INotifyPropertyChanged
    {
        private string lineNumber;
        private string brand;
        private double p1;
        private double p2;
        private double q1;
        private double q2;
        private double u1;
        private double u2;
        private double dp;
        private double dq;
        private double du;
        private double r;
        private double x;
        private double l;
        private double wp1;
        private double wp2;
        private double wq1;
        private double wq2;

        public string LineNumber
        {
            get => lineNumber;
            set
            {
                lineNumber = value;
                OnPropertyChanged(nameof(LineNumber));
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
        public double U1
        {
            get => u1;
            set
            {
                u1 = value;
                OnPropertyChanged(nameof(U1));
            }
        }
        public double U2
        {
            get => u2;
            set
            {
                u2 = value;
                OnPropertyChanged(nameof(U2));
            }
        }
        public double dP
        {
            get => dp;
            set
            {
                dp = value;
                OnPropertyChanged(nameof(dP));
            }
        }
        public double dQ
        {
            get => dq;
            set
            {
                dq = value;
                OnPropertyChanged(nameof(dQ));
            }
        }
        public double dU
        {
            get => du;
            set
            {
                du = value;
                OnPropertyChanged(nameof(dU));
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
        public double L
        {
            get => l;
            set
            {
                l = value;
                OnPropertyChanged(nameof(L));
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
        public PanelTransformerViewModel(TransformerView transformer)
        {
            var context = transformer.DataContext as TransformerViewModel;
            LineNumber = context.N.ToString() + "-" + context.K.ToString();
            P1 = context.P1;
            P2 = context.P2;
            Q1 = context.Q1;
            Q2 = context.Q2;
            R = context.R;
            X = context.X;
            //U2 = context.U2;
            //U1 = context.U1;
            dP = P1 - P2;
            dQ = Q1 - Q2;
            dU = U1 - U2;
            Brand = context.Brand;
            Wp1 = context.Wp1;
            Wp2 = context.Wp2;
            Wq1 = context.Wq1;
            Wq2 = context.Wq2;
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
