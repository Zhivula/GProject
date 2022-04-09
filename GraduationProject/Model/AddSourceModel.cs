using GraduationProject.Data;
using GraduationProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Model
{
    public class AddSourceModel
    {
        public AddSourceModel()
        {

        }
        public void AddSource(MainWindow window, string name, double voltage)
        {
            var global = GlobalGrid.GetInstance();
            var source = new SourceView(name, voltage) { Height = 90, Width = 150 }; 
            global.Source = source;
            window.GridChange.Children.Add(source);
            window.curr = source;
        }
    }
}
