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
        public ICommand CreateNewLine => new DelegateCommand(o =>
        { 
            //window.GridChange.Children.Clear();
            window.GridChange.Children.Add(new AddLineView());
        }); 
        public ICommand CreateNewTransformer => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.GridChange.Children.Add(new AddTransformerView());
        });
    }
}
