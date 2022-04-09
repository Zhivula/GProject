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
    class CatalogLineViewModel : INotifyPropertyChanged
    {
        public CatalogLineModel Model;
        private string brand;
        private string r0;
        private string x0;
        private string idop;

        public ObservableCollection<Line> LinesList { get; set; }
        
        public string Brand
        {
            get => brand;
            set
            {
                brand = value;
                OnPropertyChanged(nameof(Brand));
            }
        }
        public string R0
        {
            get => r0;
            set
            {
                r0 = value;
                OnPropertyChanged(nameof(R0));
            }
        }
        public string X0
        {
            get => x0;
            set
            {
                x0 = value;
                OnPropertyChanged(nameof(X0));
            }
        }
        public string Idop
        {
            get => idop;
            set
            {
                idop = value;
                OnPropertyChanged(nameof(Idop));
            }
        }

        public CatalogLineViewModel()
        {
            Model = new CatalogLineModel();
            LinesList = Model.Collection; 
        }
        public ICommand Add => new DelegateCommand(o =>
        {
            Model.Add(Brand, R0, X0, Idop);
            Brand = R0 = X0 = Idop = string.Empty;
        });
        public ICommand Return => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.StaticGrid.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.StaticGrid.Children[i].GetType().Name;
                if (childTypeName == nameof(CatalogLineView))
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
