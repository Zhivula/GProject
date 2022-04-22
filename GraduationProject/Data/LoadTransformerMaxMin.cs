using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data
{
    public class LoadTransformerMaxMin : INotifyPropertyChanged
    {
        private LoadTransformer loadTransformerMax;
        private LoadTransformer loadTransformerMin;

        public LoadTransformer LoadTransformerMax
        {
            get => loadTransformerMax;
            set
            {
                loadTransformerMax = value;
                OnPropertyChanged(nameof(LoadTransformerMax));
            }
        }
        public LoadTransformer LoadTransformerMin
        {
            get => loadTransformerMin;
            set
            {
                loadTransformerMin = value;
                OnPropertyChanged(nameof(LoadTransformerMin));
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
