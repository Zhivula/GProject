using GraduationProject.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace GraduationProject.ViewModel
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        private bool checkedGrid;
        private string min;

        public bool CheckedGrid
        {
            get => checkedGrid;
            set
            {

                checkedGrid = value;
                OnPropertyChanged(nameof(CheckedGrid));
                var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (value == true)
                {
                    for (var i = 0; i < window.GridChange.Children.Count; i++)
                    {
                        if (window.GridChange.Children[i] is System.Windows.Shapes.Line) window.GridChange.Children[i].Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    for (var i = 0; i < window.GridChange.Children.Count; i++)
                    {
                        if (window.GridChange.Children[i] is System.Windows.Shapes.Line) window.GridChange.Children[i].Visibility = Visibility.Visible;
                    }
                }
            }
        }

        public string Min
        {
            get => min;
            set
            {
                min = value;
                OnPropertyChanged(nameof(Min));
            }
        }

        public SettingsViewModel()
        {
            using (var context = new MyDbContext())
            {
                Min = context.Settings.Select(x => x.Min).FirstOrDefault().ToString();
            }
        }
        public ICommand Return => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.GridChangeFirst.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.GridChangeFirst.Children[i].GetType().Name;
                if (childTypeName == "SettingsView")
                {
                    window.GridChangeFirst.Children.RemoveAt(i);
                }
            }
        });
        public ICommand SaveMin => new DelegateCommand(o =>
        {
            using (var context = new MyDbContext())
            {
                var item = context.Settings.FirstOrDefault();
                item.Min = double.Parse(Min);
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
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
