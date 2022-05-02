using GraduationProject.DataBase;
using GraduationProject.Model;
using GraduationProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GraduationProject.ViewModel
{
    class CatalogTransformerViewModel : INotifyPropertyChanged
    {
        public CatalogTransformerModel Model;
        private string brand;
        private double snom;
        private double pxx;
        private double unom;
        private double ukz;
        private double ixx;
        private double pkz;
        //private string r;
        //private string x;

        public ObservableCollection<Transformer> TransformersList { get; set; }

        public string Brand
        {
            get => brand;
            set
            {
                brand = value;
                OnPropertyChanged(nameof(Brand));
            }
        }
        public double Snom
        {
            get => snom;
            set
            {
                snom = value;
                OnPropertyChanged(nameof(Snom));
            }
        }
        public double Pxx
        {
            get => pxx;
            set
            {
                pxx = value;
                OnPropertyChanged(nameof(Pxx));
            }
        }
        public double Unom
        {
            get => unom;
            set
            {
                unom = value;
                OnPropertyChanged(nameof(Unom));
            }
        }
        public double Ixx
        {
            get => ixx;
            set
            {
                ixx = value;
                OnPropertyChanged(nameof(Ixx));
            }
        }
        public double Ukz
        {
            get => ukz;
            set
            {
                ukz = value;
                OnPropertyChanged(nameof(Ukz));
            }
        }
        public double Pkz
        {
            get => pkz;
            set
            {
                pkz = value;
                OnPropertyChanged(nameof(Pkz));
            }
        }
        //public string R
        //{
        //    get => r;
        //    set
        //    {
        //        r = value;
        //        OnPropertyChanged(nameof(R));
        //    }
        //}
        //public string X
        //{
        //    get => x;
        //    set
        //    {
        //        x = value;
        //        OnPropertyChanged(nameof(X));
        //    }
        //}

        public CatalogTransformerViewModel()
        {
            Model = new CatalogTransformerModel();
            TransformersList = Model.Collection;
        }

        public ICommand Add => new DelegateCommand(o =>
        {
            Model.Add(Brand, Snom, Pkz, Pxx, Ixx, Ukz, Unom);
            Brand = string.Empty;
            Snom = Pkz = Pxx = Ixx = Ukz = Unom = 0;
        });
        public ICommand Return => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.StaticGrid.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.StaticGrid.Children[i].GetType().Name;
                if (childTypeName == nameof(CatalogTransformerView))
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
