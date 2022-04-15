﻿using GraduationProject.Data;
using GraduationProject.View;
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
    class ModeAnalysisViewModel : INotifyPropertyChanged
    {
        private double branch_1;
        private double branch_2;
        private double branch_3;
        private double branch_4;
        private double branch_5;
        private double deltaUBranch_1;
        private double deltaUBranch_2;
        private double deltaUBranch_3;
        private double deltaUBranch_4;
        private double deltaUBranch_5;
        private double m;
        private double n;
        private double ust;
        private double dUnnyMax;
        private double dUnnyMin;
        private double dUcpMax;
        private double dUcpMin;
        private double unom;


        public double Branch_1
        {
            get => branch_1;
            set
            {
                branch_1 = value;
                OnPropertyChanged(nameof(Branch_1));
                DeltaUBranch_1 = Math.Round(((0.4 / 0.38) / ((Unom + Unom * value * 0.01) / Unom) - 1) * 100, 3);
            }
        }
        public double Branch_2
        {
            get => branch_2;
            set
            {
                branch_2 = value;
                OnPropertyChanged(nameof(Branch_2));
                DeltaUBranch_2 = Math.Round(((0.4 / 0.38) / ((Unom + Unom * value * 0.01) / Unom) - 1) * 100, 3);
            }
        }
        public double Branch_3
        {
            get => branch_3;
            set
            {
                branch_3 = value;
                OnPropertyChanged(nameof(Branch_3));
                DeltaUBranch_3 = Math.Round(((0.4 / 0.38) / ((Unom + Unom * value * 0.01) / Unom) - 1) * 100, 3);
            }
        }
        public double Branch_4
        {
            get => branch_4;
            set
            {
                branch_4 = value;
                OnPropertyChanged(nameof(Branch_4));
                DeltaUBranch_4 = Math.Round(((0.4 / 0.38) / ((Unom + Unom * value * 0.01) / Unom) - 1) * 100, 3);
            }
        }
        public double Branch_5
        {
            get => branch_5;
            set
            {
                branch_5 = value;
                OnPropertyChanged(nameof(Branch_5));
                DeltaUBranch_5 = Math.Round(((0.4 / 0.38) / ((Unom + Unom * value * 0.01) / Unom) - 1) * 100, 3);
            }
        }

        public double DeltaUBranch_1
        {
            get => deltaUBranch_1;
            set
            {
                deltaUBranch_1 = value;
                OnPropertyChanged(nameof(DeltaUBranch_1));
            }
        }
        public double DeltaUBranch_2
        {
            get => deltaUBranch_2;
            set
            {
                deltaUBranch_2 = value;
                OnPropertyChanged(nameof(DeltaUBranch_2));
            }
        }
        public double DeltaUBranch_3
        {
            get => deltaUBranch_3;
            set
            {
                deltaUBranch_3 = value;
                OnPropertyChanged(nameof(DeltaUBranch_3));
            }
        }
        public double DeltaUBranch_4
        {
            get => deltaUBranch_4;
            set
            {
                deltaUBranch_4 = value;
                OnPropertyChanged(nameof(DeltaUBranch_4));
            }
        }
        public double DeltaUBranch_5
        {
            get => deltaUBranch_5;
            set
            {
                deltaUBranch_5 = value;
                OnPropertyChanged(nameof(DeltaUBranch_5));
            }
        }
        public double M
        {
            get => m;
            set
            {
                m = value;
                OnPropertyChanged(nameof(M));
            }
        }
        public double N
        {
            get => n;
            set
            {
                n = value;
                OnPropertyChanged(nameof(N));
            }
        }
        public double Ust
        {
            get => ust;
            set
            {
                ust = value;
                OnPropertyChanged(nameof(Ust));
            }
        }
        public double DUnnyMax
        {
            get => dUnnyMax;
            set
            {
                dUnnyMax = value;
                OnPropertyChanged(nameof(DUnnyMax));
            }
        }
        public double DUnnyMin
        {
            get => dUnnyMin;
            set
            {
                dUnnyMin = value;
                OnPropertyChanged(nameof(DUnnyMin));
            }
        }
        public double DUcpMax
        {
            get => dUcpMax;
            set
            {
                dUcpMax = value;
                OnPropertyChanged(nameof(DUcpMax));
            }
        }
        public double DUcpMin 
        {
            get => dUcpMin;
            set
            {
                dUcpMin = value;
                OnPropertyChanged(nameof(DUcpMin));
            }
        }
        public double Unom
        {
            get => unom;
            set
            {
                unom = value;
                OnPropertyChanged(nameof(Unom));
            }
        }
        private List<LoadTransformer> itemsMax;
        public List<LoadTransformer> ItemsSourceMax
        {
            get => itemsMax;
            set
            {
                itemsMax = value;
                OnPropertyChanged(nameof(ItemsSourceMax));
            }
        }
        private List<LoadTransformer> itemsMin;
        public List<LoadTransformer> ItemsSourceMin
        {
            get => itemsMin;
            set
            {
                itemsMin = value;
                OnPropertyChanged(nameof(ItemsSourceMin));
            }
        }
        private List<InfoNodes> infoNodes;
        public List<InfoNodes> InfoNodes
        {
            get => infoNodes;
            set
            {
                infoNodes = value;
                OnPropertyChanged(nameof(InfoNodes));
            }
        }
        private List<InfoBranches> infoBranches;
        public List<InfoBranches> InfoBranches
        {
            get => infoBranches;
            set
            {
                infoBranches = value;
                OnPropertyChanged(nameof(InfoBranches));
            }
        }
        private List<InfoBranches> infoBranchesMin;
        public List<InfoBranches> InfoBranchesMin
        {
            get => infoBranchesMin;
            set
            {
                infoBranchesMin = value;
                OnPropertyChanged(nameof(InfoBranchesMin));
            }
        }
        private BranchesMainTable branchesMainTable;
        public BranchesMainTable BranchesMainTable
        {
            get => branchesMainTable;
            set
            {
                branchesMainTable = value;
                OnPropertyChanged(nameof(BranchesMainTable));
            }
        }
        private BranchesMainTable branchesMainTableMin;
        public BranchesMainTable BranchesMainTableMin
        {
            get => branchesMainTableMin;
            set
            {
                branchesMainTableMin = value;
                OnPropertyChanged(nameof(BranchesMainTableMin));
            }
        }
        private List<BranchesTable> branchesTable;
        public List<BranchesTable> BranchesTable
        {
            get => branchesTable;
            set
            {
                branchesTable = value;
                OnPropertyChanged(nameof(BranchesTable));
            }
        }
        private List<BranchesTable> branchesTableMin;
        public List<BranchesTable> BranchesTableMin
        {
            get => branchesTableMin;
            set
            {
                branchesTableMin = value;
                OnPropertyChanged(nameof(BranchesTableMin));
            }
        }
        public ModeAnalysisViewModel()
        {
            Unom = 10;
            Branch_1 = 5;
            Branch_2 = 2.5;
            Branch_3 = 0;
            Branch_4 = -2.5;
            Branch_5 = -5;

            N = 1.3;
            M = 0.15;
            Ust = 1.78;
            DUnnyMin = 0;
            DUnnyMax = 6;
            DUcpMax = 5;
            DUcpMin = 0;

            ItemsSourceMax = FullItemSource();
            ItemsSourceMin = FullItemSource(M);
            InfoNodes = FullInfoNodes();
        }
        private List<Node<int>> GetTransformers()
        {
            var global = GlobalGrid.GetInstance();
             return global.Tree.GetTransformers();
        }
        private List<Node<int>> GetElements()
        {
            var global = GlobalGrid.GetInstance();
            return global.Tree.GetElements();
        }
        private List<LoadTransformer> FullItemSource(double m = 1)
        {
            var list = new List<LoadTransformer>();
            var nodes = GetTransformers();
            foreach (var i in nodes)
            {
                var context = i.View.DataContext as TransformerViewModel;
                list.Add(new LoadTransformer()
                {
                    N = context.N,
                    K = context.K,
                    Cosfi = (context.Cosfi).ToString("0.####"),
                    Snom = (context.Snom*m).ToString("0.####"),
                    P = (context.P1*m).ToString("0.####"),
                    Q = (context.Q1*m).ToString("0.####")
                });
            }
            return list;
        }
        public List<InfoBranches> FullInfoBranches()
        {
            var list = new List<InfoBranches>();
            var nodes = GetElements();
            foreach (var i in nodes)
            {
                if (i.View.DataContext is TransformerViewModel contextTransformer)
                {
                    list.Add(new InfoBranches()
                    {
                        N = contextTransformer.N,
                        K = contextTransformer.K,
                        dU = ((((contextTransformer.P2/1000) * (contextTransformer.R / 25) + (contextTransformer.Q2/1000)* (contextTransformer.X / 25)) /0.38)*100).ToString("0.####"),//Попровить формулу
                        dUPercent = ((((contextTransformer.P2 / 1000) * (contextTransformer.R/25) + (contextTransformer.Q2 / 1000) * (contextTransformer.X/25)) / (0.38*0.38)) * 100).ToString("0.####"),//Попровить формулу
                        P = contextTransformer.P1.ToString("0.####"),
                        Q = contextTransformer.Q1.ToString("0.####"),
                        R = contextTransformer.R.ToString("0.####"),
                        X = contextTransformer.X.ToString("0.####")
                    });
                }
                if (i.View.DataContext is LineViewModel contextLine)
                {
                    list.Add(new InfoBranches()
                    {
                        N = contextLine.N,
                        K = contextLine.K,
                        dU = (contextLine.DU*1000).ToString("0.####"),
                        dUPercent = ((contextLine.DU/(GlobalGrid.U))*100).ToString("0.####"),
                        P = contextLine.P1.ToString("0.####"),
                        Q = contextLine.Q1.ToString("0.####"),
                        R = contextLine.R.ToString("0.####"),
                        X = contextLine.X.ToString("0.####")
                    });
                }
            }
            return list;
        }
        public List<InfoNodes> FullInfoNodes()
        {
            var list = new List<InfoNodes>();
            var nodes = GetElements();
            foreach (var i in nodes)
            {
                if (i.View.DataContext is TransformerViewModel contextTransformer)
                {
                    list.Add(new InfoNodes()
                    {
                        Number = contextTransformer.K,
                        dUSum = (GlobalGrid.U - contextTransformer.U2) * 1000,
                        dUSumPercent = ((GlobalGrid.U - contextTransformer.U2) / (GlobalGrid.U)) * 100,
                    });
                }
                if (i.View.DataContext is LineViewModel contextLine)
                {
                    list.Add(new InfoNodes()
                    {
                        Number = contextLine.K,
                        dUSum = (GlobalGrid.U - contextLine.U2) * 1000,
                        dUSumPercent = ((GlobalGrid.U - contextLine.U2) / (GlobalGrid.U)) * 100,
                    });
                }
            }
            return list;
        }
        public List<BranchesTable> FullBranchesTable()
        {
            var list = new List<BranchesTable>();
            var nodes = GetTransformers();

            foreach (var i in nodes)
            {
                var item = new BranchesTable();
                var context = i.View.DataContext as TransformerViewModel;
                var parent = i.Parent.View.DataContext as LineViewModel;
                item.N = context.N;
                item.K = context.K;
                item.DeltaUSumPercent = Math.Round((((GlobalGrid.U - parent.U2) / (GlobalGrid.U)) * 100),5);

                if (double.Parse(BranchesMainTable.Branch_5.Max) >= item.DeltaUSumPercent & double.Parse(BranchesMainTable.Branch_5.Min) <= item.DeltaUSumPercent)
                {
                    item.Branch_5 = "+";
                    item.SelectedBranch = Branch_5.ToString() + "%";
                }
                else item.Branch_5 = "-";

                if (double.Parse(BranchesMainTable.Branch_4.Max) >= item.DeltaUSumPercent & double.Parse(BranchesMainTable.Branch_4.Min) <= item.DeltaUSumPercent)
                {
                    item.Branch_4 = "+";
                    item.SelectedBranch = Branch_4.ToString() + "%";
                }
                else item.Branch_4 = "-";

                if (double.Parse(BranchesMainTable.Branch_3.Max) >= item.DeltaUSumPercent & double.Parse(BranchesMainTable.Branch_3.Min) <= item.DeltaUSumPercent)
                {
                    item.Branch_3 = "+";
                    item.SelectedBranch = Branch_3.ToString() + "%";
                }
                else item.Branch_3 = "-";

                if (double.Parse(BranchesMainTable.Branch_2.Max) >= item.DeltaUSumPercent & double.Parse(BranchesMainTable.Branch_2.Min) <= item.DeltaUSumPercent)
                {
                    item.Branch_2 = "+";
                    item.SelectedBranch = "+" + Branch_2.ToString() + "%";
                }
                else item.Branch_2 = "-";

                if (double.Parse(BranchesMainTable.Branch_1.Max) >= item.DeltaUSumPercent & double.Parse(BranchesMainTable.Branch_1.Min) <= item.DeltaUSumPercent)
                {
                    item.Branch_1 = "+";
                    item.SelectedBranch = "+" + Branch_1.ToString() + "%";
                }
                else item.Branch_1 = "-";

                list.Add(item);
            }

            return list;
        }
        public ICommand Return => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.FullGridChange.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.FullGridChange.Children[i].GetType().Name;
                if (childTypeName == nameof(ModeAnalysisView))
                {
                    window.FullGridChange.Children.RemoveAt(i);
                }
            }
        });
        public ICommand StartCommand => new DelegateCommand(o =>
        {
            var listTransformers = GlobalGrid.GetInstance().Tree.GetTransformers();
            foreach (var i in ItemsSourceMax)
            {
                var transformer = listTransformers.Where(x => x.Data == i.K).FirstOrDefault();
                var context = transformer.View.DataContext as TransformerViewModel;

                context.ChangeParameters(transformer, double.Parse(i.Snom), double.Parse(i.Cosfi));
            }

            InfoBranches = FullInfoBranches();
            BranchesMainTable = FindRangeForBranches(DUcpMax);
            BranchesTable = FullBranchesTable();


            foreach (var i in ItemsSourceMin)
            {
                var transformer = listTransformers.Where(x => x.Data == i.K).FirstOrDefault();
                var context = transformer.View.DataContext as TransformerViewModel;

                context.ChangeParameters(transformer, double.Parse(i.Snom), double.Parse(i.Cosfi));
            }

            InfoBranchesMin = FullInfoBranches();
            BranchesMainTableMin = FindRangeForBranches(DUcpMin, M); 
            //BranchesTableMin = FullBranchesTable();
        });
        public BranchesMainTable FindRangeForBranches(double dUcp, double m = 1)
        {
            var table = new BranchesMainTable();
            table.Branch_1 = GetRange(DeltaUBranch_1, dUcp, m).ToString();
            table.Branch_2 = GetRange(DeltaUBranch_2, dUcp, m).ToString();
            table.Branch_3 = GetRange(DeltaUBranch_3, dUcp, m).ToString();
            table.Branch_4 = GetRange(DeltaUBranch_4, dUcp, m).ToString();
            table.Branch_5 = GetRange(DeltaUBranch_5, dUcp, m).ToString();

            return table;
        }
        public Range GetRange(double dUBranch, double DUcp, double m)
        {
            var range = new Range();
            var rangeMax = new Range();
            var rangeMin = new Range();

            var dUMaxU1 = PermissibleVoltageDeviations_10000V(DUcp).Max + dUBranch - PermissibleVoltageDeviations_380V(m).Min;
            var dUMinU1 = PermissibleVoltageDeviations_10000V(DUcp).Max + dUBranch - PermissibleVoltageDeviations_380V(m).Max;

            var dUMaxU2 = PermissibleVoltageDeviations_10000V(DUcp).Min + dUBranch - PermissibleVoltageDeviations_380V(m).Min;
            var dUMinU2 = PermissibleVoltageDeviations_10000V(DUcp).Min + dUBranch - PermissibleVoltageDeviations_380V(m).Max;

            if (dUMaxU1 > dUMinU1)
            {
                rangeMax.Max = dUMaxU1;
                rangeMax.Min = dUMinU1;
            }
            else
            {
                rangeMax.Max = dUMinU1;
                rangeMax.Min = dUMaxU1;
            }

            if (dUMaxU2 > dUMinU2)
            {
                rangeMin.Max = dUMaxU2;
                rangeMin.Min = dUMinU2;
            }
            else
            {
                rangeMin.Max = dUMinU2;
                rangeMin.Min = dUMaxU2;
            }

            if (rangeMax.Min > rangeMin.Min) range.Min = rangeMax.Min;
            else range.Min = rangeMin.Min;

            if (rangeMax.Max < rangeMin.Max) range.Max = rangeMax.Max;
            else range.Max = rangeMin.Max;

            return range;
        }
        /// <summary>
        /// 4.	Определение зоны нечувствительности автоматического регулятора напряжения трансформатора в центре питания
        /// </summary>
        /// <returns></returns>
        public double ZoneOfInsensitivity()
        {
            return ((N * Ust) / 2); 
        }
        /// <summary>
        /// По ГОСТу отклонение напряжения у потребителя от -5% до +5%
        /// </summary>
        /// <returns></returns>
        public Range PermissibleVoltageDeviations_380V(double m)
        { 
            return new Range() { Max=DUnnyMin*m+5, Min=DUnnyMax*m-5 };
        }
        public Range PermissibleVoltageDeviations_10000V(double dUcp)
        {
            return new Range() { Max = dUcp + ZoneOfInsensitivity(), Min = dUcp - ZoneOfInsensitivity() };
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
