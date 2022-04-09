using GraduationProject.Data;
using GraduationProject.Model;
using GraduationProject.View;
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
    public class AddSourceViewModel : INotifyPropertyChanged
    {
        MainWindow window;
        private readonly AddSourceModel Model;
        private double voltage;
        private string name;

        public double Voltage
        {
            get => voltage;
            set
            {
                voltage = value;
                OnPropertyChanged(nameof(Voltage));
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }


        public AddSourceViewModel()
        {
            Model = new AddSourceModel();
            window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        }
        public ICommand AddSourceCommand => new DelegateCommand(o =>
        {
            Model.AddSource(window, Name, Voltage);
            Close();
        });
        public ICommand CloseWindow => new DelegateCommand(o =>
        {
            Close();
        });
        private void Close()
        {
            for (int i = window.StaticGrid.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.StaticGrid.Children[i].GetType().Name;
                if (childTypeName == nameof(AddSourceView))
                {
                    window.StaticGrid.Children.RemoveAt(i);
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
