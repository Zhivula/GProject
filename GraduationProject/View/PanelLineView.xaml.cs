using GraduationProject.ViewModel;
using System.Windows.Controls;

namespace GraduationProject.View
{
    /// <summary>
    /// Логика взаимодействия для PanelLineView.xaml
    /// </summary>
    public partial class PanelLineView : UserControl
    {
        public PanelLineView(Button1 line)
        {
            InitializeComponent();
            DataContext = new PanelLineViewModel(line);
        }
    }
}
