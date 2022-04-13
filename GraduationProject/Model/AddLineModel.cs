using GraduationProject.Data;
using GraduationProject.DataBase;
using GraduationProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Model
{
    public class AddLineModel : INotifyPropertyChanged
    {
        private string selectedWireBrand;

        public ObservableCollection<string> SourceWireBrand { get; set; }
        public string SelectedWireBrand
        {
            get => selectedWireBrand;
            set
            {
                selectedWireBrand = value;
                OnPropertyChanged(nameof(SelectedWireBrand));
            }
        }

        public AddLineModel()
        {
            SourceWireBrand = GetWireBrands();
            SelectedWireBrand = SourceWireBrand.FirstOrDefault();
        }
        public void AddLine(MainWindow window, string selectedWireBrand, string lineLength, double r0, double x0, double idop)
        {
            var global = GlobalGrid.GetInstance();
            var line = new LineView(selectedWireBrand, double.Parse(lineLength), r0, x0, idop) { Height = 50, Width = 100 };
            window.GridChange.Children.Add(line);
            window.curr = line;

            global.Lines.Add(line);
        }
        public ObservableCollection<string> GetWireBrands()
        {
            var list = new List<string>();
            using (var context = new MyDbContext())
            {
                if (context.Lines.Count() > 0) list = context.Lines.Select(x => x.Brand).ToList();
            }

            var SourceWireBrand = new ObservableCollection<string>();
            foreach (var i in list)
            {
                SourceWireBrand.Add(i);
            }
            return SourceWireBrand;
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
