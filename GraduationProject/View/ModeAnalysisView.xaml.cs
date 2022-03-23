using GraduationProject.ViewModel;
using System.Windows.Controls;

namespace GraduationProject.View
{
    /// <summary>
    /// Логика взаимодействия для ModeAnalysisView.xaml
    /// </summary>
    public partial class ModeAnalysisView : UserControl
    {
        public ModeAnalysisView()
        {
            InitializeComponent();
            DataContext = new ModeAnalysisViewModel();
        }
    }
}
