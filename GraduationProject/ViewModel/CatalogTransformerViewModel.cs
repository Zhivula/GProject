using GraduationProject.DataBase;
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
        private string brand;
        private string snom;
        private string pxx;
        private string qxx;
        private string ukz;
        private string ixx;
        private string pkz;
        private string r;
        private string x;
        private string tnb;

        public List<Transformer> TransformersList { get; set; }

        public string Brand
        {
            get => brand;
            set
            {
                brand = value;
                OnPropertyChanged(nameof(Brand));
            }
        }
        public string Snom
        {
            get => snom;
            set
            {
                snom = value;
                OnPropertyChanged(nameof(Snom));
            }
        }
        public string Pxx
        {
            get => pxx;
            set
            {
                pxx = value;
                OnPropertyChanged(nameof(Pxx));
            }
        }
        public string Qxx
        {
            get => qxx;
            set
            {
                qxx = value;
                OnPropertyChanged(nameof(Qxx));
            }
        }
        public string Ixx
        {
            get => ixx;
            set
            {
                ixx = value;
                OnPropertyChanged(nameof(Ixx));
            }
        }
        public string Ukz
        {
            get => ukz;
            set
            {
                ukz = value;
                OnPropertyChanged(nameof(Ukz));
            }
        }
        public string Pkz
        {
            get => pkz;
            set
            {
                pkz = value;
                OnPropertyChanged(nameof(Pkz));
            }
        }
        public string Tnb
        {
            get => tnb;
            set
            {
                tnb = value;
                OnPropertyChanged(nameof(Tnb));
            }
        }
        public string R
        {
            get => r;
            set
            {
                r = value;
                OnPropertyChanged(nameof(R));
            }
        }
        public string X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged(nameof(X));
            }
        }

        public CatalogTransformerViewModel()
        {
            TransformersList = new List<Transformer>();
            using (var context = new MyDbContext())
            {
                foreach (var item in context.Transformers)
                {
                    TransformersList.Add(item);
                }
            }
        }

        public ICommand Add => new DelegateCommand(o =>
        {
            using (var context = new MyDbContext())
            {
                var item = new Transformer() {
                    Brand = Brand,
                    Snom = double.Parse(Snom),
                    Pkz = double.Parse(Pkz),
                    Pxx = double.Parse(Pxx),
                    Qxx = double.Parse(Qxx),
                    Ixx = double.Parse(Ixx),
                    Ukz = double.Parse(Ukz),
                    Tnb = double.Parse(Tnb),
                    R = double.Parse(R),
                    X = double.Parse(X)
                };
                context.Transformers.Add(item);
                context.SaveChanges();

                TransformersList.Clear();

                foreach (var i in context.Transformers)
                {
                    TransformersList.Add(i);
                }
            }
            Brand = Snom = Pkz = Pxx = Qxx = Ixx = Ukz = Tnb = string.Empty;
        });
        public ICommand Return => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.StaticGrid.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.StaticGrid.Children[i].GetType().Name;
                if (childTypeName == "CatalogTransformerView")
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
