using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data
{
    public class LoadTransformer : INotifyPropertyChanged
    {
        private string snom;
        private string cosfi;
        private string p;
        private string q;

        public int N { get; set; }
        public int K { get; set; }

        public string Snom
        {
            get => snom;
            set
            {
                snom = value;
                OnPropertyChanged(nameof(Snom));
                if (Snom != "" & Snom != null & Cosfi != null & P != null & Q != null)
                {
                    p = (double.Parse(Snom) * double.Parse(Cosfi)).ToString("#.####");
                    q = Math.Sqrt(Math.Pow(double.Parse(Snom), 2) - Math.Pow(double.Parse(p), 2)).ToString("#.####");
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
                cosfi = value;
                OnPropertyChanged(nameof(Cosfi));
                if (Cosfi != null && Snom != null && P != null && Q != null)
                {
                    p = (double.Parse(Snom) * double.Parse(Cosfi)).ToString("#.####");
                    q = Math.Sqrt(Math.Pow(double.Parse(Snom), 2) - Math.Pow(double.Parse(p), 2)).ToString("#.####");
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
                p = value;
                OnPropertyChanged(nameof(P));
                if (Cosfi != null && Snom != null && P != null && Q != null)
                {
                    snom = Math.Sqrt(Math.Pow(double.Parse(P), 2) + Math.Pow(double.Parse(Q), 2)).ToString("#.####");
                    cosfi = (double.Parse(P) / double.Parse(snom)).ToString("0.####");
                    OnPropertyChanged(nameof(Snom));
                    OnPropertyChanged(nameof(Cosfi));
                }
            }
        }
        public string Q
        {
            get => q;
            set
            {
                q = value;
                OnPropertyChanged(nameof(Q));
                if (Cosfi != null && Snom != null && P != null && Q != null)
                {
                    snom = Math.Sqrt(Math.Pow(double.Parse(Q), 2) + Math.Pow(double.Parse(P), 2)).ToString("#.####");
                    cosfi = (double.Parse(P) / double.Parse(snom)).ToString("0.####");
                    OnPropertyChanged(nameof(Snom));
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
