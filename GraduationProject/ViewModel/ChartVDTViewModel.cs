using GraduationProject.Data;
using GraduationProject.View;
using OxyPlot;
using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GraduationProject.ViewModel
{
    public class ChartVDTViewModel : INotifyPropertyChanged
    {
        private PlotModel plotBefore;
        public PlotModel PlotBefore
        {
            get => plotBefore;
            set
            {
                plotBefore = value;
                OnPropertyChanged(nameof(PlotBefore));
            }
        }
        private PlotModel plotAfter;
        public PlotModel PlotAfter
        {
            get => plotAfter;
            set
            {
                plotAfter = value;
                OnPropertyChanged(nameof(PlotAfter));
            }
        }
        private PlotModel plotNow;
        public PlotModel PlotNow
        {
            get => plotNow;
            set
            {
                plotNow = value;
                OnPropertyChanged(nameof(PlotNow));
            }
        }
        public ChartVDTViewModel()
        {
            var chartBeforeVDT = new ChartBeforeVDT();
            if (ChartBeforeVDT.plotBefore != null)
            {
                PlotBefore = ChartBeforeVDT.plotBefore;
                PlotAfter = GetPlotModelSorted();
                PlotNow = GetPlotModel();
            }
        }
        private List<Node<int>> GetElements()
        {
            var global = GlobalGrid.GetInstance();
            return global.Tree.GetElements();
        }
        public PlotModel GetPlotModel()
        {
            var plot = new PlotModel();

            var line = new OxyPlot.Series.LineSeries()
            {
                Title = "Режим наибольших нагрузок",
                Color = OxyColor.FromRgb(22, 186, 124),
                StrokeThickness = 3,
                MarkerSize = 5,
                MarkerType = MarkerType.Circle,
            };
            var lineWithTransformers = new OxyPlot.Series.LineSeries()
            {
                Title = "Режим наибольших нагрузок",
                Color = OxyColor.FromRgb(22, 22, 124),
                StrokeThickness = 3,
                MarkerSize = 5,
                MarkerType = MarkerType.Circle,
            };
            var lineRed = new OxyPlot.Series.LineSeries()
            {
                Title = "Режим наибольших нагрузок",
                Color = OxyColor.FromRgb(255, 0, 0),
                StrokeThickness = 2,
                MarkerSize = 2,
                MarkerType = MarkerType.None,
            };

            var dictionarySecond = GetDataChartSecond(5);
            if (dictionarySecond.Count > 0)
            {
                var valuesList = dictionarySecond.Values.ToList();
                var keysList = dictionarySecond.Keys.ToList();

                lineWithTransformers.Points.Add(new DataPoint(0, valuesList[0][0]));
                for (int i = 1; i < dictionarySecond.Count(); i++)
                {
                    lineWithTransformers.Points.Add(new DataPoint(i, valuesList[i][0]));
                    lineWithTransformers.Points.Add(new DataPoint(i, valuesList[i][1]));
                    lineWithTransformers.Points.Add(new DataPoint(i, valuesList[i][2]));
                }
                plot.Series.Add(lineWithTransformers);
                plot.TextColor = OxyColors.Black;
                plot.PlotAreaBorderThickness = new OxyThickness(2, 0, 0, 2);
                plot.PlotAreaBorderColor = OxyColors.DimGray;

                ////var min = dictionarySecond.Values.ToList().Min();
                ////var max = dictionarySecond.Values.ToList().Max();

                //var dateAxisH = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MinorGridlineColor = OxyColors.LightGray, MajorGridlineColor = OxyColors.Transparent, Position = AxisPosition.Left };
                //dateAxisH.Layer = AxisLayer.BelowSeries;
                //plot.Axes.Add(dateAxisH);

                //var dateAxisV = new CategoryAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MinorGridlineColor = OxyColors.LightGray, MajorGridlineColor = OxyColors.Transparent, Position = AxisPosition.Bottom, Angle = -90 };
                //foreach (var i in keysList) dateAxisV.Labels.Add(i);
                //dateAxisV.Layer = AxisLayer.AboveSeries;
                //plot.Axes.Add(dateAxisV);
            }

            var dictionary = GetDataChart(5);

            if (dictionary.Count > 0)
            {
                var valuesList = dictionary.Values.ToList();
                var keysList = dictionary.Keys.ToList();

                for (int i = 0; i < dictionary.Count(); i++)
                {
                    line.Points.Add(new DataPoint(i, valuesList[i]));
                }
                plot.Series.Add(line);
                plot.TextColor = OxyColors.Black;
                plot.PlotAreaBorderThickness = new OxyThickness(2, 0, 0, 2);
                plot.PlotAreaBorderColor = OxyColors.DimGray;

                var min = dictionary.Values.ToList().Min();
                var max = dictionary.Values.ToList().Max();

                var dateAxisH = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MinorGridlineColor = OxyColors.LightGray, MajorGridlineColor = OxyColors.Red, MajorStep = 5, Maximum = max, Minimum = min, Position = AxisPosition.Left };
                dateAxisH.Layer = AxisLayer.BelowSeries;
                plot.Axes.Add(dateAxisH);

                var dateAxisV = new CategoryAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MinorGridlineColor = OxyColors.LightGray, MajorGridlineColor = OxyColors.Transparent, Position = AxisPosition.Bottom, Angle = -90 };
                foreach (var i in keysList) dateAxisV.Labels.Add(i);
                dateAxisV.Layer = AxisLayer.AboveSeries;
                plot.Axes.Add(dateAxisV);
            }

            return plot;
        }
        public Dictionary<string, double> GetDataChart(double percent)
        {
            var list = new Dictionary<string, double>();
            var nodes = GetElements();
            list.Add("ЦП", percent);

            foreach (var i in nodes)
            {
                if (i.View.DataContext is TransformerViewModel contextTransformer)
                {
                    list.Add(contextTransformer.K.ToString(), percent - ((GlobalGrid.U - contextTransformer.U1) / (GlobalGrid.U)) * 100);
                }
            }
            list = list.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return list;
        }
        public Dictionary<string, List<double>> GetDataChartSecond(double percent)
        {
            var list = new Dictionary<string, List<double>>();
            var nodes = GetElements();
            list.Add("ЦП", new List<double>(1));
            list.Values.First().Add(percent);

            foreach (var i in nodes)
            {
                if (i.View.DataContext is TransformerViewModel contextTransformer)
                {
                    list.Add(contextTransformer.K.ToString(), new List<double>(3) {
                        percent - ((GlobalGrid.U - contextTransformer.U1) / GlobalGrid.U) * 100,
                        percent - ((GlobalGrid.U - (contextTransformer.U1-contextTransformer.DeltaU)) / GlobalGrid.U) * 100,
                        percent - ((GlobalGrid.U - contextTransformer.U1) / GlobalGrid.U) * 100
                    });
                }
            }
            list = list.OrderByDescending(x => x.Value.First()).ToDictionary(x => x.Key, x => x.Value);
            return list;
        }

        public PlotModel GetPlotModelSorted()
        {
            var plot = new PlotModel();

            var line = new OxyPlot.Series.LineSeries()
            {
                Title = "Режим наибольших нагрузок",
                Color = OxyColor.FromRgb(22, 186, 124),
                StrokeThickness = 3,
                MarkerSize = 5,
                MarkerType = MarkerType.Circle,
            };
            var lineWithTransformers = new OxyPlot.Series.LineSeries()
            {
                Title = "Режим наибольших нагрузок",
                Color = OxyColor.FromRgb(22, 22, 124),
                StrokeThickness = 3,
                MarkerSize = 5,
                MarkerType = MarkerType.Circle,
            };
            var lineRed = new OxyPlot.Series.LineSeries()
            {
                Title = "Режим наибольших нагрузок",
                Color = OxyColor.FromRgb(255, 0, 0),
                StrokeThickness = 2,
                MarkerSize = 2,
                MarkerType = MarkerType.None,
            };

            var listFirstBefore = ChartBeforeVDT.dictionaryFirstBefore;
            var listSecondBefore = ChartBeforeVDT.dictionarySecondBefore;



            var dictionarySecondNow = GetDataChartSecond(5);
            if (dictionarySecondNow.Count > 0)
            {
                var valuesListNow = dictionarySecondNow.Values.ToList();
                var keysListNow = dictionarySecondNow.Keys.ToList();

                var valuesList = listSecondBefore.Values.ToList();
                var keysList = listSecondBefore.Keys.ToList();

                lineWithTransformers.Points.Add(new DataPoint(0, valuesList[0][0]));

                for (int i = 1; i < dictionarySecondNow.Count(); i++)
                {
                    var item = dictionarySecondNow.Where(x => x.Key == keysList[i]).ToDictionary(k => k.Key, v => v.Value).Single();

                    lineWithTransformers.Points.Add(new DataPoint(i, item.Value[0]));
                    lineWithTransformers.Points.Add(new DataPoint(i, item.Value[1]));
                    lineWithTransformers.Points.Add(new DataPoint(i, item.Value[2]));
                }
                plot.Series.Add(lineWithTransformers);
                plot.TextColor = OxyColors.Black;
                plot.PlotAreaBorderThickness = new OxyThickness(2, 0, 0, 2);
                plot.PlotAreaBorderColor = OxyColors.DimGray;
            }



            var dictionaryFirstNow = GetDataChart(5);

            if (dictionaryFirstNow.Count > 0)
            {
                var valuesListNow = dictionaryFirstNow.Values.ToList();
                var keysListNow = dictionaryFirstNow.Keys.ToList();

                var valuesList = listFirstBefore.Values.ToList();
                var keysList = listFirstBefore.Keys.ToList();

                for (int i = 0; i < listFirstBefore.Count(); i++)
                {
                    var item = dictionaryFirstNow.Where(x => x.Key == keysList[i]).ToDictionary(k => k.Key, v=>v.Value).Single();

                    line.Points.Add(new DataPoint(i, item.Value));
                }
                plot.Series.Add(line);
                plot.TextColor = OxyColors.Black;
                plot.PlotAreaBorderThickness = new OxyThickness(2, 0, 0, 2);
                plot.PlotAreaBorderColor = OxyColors.DimGray;

                var min = dictionaryFirstNow.Values.ToList().Min();
                var max = dictionaryFirstNow.Values.ToList().Max();

                var dateAxisH = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MinorGridlineColor = OxyColors.LightGray, MajorGridlineColor = OxyColors.Red, MajorStep = 5, Maximum = max, Minimum = min, Position = AxisPosition.Left };
                dateAxisH.Layer = AxisLayer.BelowSeries;
                plot.Axes.Add(dateAxisH);

                var dateAxisV = new CategoryAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MinorGridlineColor = OxyColors.LightGray, MajorGridlineColor = OxyColors.Transparent, Position = AxisPosition.Bottom, Angle = -90 };
                foreach (var i in keysList) dateAxisV.Labels.Add(i);
                dateAxisV.Layer = AxisLayer.AboveSeries;
                plot.Axes.Add(dateAxisV);
            }

            return plot;
        }

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
                if (childTypeName == nameof(ChartVDTView))
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
