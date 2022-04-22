using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraduationProject.Data
{
    public class BranchesTableMaxMin
    {
        public BranchesTable BranchesTableMax { get; set; }
        public BranchesTable BranchesTableMin { get; set; }
        public string BranchesTableSelected { get; set; }
        public SolidColorBrush ColorSelected { get; set; }
    }
}
