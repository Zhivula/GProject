using GraduationProject.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GraduationProject.View
{
    /// <summary>
    /// Логика взаимодействия для SourceView.xaml
    /// </summary>
    public partial class SourceView : UserControl
    {
        public MainWindow window;

        public SourceView(string name, double voltage)
        {
            window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            InitializeComponent();
            DataContext = new SourceViewModel(name, voltage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.curr = this;
        }
    }
}
