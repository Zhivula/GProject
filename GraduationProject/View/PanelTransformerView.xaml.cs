﻿using GraduationProject.ViewModel;
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
    /// Логика взаимодействия для PanelTransformerView.xaml
    /// </summary>
    public partial class PanelTransformerView : UserControl
    {
        public PanelTransformerView(TransformerView transformer)
        {
            InitializeComponent();
            DataContext = new PanelTransformerViewModel(transformer);
        }
    }
}
