using GraduationProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GraduationProject.ViewModel
{
    class MainWindowViewModel
    {
        private MainWindow window;

        public MainWindowViewModel()
        {
            window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        }
        public ICommand CreateNewSource => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.GridChangeFirst.Children.Add(new AddSourceView());
        });
        public ICommand CreateNewLine => new DelegateCommand(o =>
        { 
            //window.GridChange.Children.Clear();
            window.GridChangeFirst.Children.Add(new AddLineView());
        }); 
        public ICommand CreateNewTransformer => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.GridChangeFirst.Children.Add(new AddTransformerView());
        });
        public ICommand CatalogLine => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.GridChangeFirst.Children.Add(new CatalogLineView());
        });
        public ICommand CatalogTransformer => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.GridChangeFirst.Children.Add(new CatalogTransformerView());
        });
        public ICommand Analysis => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.GridChangeFirst.Children.Add(new AnalysisView());
        });
        public ICommand Settings => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.GridChangeFirst.Children.Add(new SettingsView());
        });
    }
}
