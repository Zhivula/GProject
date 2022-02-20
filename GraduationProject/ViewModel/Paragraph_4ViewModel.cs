using GraduationProject.Data;
using GraduationProject.Model;
using OxyPlot;
using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GraduationProject.ViewModel
{
    class Paragraph_4ViewModel : INotifyPropertyChanged
    {
        private float max;
        private float min;
        private int count;
        private float step;
        private string theBestKz;
        private float pl;
        private float pt;
        private float bnl;
        private float bxt;
        private float bnt;
        private float kty;
        private float kly;
        private PlotModel plotCp;
        private PlotModel plotA;
        private PlotModel plotB;
        private PlotModel plotC;
        private List<Losses> items;

        public float Min
        {
            get => min;
            set
            {
                min = value;
                OnPropertyChanged(nameof(Min));
                Max = Min + (Count - 1) * Step;
            }
        }
        public float Max
        {
            get => max;
            set
            {
                max = value;
                OnPropertyChanged(nameof(Max));
            }
        }
        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged(nameof(Count));
                Max = Min + (Count - 1) * Step;
            }
        }
        public float Step
        {
            get => step;
            set
            {
                step = value;
                OnPropertyChanged(nameof(Step));
                Max = Min + (Count - 1) * Step;
            }
        }
        public string TheBestKz
        {
            get => theBestKz;
            set
            {
                theBestKz = value;
                OnPropertyChanged(nameof(TheBestKz));
            }
        }

        public float Pl
        {
            get => pl;
            set
            {
                pl = value;
                OnPropertyChanged(nameof(Pl));
            }
        }
        public float Pt
        {
            get => pt;
            set
            {
                pt = value;
                OnPropertyChanged(nameof(Pt));
            }
        }
        public float Bnl
        {
            get => bnl;
            set
            {
                bnl = value;
                OnPropertyChanged(nameof(Bnl));
            }
        }
        public float Bxt
        {
            get => bxt;
            set
            {
                bxt = value;
                OnPropertyChanged(nameof(Bxt));
            }
        }
        public float Bnt
        {
            get => bnt;
            set
            {
                bnt = value;
                OnPropertyChanged(nameof(Bnt));
            }
        }
        public float Kty
        {
            get => kty;
            set
            {
                kty = value;
                OnPropertyChanged(nameof(Kty));
            }
        }
        public float Kly
        {
            get => kly;
            set
            {
                kly = value;
                OnPropertyChanged(nameof(Kly));
            }
        }

        public List<Losses> ItemsSource
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged(nameof(ItemsSource));
            }
        }

        public PlotModel PlotA
        {
            get => plotA;
            set
            {
                plotA = value;
                OnPropertyChanged(nameof(PlotA));
            }
        }
        public PlotModel PlotB
        {
            get => plotB;
            set
            {
                plotB = value;
                OnPropertyChanged(nameof(PlotB));
            }
        }
        public PlotModel PlotC
        {
            get => plotC;
            set
            {
                plotC = value;
                OnPropertyChanged(nameof(PlotC));
            }
        }
        public PlotModel PlotCp
        {
            get => plotCp;
            set
            {
                plotCp = value;
                OnPropertyChanged(nameof(PlotCp));
            }
        }

        public Paragraph_4ViewModel()
        {
            ItemsSource = new List<Losses>();
        }
        public List<PlotModel> GetPlotModel()
        {
            var plotList = new List<PlotModel>();

            var lineA = new OxyPlot.Series.LineSeries()
            {
                Color = OxyColor.FromRgb(22, 186, 124),
                StrokeThickness = 2,
                MarkerSize = 3,
                MarkerType = MarkerType.Circle
            };

            var lineB = new OxyPlot.Series.LineSeries()
            {
                Color = OxyColor.FromRgb(24, 6, 189),
                StrokeThickness = 2,
                MarkerSize = 3,
                MarkerType = MarkerType.Circle
            };
            var lineC = new OxyPlot.Series.LineSeries()
            {
                Color = OxyColor.FromRgb(189, 33, 6),
                StrokeThickness = 2,
                MarkerSize = 3,
                MarkerType = MarkerType.Circle
            };
            var lineCp = new OxyPlot.Series.LineSeries()
            {
                Color = OxyColor.FromRgb(2, 191, 212),
                StrokeThickness = 2,
                MarkerSize = 3,
                MarkerType = MarkerType.Circle
            };
            for (int i = 0; i < Count; i++)
            {
                ChangedKz(Min + Step * i);

                var dWxx = GetdWxx();
                var dWnt = GetdWnt();
                var dWnl = GetdWnl();
                var dW = dWxx + dWnt + dWnl;

                var root = GlobalGrid.GetInstance().Tree.Root;

                if (root != null)
                {
                    var firstLine = root.View.DataContext as ButtonViewModel;

                    var A = (Pl * Kly * SumL() + Pt * Kty * SumT()) / firstLine.Wp1;
                    var B = (dWxx*Bxt)/ firstLine.Wp1;
                    var C = ((dWnt + dWnl)*Bnl)/ firstLine.Wp1;
                    var Cp = A+B+C;

                    ItemsSource.Add(new Losses()
                    {
                        Kz = (Min + Step * i).ToString(),
                        dWxx = dWxx.ToString("#.##"),
                        dWnt = dWnt.ToString("#.##"),
                        dWnl = dWnl.ToString("#.##"),
                        dW = dW.ToString("#.##"),

                        A = A.ToString("#.#####"),
                        B = B.ToString("#.#####"),
                        C = C.ToString("#.#####"),
                        Cp = Cp.ToString("#.#####"),
                        WpMain = firstLine.Wp1.ToString("#.##")
                    });

                    lineA.Points.Add(new DataPoint(Min + Step * i, A));
                    lineB.Points.Add(new DataPoint(Min + Step * i, B));
                    lineC.Points.Add(new DataPoint(Min + Step * i, C));
                    lineCp.Points.Add(new DataPoint(Min + Step * i, Cp));
                }
            }
            TheBestKz = ItemsSource.Where(c => c.dW == ItemsSource.Select(x => x.dW).Min()).Select(t => t.Kz).SingleOrDefault();

            OnPropertyChanged(nameof(ItemsSource));

            plotList.Add(new PlotModel());
            plotList.Add(new PlotModel());
            plotList.Add(new PlotModel());
            plotList.Add(new PlotModel());

            plotList[0].Series.Add(lineA);
            plotList[1].Series.Add(lineB);
            plotList[2].Series.Add(lineC);
            plotList[3].Series.Add(lineCp);

            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MajorGridlineColor = OxyColors.Gray };

            foreach (var i in plotList)
            {
                i.TextColor = OxyColors.Gray;
                i.PlotAreaBorderThickness = new OxyThickness(1, 0, 0, 1);
                i.PlotAreaBorderColor = OxyColors.White;
                //i.Axes.Add(valueAxis);
            }

            return plotList;
        }
        public void ChangedKz(double newKz)
        {
            var global = GlobalGrid.GetInstance();
            global.Tree.ChangedKz(newKz);
        }
        public double GetdWxx()
        {
            var global = GlobalGrid.GetInstance();

            var list = global.Tree.GetdWxx().Values.Sum();

            return list;
        }
        public double GetdWnt()
        {
            var global = GlobalGrid.GetInstance();

            var list = global.Tree.GetdWnt().Values.Sum();

            return list;
        }
        public double GetdWnl()
        {
            var global = GlobalGrid.GetInstance();

            var list = global.Tree.GetdWnl().Values.Sum();

            return list;
        }
        public double SumL()
        {
            var global = GlobalGrid.GetInstance();

            var value = global.Tree.GetSumL();

            return value;
        }
        public double SumT()
        {
            var global = GlobalGrid.GetInstance();

            var value = global.Tree.GetSumT();

            return value;
        }
        public ICommand Calculate => new DelegateCommand(o =>
        {
            var list = GetPlotModel();

            PlotA = list[0];
            PlotB = list[1];
            PlotC = list[2];
            PlotCp = list[3];
        });
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
