using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.ViewModel
{
    [Serializable]
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
        private double w1;
        private double w2;
        private double r0;
        private double x0;
        private double wp1;
        private double wp2;
        private double wq1;
        private double wq2;
        private double dWp;
        private double dWq;
        private double dU;
        private double r;
        private double x;
        private Dictionary<int,double> p2List;
        private Dictionary<int, double> p1List;
        private Dictionary<int, double> q2List;
        private Dictionary<int, double> q1List;
        private Dictionary<int, double> wp2List;
        private Dictionary<int, double> wp1List;
        private Dictionary<int, double> wq2List;
        private Dictionary<int, double> wq1List;

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
        public double W1
        {
            get => w1;
            set
            {
                w1 = value;
                OnPropertyChanged(nameof(W1));
            }
        }
        public double W2
        {
            get => w2;
            set
            {
                w2 = value;
                OnPropertyChanged(nameof(W2));
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
        public double DU
        {
            get => dU;
            set
            {
                dU = value;
                OnPropertyChanged(nameof(DU));
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
        public double R
        {
            get => r;
            set
            {
                r = value;
                OnPropertyChanged(nameof(R));
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
        public double X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged(nameof(X));
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

        /// <summary>
        /// Хранит активные мощности P2 протекающие по линии
        /// </summary>
        public Dictionary<int, double> P2List
        {
            get => p2List;
            set
            {
                p2List = value;
                OnPropertyChanged(nameof(P2List));
            }
        }
        /// <summary>
        /// Хранит активные мощности P1 протекающие по линии
        /// </summary>
        public Dictionary<int, double> P1List
        {
            get => p1List;
            set
            {
                p1List = value;
                OnPropertyChanged(nameof(P1List));
            }
        }
        /// <summary>
        /// Хранит реактивные мощности Q2 протекающие по линии
        /// </summary>
        public Dictionary<int, double> Q2List
        {
            get => q2List;
            set
            {
                q2List = value;
                OnPropertyChanged(nameof(Q2List));
            }
        }
        /// <summary>
        /// Хранит реактивные мощности Q1 протекающие по линии
        /// </summary>
        public Dictionary<int, double> Q1List
        {
            get => q1List;
            set
            {
                q1List = value;
                OnPropertyChanged(nameof(Q1List));
            }
        }

        /// <summary>
        /// Хранит активную энергию протекающую по концу линии
        /// </summary>
        public Dictionary<int, double> Wp2List
        {
            get => wp2List;
            set
            {
                wp2List = value;
                OnPropertyChanged(nameof(Wp2List));
            }
        }
        /// <summary>
        /// Хранит активную энергию протекающую по началу линии
        /// </summary>
        public Dictionary<int, double> Wp1List
        {
            get => wp1List;
            set
            {
                wp1List = value;
                OnPropertyChanged(nameof(Wp1List));
            }
        }
        /// <summary>
        /// Хранит реактивную энергию протекающую по концу линии
        /// </summary>
        public Dictionary<int, double> Wq2List
        {
            get => wq2List;
            set
            {
                wq2List = value;
                OnPropertyChanged(nameof(Wq2List));
            }
        }
        /// <summary>
        /// Хранит реактивную энергию протекающую по началу линии
        /// </summary>
        public Dictionary<int, double> Wq1List
        {
            get => wq1List;
            set
            {
                wq1List = value;
                OnPropertyChanged(nameof(Wq1List));
            }
        }

        public ButtonViewModel(string brand, double length, double r0, double x0)
        {
            P2List = new Dictionary<int, double>();
            P1List = new Dictionary<int, double>();
            Q2List = new Dictionary<int, double>();
            Q1List = new Dictionary<int, double>();
            Wp2List = new Dictionary<int, double>();
            Wp1List = new Dictionary<int, double>();
            Wq2List = new Dictionary<int, double>();
            Wq1List = new Dictionary<int, double>();
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
