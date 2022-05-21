using GraduationProject.ViewModel;
using System.Windows.Controls;

namespace GraduationProject.View
{
    /// <summary>
    /// Логика взаимодействия для ChartVDTView.xaml
    /// </summary>
    public partial class ChartVDTView : UserControl
    {
        public ChartVDTView()
        {
            InitializeComponent();
            DataContext = new ChartVDTViewModel();
        }
    }
}
