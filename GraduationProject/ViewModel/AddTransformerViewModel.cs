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
        private double snom;
        private double s;
        private double p;
        private double q;
        private double kz; 
        private double cosfi;
        private string selectedTransformerBrand;
        private Transformer transformer;

        public string Pnom
        {
            get => pnom;
            set
            {
                pnom = value;
                OnPropertyChanged(nameof(Pnom));
            }
        }
        public double Snom
        {
            get => snom;
            set
            {
                snom = value;
                OnPropertyChanged(nameof(Snom));
            }
        }
        public double S
        {
            get => s;
            set
            {
                s = value;
                OnPropertyChanged(nameof(S));
            }
        }
        public double P
        {
            get => p;
            set
            {
                p = value;
                OnPropertyChanged(nameof(P));
            }
        }
        public double Q
        {
            get => q;
            set
            {
                q = value;
                OnPropertyChanged(nameof(Q));
            }
        }
        public double Kz
        {
            get => kz;
            set
            {
                kz = value;
                OnPropertyChanged(nameof(Kz));
                s = Snom * Kz;
                cosfi = Math.Round(0.7, 4);
                p = S * Cosfi;
                q = Math.Sqrt(Math.Pow(S, 2) - Math.Pow(P, 2));

                OnPropertyChanged(nameof(S));
                OnPropertyChanged(nameof(Cosfi));
                OnPropertyChanged(nameof(P));
                OnPropertyChanged(nameof(Q));
            }
        }
        public double Cosfi
        {
            get => cosfi;
            set
            {
                cosfi = value;
                OnPropertyChanged(nameof(Cosfi));

                p = S * Cosfi;
                q = Math.Sqrt(Math.Pow(S, 2) - Math.Pow(P, 2));

                OnPropertyChanged(nameof(P));
                OnPropertyChanged(nameof(Q));
            }
        }
        public string SelectedTransformerBrand
        {
            get => selectedTransformerBrand;
            set
            {
                selectedTransformerBrand = value;
                OnPropertyChanged(nameof(SelectedTransformerBrand));
                using (var context = new MyDbContext())
                {
                    transformer = context.Transformers.Where(x => x.Brand == selectedTransformerBrand).FirstOrDefault();
                    Snom = transformer.Snom;
                    Kz = 1;
                    S = Snom*Kz;
                    Cosfi = Math.Round(0.7,4);
                    P = Math.Round(S * Cosfi, 4);
                    Q = Math.Round(Math.Sqrt(Math.Pow(S,2) - Math.Pow(P,2)), 4);
                }
            }
        }

        public AddTransformerViewModel()
        {
            transformer = new Transformer();
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
            SelectedTransformerBrand = SourceTransformerBrand.FirstOrDefault();
        }
        public ICommand AddTransformerCommand => new DelegateCommand(o =>
        {
            var global = GlobalGrid.GetInstance();

            var view = new TransformerView(transformer, S, Cosfi) { Height = 50, Width = 100 };
            Close();
            
            window.GridChange.Children.Add(view);
            window.curr = view;

            global.Transformers.Add(view);
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
                if (childTypeName == nameof(AddTransformerView))
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