using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.ViewModel
{
    class SourceViewModel : INotifyPropertyChanged
    {
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

        public SourceViewModel(string name, double voltage)
        {
            Name = name;
            Voltage = voltage;
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
