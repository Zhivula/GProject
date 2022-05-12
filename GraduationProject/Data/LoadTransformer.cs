using System;
using System.ComponentModel;

namespace GraduationProject.Data
{
    public class LoadTransformer : INotifyPropertyChanged
    {
        private string sj;
        private string cosfi;
        private string p;
        private string q;

        public int N { get; set; }
        public int K { get; set; }

        public string Sj
        {
            get => sj;
            set
            {
                sj = value.Replace(".", ",");
                OnPropertyChanged(nameof(Sj));
                if (Sj != "" & Sj != null & Cosfi != null & P != null & Q != null)
                {
                    
                    p = (double.Parse(Sj) * double.Parse(Cosfi)).ToString("#.####");
                    q = Math.Sqrt(Math.Pow(double.Parse(Sj), 2) - Math.Pow(double.Parse(p), 2)).ToString("#.####");
                    OnPropertyChanged(nameof(P));
                    OnPropertyChanged(nameof(Q));
                }
            }
        }
        public string Cosfi
        {
            get => cosfi;
            set
            {
                cosfi = value.Replace(".", ",");
                OnPropertyChanged(nameof(Cosfi));
                if (Cosfi != null && Sj != null && P != null && Q != null)
                {
                    p = (double.Parse(Sj) * double.Parse(Cosfi)).ToString("#.####");
                    q = Math.Sqrt(Math.Pow(double.Parse(Sj), 2) - Math.Pow(double.Parse(p), 2)).ToString("#.####");
                    OnPropertyChanged(nameof(P));
                    OnPropertyChanged(nameof(Q));
                }
            }
        }
        public string P
        {
            get => p;
            set
            {
                p = value.Replace(".", ",");
                OnPropertyChanged(nameof(P));
                if (Cosfi != null && Sj != null && P != null && Q != null)
                {
                    sj = Math.Sqrt(Math.Pow(double.Parse(P), 2) + Math.Pow(double.Parse(Q), 2)).ToString("#.####");
                    cosfi = (double.Parse(P) / double.Parse(sj)).ToString("0.####");
                    OnPropertyChanged(nameof(Sj));
                    OnPropertyChanged(nameof(Cosfi));
                }
            }
        }
        public string Q
        {
            get => q;
            set
            {
                q = value.Replace(".", ",");
                OnPropertyChanged(nameof(Q));
                if (Cosfi != null && Sj != null && P != null && Q != null)
                {
                    sj = Math.Sqrt(Math.Pow(double.Parse(Q), 2) + Math.Pow(double.Parse(P), 2)).ToString("#.####");
                    cosfi = (double.Parse(P) / double.Parse(sj)).ToString("0.####");
                    OnPropertyChanged(nameof(Sj));
                    OnPropertyChanged(nameof(Cosfi));
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
