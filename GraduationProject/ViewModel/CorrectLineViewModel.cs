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

            var listElements = GlobalGrid.GetInstance().Tree.GetElements();
            var list = listElements.Where(x => x.View.DataContext is LineViewModel).ToList();
            foreach (var i in ItemsSourceLine)
            {
                var line = list.Where(x => x.Data == i.K).FirstOrDefault();
                if (line.View.DataContext is LineViewModel context)
                {
                    if (context.Length != i.Length)
                    {
                        context.Length = i.Length;
                        context.LengthFormat = i.Length + " км";
                    }
                    if (context.Idop != i.Idop)
                    {
                        context.Idop = i.Idop;
                    }
                    if (context.X0 != i.X0)
                    {
                        context.X0 = i.X0;
                    }
                    if (context.R0 != i.R0)
                    {
                        context.R0 = i.R0;
                    }
                }
            }
            var listTransformers = listElements.Where(x => x.View.DataContext is TransformerViewModel).ToList();
            foreach (var i in listTransformers)
            {
                var c = i.View.DataContext as TransformerViewModel;
                c.ChangeParameters(i, c.Snom, c.Cosfi);
            }
            CloseControl();
        });
        public ICommand Close => new DelegateCommand(o =>
        {
            CloseControl();
        });
        private void CloseControl()
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
