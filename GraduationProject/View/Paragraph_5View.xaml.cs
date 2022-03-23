using GraduationProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraduationProject.View
{
    /// <summary>
    /// Логика взаимодействия для Paragraph_5View.xaml
    /// </summary>
    public partial class Paragraph_5View : UserControl
    {
        public Paragraph_5View()
        {
            InitializeComponent();
            DataContext = new Paragraph_5ViewModel();
        }
    }
}
