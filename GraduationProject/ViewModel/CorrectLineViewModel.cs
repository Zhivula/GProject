using GraduationProject.Data;
using GraduationProject.Model;
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
    public class CorrectLineViewModel : INotifyPropertyChanged
    {
        private List<LoadLine> itemsSourceLine;
        public List<LoadLine> ItemsSourceLine
        {
            get => itemsSourceLine;
            set
            {
                itemsSourceLine = value;
                OnPropertyChanged(nameof(ItemsSourceLine));
            }
        }

        public CorrectLineViewModel()
        {
            var model = new CorrectLineModel();
            ItemsSourceLine = model.FullItemSource();
        }
        public ICommand StartCommand => new DelegateCommand(o =>
        {

            var list = GlobalGrid.GetInstance().Tree.GetLines();
            foreach (var i in ItemsSourceLine)
            {
                var transformer = list.Where(x => x.Data == i.K).FirstOrDefault();
                var context = transformer.View.DataContext as LineViewModel;

                //context.ChangeParameters(transformer, double.Parse(i.Snom), double.Parse(i.Cosfi));
            }
        });
        public ICommand Close => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.StaticGrid.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.StaticGrid.Children[i].GetType().Name;
                if (childTypeName == nameof(CorrectLineView))
                {
                    window.StaticGrid.Children.RemoveAt(i);
                }
            }
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
