using GraduationProject.ViewModel;
using System.Windows.Controls;

namespace GraduationProject.View
{
    /// <summary>
    /// Логика взаимодействия для AddSourceView.xaml
    /// </summary>
    public partial class AddSourceView : UserControl
    {
        public AddSourceView()
        {
            InitializeComponent();
            DataContext = new AddSourceViewModel();
        }
    }
}
