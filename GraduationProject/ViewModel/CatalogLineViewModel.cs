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
    class CatalogLineViewModel : INotifyPropertyChanged
    {
        private string brand;
        private string r0;
        private string x0;

        public List<Line> LinesList { get; set; }

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
        public CatalogLineViewModel()
        {
            LinesList = new List<Line>();
            using (var context = new MyDbContext())
            {
                foreach(var item in context.Lines)
                {
                    LinesList.Add(item);
                }
            }
            
        }
        public ICommand Add => new DelegateCommand(o =>
        {
            using (var context = new MyDbContext())
            {
                var item = new Line()
                {
                    Brand = Brand,
                    R0 = double.Parse(R0),
                    X0 = double.Parse(X0)
                };
                context.Lines.Add(item);
                context.SaveChanges();

                LinesList.Clear();

                foreach (var i in context.Lines)
                {
                    LinesList.Add(i);
                }
            }
            Brand = R0 = X0 = string.Empty;
        });
        public ICommand Return => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.GridChangeFirst.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.GridChangeFirst.Children[i].GetType().Name;
                if (childTypeName == "CatalogLineView")
                {
                    window.GridChangeFirst.Children.RemoveAt(i);
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
