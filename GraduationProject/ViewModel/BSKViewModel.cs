using GraduationProject.Data;
using GraduationProject.View;
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
    public class BSKViewModel : INotifyPropertyChanged
    {
        private int n;
        private int k;
        private double voltage1;
        private double voltage2;
        private double maxPlusVoltage;
        private double sBSK;
        private double sBSKFull;
        private int count;
        private double sBattery;
        private string name;
        private LineViewModel line;
        private Node<int> node;

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
        public double SBSK 
        {
            get => sBSK;
            set
            {
                sBSK = value;
                OnPropertyChanged(nameof(SBSK));
            }
        }
        public double SBSKFull
        {
            get => sBSKFull;
            set
            {
                sBSKFull = value;
                OnPropertyChanged(nameof(SBSKFull));
            }
        }
        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged(nameof(Count));
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
        public double SBattery
        {
            get => sBattery;
            set
            {
                sBattery = value;
                OnPropertyChanged(nameof(SBattery));
            }
        }
        public BSKViewModel()
        {
            var list = GlobalGrid.GetInstance().Tree.GetElements();
            var listBSK = new List<BSK>();
            listBSK.Add(new BSK() { Name = "БСК-10-2,5 УХЛ1", C = 79.58, Q = 250 });
            listBSK.Add(new BSK() { Name = "БСК-10-3,75 УХЛ1", C = 119.37, Q = 375 });
            listBSK.Add(new BSK() { Name = "БСК-10-5 УХЛ1", C = 159.15, Q = 500 });
            listBSK.Add(new BSK() { Name = "БСК-10-5,65 УХЛ1", C = 179.85, Q = 565 });
            listBSK.Add(new BSK() { Name = "БСК-10-7,5 УХЛ1", C = 238.73, Q = 750 });
            listBSK.Add(new BSK() { Name = "БСК-10-8,75 УХЛ1", C = 278.52, Q = 875 });
            listBSK.Add(new BSK() { Name = "БСК-10-10 УХЛ1", C = 318.31, Q = 1000 });
            listBSK.Add(new BSK() { Name = "БСК-10-11,25 УХЛ1", C = 358.10, Q = 1130 });

            foreach (var i in list)
            {
                if (i.View.DataContext is LineViewModel line)
                {
                    var deltaUPercent = ((GlobalGrid.U - line.U2) / GlobalGrid.U) * 100;
                    if (deltaUPercent >= 10)
                    {
                        this.line = line;
                        node = i;
                        N = line.N;
                        K = line.K;
                        Voltage1 = Math.Round(line.U1, 5);
                        Voltage2 = Math.Round(line.U2, 5);
                        MaxPlusVoltage = Math.Round(GlobalGrid.U - line.U2, 5);
                        //SBSK = Math.Round((MaxPlusVoltage * GlobalGrid.U - line.P1*line.R)/line.X, 5);
                        SBSKFull = (((GlobalGrid.U - line.U2) / line.X) * GlobalGrid.U)*1000;
                        if (SBSKFull > line.Q1) SBSKFull = line.Q1 * 0.9;

                        var minOstatok = SBSKFull / listBSK.First().Q - (int)SBSKFull / (int)listBSK.First().Q;
                        var min = listBSK.First();
                        foreach (var o in listBSK)
                        {
                            var ostatok = SBSKFull % o.Q - (int)SBSKFull / (int)o.Q;
                            if (ostatok < minOstatok)
                            {
                                min = o;
                                minOstatok = ostatok;
                            }
                        }
                        
                        Name = min.Name;
                        Count = (int) SBSKFull/(int)min.Q;
                        SBSK = Count * min.Q;
                        SBattery = min.Q;
                        return;
                    }
                }
            }
            CloseUserControl();
        }
        public ICommand StartCommand => new DelegateCommand(o =>
        {
            if (line != null)
            {
                line.VisibilytyBSK = Visibility.Visible;
                //line.QBSK = SBSK;
                //line.U2 = (float)GlobalGrid.U;
                node.AddBSK(-SBSK);
                //foreach (var i in node.List)
                //{
                //    i.Volt((float)GlobalGrid.U);
                //}
                CloseUserControl();
            }
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
                if (childTypeName == nameof(BSKView))
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
