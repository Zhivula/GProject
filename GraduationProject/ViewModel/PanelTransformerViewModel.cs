﻿using GraduationProject.View;
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
        private double deltaU;

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
        public double DeltaU
        {
            get => deltaU;
            set
            {
                deltaU = value;
                OnPropertyChanged(nameof(DeltaU));
            }
        }
        public PanelTransformerViewModel(TransformerView transformer)
        {
            var context = transformer.DataContext as TransformerViewModel;
            LineNumber = context.N.ToString() + "-" + context.K.ToString();
            P1 = Math.Round(context.P1,5);
            P2 = Math.Round(context.P2,5);
            Q1 = Math.Round(context.Q1,5);
            Q2 = Math.Round(context.Q2,5);
            R = Math.Round(context.R,5);
            X = Math.Round(context.X,5);
            dP = Math.Round(P1 - P2,5);
            dQ = Math.Round(Q1 - Q2,5);
            dU = Math.Round(context.U1 - context.U2,5);
            U1 = Math.Round(context.U1,5);
            U2 = Math.Round(context.U2,5);
            Brand = context.Brand;
            DeltaU = Math.Round(context.U1 - context.U2,5);
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
