using GraduationProject.Data;
using GraduationProject.Model;
using GraduationProject.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraduationProject.ViewModel
{
    public class MainWindowViewModel
    {
        private MainWindow window;
        public readonly MainWindowModel Model;

        public MainWindowViewModel()
        {
            Model = new MainWindowModel();
            window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        }
        public ICommand CreateNewSource => new DelegateCommand(o =>
        {
            window.StaticGrid.Children.Add(new AddSourceView());
        });
        public ICommand CreateNewLine => new DelegateCommand(o =>
        { 
            window.StaticGrid.Children.Add(new AddLineView());
        }); 
        public ICommand CreateNewTransformer => new DelegateCommand(o =>
        {
            window.StaticGrid.Children.Add(new AddTransformerView());
        });
        public ICommand CatalogLine => new DelegateCommand(o =>
        {
            window.StaticGrid.Children.Add(new CatalogLineView());
        });
        public ICommand CatalogTransformer => new DelegateCommand(o =>
        {
            window.StaticGrid.Children.Add(new CatalogTransformerView());
        });
        public ICommand Analysis => new DelegateCommand(o =>
        {
            window.FullGridChange.Children.Add(new ModeAnalysisView());
        });
        public ICommand Settings => new DelegateCommand(o =>
        {
            window.GridChangeFirst.Children.Add(new SettingsView());
        });
        public ICommand AddRight => new DelegateCommand(o =>
        {
            var width = window.GridChangeFirst.ActualWidth;
            var height = window.GridChangeFirst.ActualHeight;

            for (var i = 7; i < 27; i++)
            {
                window.GridChange.Children.Add(new Line() { X1 = (i * 10) + window.GridChangeFirst.ActualWidth - window.GridChangeFirst.ActualWidth%10, X2 = (i * 10) + window.GridChangeFirst.ActualWidth - window.GridChangeFirst.ActualWidth % 10, Y1 = 0, Y2 = height, Stroke = Brushes.Gray });
            }

            window.GridChangeFirst.Width = width + 200;

            for (var i = 0; i < window.GridChange.Children.Count; i++)
            {
                if (window.GridChange.Children[i] is Line line)
                {
                    if (line.X1 == 0)
                    {
                        line.X2 += 200;
                    }
                }
            }
        });
        public ICommand AddBottom => new DelegateCommand(o =>
        {
            var width = window.GridChangeFirst.ActualWidth;
            var height = window.GridChangeFirst.ActualHeight;

            for (var i = 53; i < 73; i++)
            {
                window.GridChange.Children.Add(new Line() { X1 = 0, X2 = width, Y1 = (i * 10) + window.GridChangeFirst.ActualHeight - window.GridChangeFirst.ActualHeight % 10, Y2 = (i * 10) + window.GridChangeFirst.ActualHeight - window.GridChangeFirst.ActualHeight % 10, Stroke = Brushes.Gray });
            }

            window.GridChangeFirst.Height = height + 200;

            for (var i = 0; i < window.GridChange.Children.Count; i++)
            {
                if (window.GridChange.Children[i] is Line line)
                {
                    if (line.Y1 == 0)
                    {
                        line.Y2 += 200;
                    }
                }
            }
        });
        public ICommand ClearGrid => new DelegateCommand(o =>
        {
            for (int i = window.GridChange.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.GridChange.Children[i].GetType().Name;
                if (childTypeName == nameof(LineView) | childTypeName == nameof(TransformerView) | childTypeName == nameof(SourceView))
                {
                    window.GridChange.Children.RemoveAt(i);
                }
            }
            GlobalGrid.GetInstance().Tree.DeleteTree();

            MessageBox.Show("Успешно выполнено!");
        });
        public ICommand SaveModel => new DelegateCommand(o =>
        {
            Model.SaveModel();
        });
        public ICommand OpenModel => new DelegateCommand(o =>
        {
            Model.OpenModel(window);
        });
    }
    
}
