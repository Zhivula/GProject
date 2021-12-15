using GraduationProject.Data;
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
            SourceTransformerBrand = new ObservableCollection<string>() { "ТМ-30", "ТМ-60", "ТМ-63", "ТМ-100", "ТМ-400", "ТМ-600" };
            SelectedTransformerBrand = SourceTransformerBrand.First();
        }
        public ICommand AddTransformerCommand => new DelegateCommand(o =>
        {
            var global = GlobalGrid.GetInstance();
            var transformer = new TransformerView() { Height = 50, Width = 100 };
            //line.UTextBlock.Text = "0";
            //line.R0 = R0;
            //line.X0 = X0;
            //line.Length = float.Parse(LineLength);
            OnPropertyChanged(nameof(transformer));
            Close();
            //line.MainButton.Click += new RoutedEventHandler(MouseEvent);
            window.GridChange.Children.Add(transformer);
            window.curr = transformer;

            //global.Lines.Add(transformer);
            //var global = GlobalGrid.GetInstance();
            //var transformer = new Transformer();
            //transformer.Brand = SelectedTransformerBrand;
            //transformer.Snom = int.Parse(Pnom);
            //Close();
            //transformer.Click += new RoutedEventHandler(MouseEvent);
            //window.GridChange.Children.Add(transformer);
            //window.curr = transformer;
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
            for (int i = window.GridChange.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.GridChange.Children[i].GetType().Name;
                if (childTypeName == "AddTransformerView")
                {
                    window.GridChange.Children.RemoveAt(i);
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