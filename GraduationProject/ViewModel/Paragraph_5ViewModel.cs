using GraduationProject.Data;
using GraduationProject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GraduationProject.ViewModel
{
    class Paragraph_5ViewModel : INotifyPropertyChanged
    {
        private float fuelConsumption;
        private List<Losses_Paragraph_5> items;
        private List<Losses> itemsSource;

        public float FuelConsumption
        {
            get => fuelConsumption;
            set
            {
                fuelConsumption = value;
                OnPropertyChanged(nameof(FuelConsumption));
            }
        }
        public List<Losses_Paragraph_5> Items
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        public List<Losses> ItemsSource
        {
            get => itemsSource;
            set
            {
                itemsSource = value;
                OnPropertyChanged(nameof(ItemsSource));
            }
        }
        public Paragraph_5ViewModel()
        {
            ItemsSource = new List<Losses>();
            Items = new List<Losses_Paragraph_5>();
        }
        public ICommand Calculate => new DelegateCommand(o =>
        {
            var model = new Paragraph_3ViewModel();
            var Count = 100;
            var Min = 0.05;
            var Step = 0.05;
            var HighKz = 1;
            var LowKz = HighKz / 5;
            var list = new List<Losses>();
            for (int i = 0; i < Count; i++)
            {                
                model.ChangedKz(Min + Step * i);
                var root = GlobalGrid.GetInstance().Tree.Root;
                var dWxx = model.GetdWxx();
                var dWnt = model.GetdWnt();
                var dWnl = model.GetdWnl();
                var dW = dWxx + dWnt + dWnl;

                if (root != null)
                {
                    var firstLine = root.View.DataContext as ButtonViewModel;

                    list.Add(new Losses()
                    {
                        Kz = (Min + Step * i).ToString(),
                        dWxx = dWxx.ToString("#.##"),
                        dWxxPercent = ((dWxx / firstLine.Wp1) * 100).ToString("#.##"),
                        dWnt = dWnt.ToString("#.##"),
                        dWntPercent = ((dWnt / firstLine.Wp1) * 100).ToString("#.##"),
                        dWnl = dWnl.ToString("#.##"),
                        dWnlPercent = ((dWnl / firstLine.Wp1) * 100).ToString("#.##"),
                        dW = dW.ToString("#.##"),
                        dWPercent = ((dW / firstLine.Wp1) * 100).ToString("#.##"),
                        WpMain = firstLine.Wp1.ToString("#.##")
                    });
                }
            }
            var OptimalKz = model.GetTheBestKz(list);

            ItemsSource.AddRange(list.Where(x=>x.Kz == OptimalKz).ToList());

            var losses = GetLosses(HighKz);
            ItemsSource.Add(losses);

            losses = GetLosses(LowKz);
            ItemsSource.Add(losses);

            foreach (var i in ItemsSource)
            {
                Items.Add(new Losses_Paragraph_5() {
                    Kz = i.Kz, 
                    dWxxPercent = i.dWxxPercent,
                    dWntPercent = i.dWntPercent,
                    dWnlPercent = i.dWnlPercent,
                    W = i.WpMain, 
                    dWPercent = i.dWPercent
                });
            }
            Items[0].Info = "Оптимальный режим";
            Items[1].Info = "Максимальный режим";
            Items[2].Info = "Минимальный режим";

            OnPropertyChanged(nameof(ItemsSource));
            OnPropertyChanged(nameof(Items));
        });
        public Losses GetLosses(double Kz)
        {
            var losses = new Losses();
            var model = new Paragraph_3ViewModel();
            model.ChangedKz(Kz);
            var root = GlobalGrid.GetInstance().Tree.Root;
            var dWxx = model.GetdWxx();
            var dWnt = model.GetdWnt();
            var dWnl = model.GetdWnl();
            var dW = dWxx + dWnt + dWnl;

            if (root != null)
            {
                var firstLine = root.View.DataContext as ButtonViewModel;

                losses = new Losses()
                {
                    Kz = Kz.ToString(),
                    dWxx = dWxx.ToString("#.##"),
                    dWxxPercent = ((dWxx / firstLine.Wp1) * 100).ToString("#.##"),
                    dWnt = dWnt.ToString("#.##"),
                    dWntPercent = ((dWnt / firstLine.Wp1) * 100).ToString("#.##"),
                    dWnl = dWnl.ToString("#.##"),
                    dWnlPercent = ((dWnl / firstLine.Wp1) * 100).ToString("#.##"),
                    dW = dW.ToString("#.##"),
                    dWPercent = ((dW / firstLine.Wp1) * 100).ToString("#.##"),
                    WpMain = firstLine.Wp1.ToString("#.##")
                };
            }
            return losses;
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