using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.ViewModel
{
    public class ButtonViewModel : INotifyPropertyChanged
    {
        private int n;
        private int k;
        private bool flag;
        private double length;
        private string brand;
        private double p1;
        private double p2;
        private double q1;
        private double q2;
        private float u1;
        private float u2;
        private double r0;
        private double x0;

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
        public double Length
        {
            get => length;
            set
            {
                length = value;
                OnPropertyChanged(nameof(Length));
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
        public float U1
        {
            get => u1;
            set
            {
                u1 = value;
                OnPropertyChanged(nameof(U1));
            }
        }
        public float U2
        {
            get => u2;
            set
            {
                u2 = value;
                OnPropertyChanged(nameof(U2));
            }
        }
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
        public ButtonViewModel(string brand, double length, double r0, double x0)
        {
            Brand = brand;
            Length = length;
            R0 = r0;
            X0 = x0;
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
