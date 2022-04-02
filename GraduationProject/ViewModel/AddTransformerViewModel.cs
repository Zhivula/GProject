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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraduationProject.ViewModel
{
    class AddTransformerViewModel : INotifyPropertyChanged
    {
        MainWindow window;
        public ObservableCollection<string> SourceTransformerBrand { get; set; }
        private string pnom;
        private string selectedTransformerBrand;

        public string Pnom
        {
            get => pnom;
            set
            {
                pnom = value;
                OnPropertyChanged(nameof(Pnom));
            }
        }
        public string SelectedTransformerBrand
        {
            get => selectedTransformerBrand;
            set
            {
                selectedTransformerBrand = value;
                OnPropertyChanged(nameof(SelectedTransformerBrand));
            }
        }

        public AddTransformerViewModel()
        {
            window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            var list = new List<string>();
            using (var context = new MyDbContext())
            {
                list = context.Transformers.Select(x => x.Brand).ToList();
            }

            SourceTransformerBrand = new ObservableCollection<string>();
            foreach (var i in list)
            {
                SourceTransformerBrand.Add(i);
            }
            SelectedTransformerBrand = SourceTransformerBrand.First();
        }
        public ICommand AddTransformerCommand => new DelegateCommand(o =>
        {
            var global = GlobalGrid.GetInstance();
            var item = new Transformer();
            using (var context = new MyDbContext())
            {
                item = context.Transformers.Where(x => x.Brand == SelectedTransformerBrand).Single();
            }
            var transformer = new TransformerView(item) { Height = 50, Width = 100 };
            OnPropertyChanged(nameof(transformer));
            Close();
            
            window.GridChange.Children.Add(transformer);
            window.curr = transformer;

            global.Transformers.Add(transformer);
        });
        private void MouseEvent(object sender, RoutedEventArgs e)
        {
            window.curr = sender;
        }
        public ICommand CloseWindow => new DelegateCommand(o =>
        {
            Close();
        });
        private void Close()
        {
            for (int i = window.StaticGrid.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.StaticGrid.Children[i].GetType().Name;
                if (childTypeName == "AddTransformerView")
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