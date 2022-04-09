using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GraduationProject.DataBase;
using GraduationProject.Model;
using GraduationProject.View;

namespace GraduationProject.ViewModel
{
    class AddLineViewModel : INotifyPropertyChanged
    {
        private MainWindow window;
        private readonly AddLineModel Model;
        private string lineLength;
        private string selectedWireBrand;
        private double r0;
        private double x0;
        private double idop;

        public double R0
        {
            get => r0;
            set
            {
                r0 = value;
                OnPropertyChanged(nameof(R0));
            }
        }
        public double X0
        {
            get => x0;
            set
            {
                x0 = value;
                OnPropertyChanged(nameof(X0));
            }
        }
        public double Idop
        {
            get => idop;
            set
            {
                idop = value;
                OnPropertyChanged(nameof(Idop));
            }
        }
        public string LineLength
        {
            get => lineLength;
            set
            {
                lineLength = value;
                OnPropertyChanged(nameof(LineLength));
            }
        }
        public string SelectedWireBrand
        {
            get => selectedWireBrand;
            set
            {
                selectedWireBrand = value;
                OnPropertyChanged(nameof(SelectedWireBrand));
                using (var context = new MyDbContext())
                {
                    if(context.Lines.Count() > 0)
                    {
                        var item = context.Lines.Where(x => x.Brand == selectedWireBrand).Single();
                        R0 = item.R0;
                        X0 = item.X0;
                        Idop = item.Idop;
                    }
                }
            }
        }
        public ObservableCollection<string> SourceWireBrand { get; set; }

        public AddLineViewModel()
        {
            Model = new AddLineModel();
            window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            SourceWireBrand = Model.GetWireBrands();

            SelectedWireBrand = Model.SelectedWireBrand;

            LineLength = "1";
        }
        public ICommand AddLineCommand => new DelegateCommand(o =>
        {
            Model.AddLine(window, SelectedWireBrand, LineLength, R0, X0);
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
                if (childTypeName == nameof(AddLineView))
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