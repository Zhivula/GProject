using GraduationProject.ViewModel;
using System.Windows.Controls;

namespace GraduationProject.View
{
    /// <summary>
    /// Логика взаимодействия для NewLineDataView.xaml
    /// </summary>
    public partial class AddLineView : UserControl
    {
        public AddLineView() 
        {
            InitializeComponent();
            DataContext = new AddLineViewModel();
        }
    }
}
