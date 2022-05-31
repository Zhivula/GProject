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

namespace GraduationProject.ViewModel
{
    public class VDTViewModel : INotifyPropertyChanged
    {
        private int n;
        private int k;
        private double voltage1;
        private double voltage2;
        private double maxPlusVoltage;
        private double sVDT;
        private string selectedItem;

        public int N
        {
            get => n;
            set
            {
                n = value;
                OnPropertyChanged(nameof(N));
            }
        }
        public int K
        {
            get => k;
            set
            {
                k = value;
                OnPropertyChanged(nameof(K));
            }
        }
        public double Voltage1
        {
            get => voltage1;
            set
            {
                voltage1 = value;
                OnPropertyChanged(nameof(Voltage1));
            }
        }
        public double Voltage2
        {
            get => voltage2;
            set
            {
                voltage2 = value;
                OnPropertyChanged(nameof(Voltage2));
            }
        }
        public double MaxPlusVoltage
        {
            get => maxPlusVoltage;
            set
            {
                maxPlusVoltage = value;
                OnPropertyChanged(nameof(MaxPlusVoltage));
            }
        }
        public double SVDT
        {
            get => sVDT;
            set
            {
                sVDT = value;
                OnPropertyChanged(nameof(SVDT));
            }
        }
        public string SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));

                Node<int> item = null;
                var listFUL = GetElements().Where(x => x.View is LineView).ToList();
                foreach (var i in listFUL)
                {
                    var context = i.View.DataContext as LineViewModel;
                    if (selectedItem == (context.N.ToString() + "-" + context.K.ToString()))
                    {
                        item = i;
                        continue;
                    }
                }
                if (item.View.DataContext is LineViewModel line)
                {
                    Line = item.View;
                    var datacontext = item.View.DataContext as LineViewModel;
                    N = line.N;
                    K = line.K;
                    Voltage1 = Math.Round(datacontext.U1, 5);
                    Voltage2 = Math.Round(datacontext.U2, 5);
                    MaxPlusVoltage = Math.Round(GlobalGrid.U - datacontext.U1, 5);
                    SVDT = Math.Round(Math.Sqrt(datacontext.P1 * datacontext.P1 + datacontext.Q1 * datacontext.Q1) * (MaxPlusVoltage / GlobalGrid.U), 5);
                    node = item;
                    return;

                }
            }
        }
        public ObservableCollection<string> ComboboxSource { get; set; }
        private UserControl Line;
        private Node<int> node;

        public VDTViewModel()
        {
            ComboboxSource = GetLinesBranchesFormat();
            var list = GlobalGrid.GetInstance().Tree.GetElements();
            foreach (var i in list)
            {
                if (i.View.DataContext is LineViewModel line)
                {
                    var deltaUPercent = ((GlobalGrid.U - line.U2) / GlobalGrid.U) * 100;
                    if (deltaUPercent >= 10)
                    {
                        Line = i.View;
                        var datacontext = i.View.DataContext as LineViewModel;
                        N = line.N;
                        K = line.K;
                        Voltage1 = Math.Round(datacontext.U1, 5);
                        Voltage2 = Math.Round(datacontext.U2, 5);
                        MaxPlusVoltage = Math.Round(GlobalGrid.U - datacontext.U1, 5);
                        SVDT = Math.Round(Math.Sqrt(datacontext.P1 * datacontext.P1 + datacontext.Q1 * datacontext.Q1) * (MaxPlusVoltage / GlobalGrid.U), 5);
                        node = i;
                        return;
                    }
                }
            }


            //Позволяет устанавлить ВДТ даже если нпряжение не опускается ниже допустимого
            if (Line == null)
            {
                foreach (var i in list)
                {
                    if (i.View.DataContext is LineViewModel line)
                    {
                        var deltaUPercent = ((GlobalGrid.U - line.U2) / GlobalGrid.U) * 100;
                        if (deltaUPercent >= 5)
                        {
                            Line = i.Parent.View;
                            var datacontext = i.Parent.View.DataContext as LineViewModel;
                            N = line.N;
                            K = line.K;
                            Voltage1 = Math.Round(datacontext.U1, 5);
                            Voltage2 = Math.Round(datacontext.U2, 5);
                            MaxPlusVoltage = Math.Round(GlobalGrid.U - datacontext.U2, 5);
                            SVDT = Math.Round(Math.Sqrt(datacontext.P1 * datacontext.P1 + datacontext.Q1 * datacontext.Q1) * (MaxPlusVoltage / GlobalGrid.U), 5);
                            node = i;
                            return;
                        }
                    }
                }
            }
        }
        private ObservableCollection<string> GetLinesBranchesFormat()
        {
            var listFUL = GetElements().Where(x => x.View is LineView).ToList();
            var list = new ObservableCollection<string>();
            foreach (var i in listFUL)
            {
                var item = i.View.DataContext as LineViewModel;
                list.Add(item.N.ToString()+"-"+item.K.ToString());
            }
            return list;
        }
        private List<Node<int>> GetElements()
        {
            var global = GlobalGrid.GetInstance();
            return global.Tree.GetElements();
        }
        public ICommand StartCommand => new DelegateCommand(o =>
        {
            if(Line != null)
            {
                GlobalGrid.VDT = true;
                GlobalGrid.BSK = false;
                var chartVDTViewModel = new ChartVDTViewModel("ВДТ");
                var chartBeforeVDT = new ChartBeforeVDT();
                ChartBeforeVDT.plotBefore = chartVDTViewModel.GetPlotModel();//Как бы сохранение исходного графика до установки ВДТ
                ChartBeforeVDT.dictionarySecondBefore = chartVDTViewModel.GetDataChartSecond(5);
                ChartBeforeVDT.dictionaryFirstBefore = chartVDTViewModel.GetDataChart(5);

                var line = Line as LineView;
                var context = line.DataContext as LineViewModel;
                context.VisibilytyVDT = Visibility.Visible;
                context.U2 = (float)GlobalGrid.U;
                var parent = (node.Parent.View as LineView).DataContext as LineViewModel;

                var deltaUPercentFirst = ((GlobalGrid.U - parent.U1) / GlobalGrid.U) * 100;
                var deltaUPercentLast = ((GlobalGrid.U - parent.U2) / GlobalGrid.U) * 100;

                if (deltaUPercentFirst >= 10)
                {
                    parent.Opacity = 1;
                    parent.ColorNode = context.ColorNode = new SolidColorBrush(Colors.Red);
                }
                if (deltaUPercentFirst >= 5 && deltaUPercentFirst < 10)
                {
                    parent.Opacity = 1;
                    parent.ColorNode = context.ColorNode = new SolidColorBrush(Colors.Yellow);
                }
                if (deltaUPercentFirst < 5)
                {
                    parent.Opacity = 1;
                    parent.ColorNode = context.ColorNode = new SolidColorBrush(Colors.Green);
                }
                if (deltaUPercentLast >= 10)
                {
                    parent.Opacity = 1;
                    parent.ColorNodeLast = context.ColorNode = new SolidColorBrush(Colors.Red);
                }
                if (deltaUPercentLast >= 5 && deltaUPercentLast < 10)
                {
                    parent.Opacity = 1;
                    parent.ColorNodeLast = context.ColorNode = new SolidColorBrush(Colors.Yellow);
                }
                if (deltaUPercentLast < 5)
                {
                    parent.Opacity = 1;
                    parent.ColorNodeLast = context.ColorNode = new SolidColorBrush(Colors.Green);
                }

                foreach (var i in node.List)
                {
                    i.Volt((float)GlobalGrid.U);
                }
            }
            CloseUserControl();
        });
        public ICommand Close => new DelegateCommand(o =>
        {
            CloseUserControl();
        });
        private void CloseUserControl()
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.StaticGrid.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.StaticGrid.Children[i].GetType().Name;
                if (childTypeName == nameof(VDTView))
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
