using GraduationProject.ViewModel;
using System.Windows.Controls;

namespace GraduationProject.View
{
    /// <summary>
    /// Логика взаимодействия для Paragraph_4View.xaml
    /// </summary>
    public partial class Paragraph_4View : UserControl
    {
        public Paragraph_4View()
        {
            InitializeComponent();
            DataContext = new Paragraph_4ViewModel();
        }
    }
}
