using GraduationProject.ViewModel;
using System.Windows.Controls;


namespace GraduationProject.View
{
    /// <summary>
    /// Логика взаимодействия для Paragraph_3View.xaml
    /// </summary>
    public partial class Paragraph_3View : UserControl
    {
        public Paragraph_3View()
        {
            InitializeComponent();
            DataContext = new Paragraph_3ViewModel();
        }
    }
}
