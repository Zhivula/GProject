using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Model
{
    [Serializable]
    public class LineModel
    {
        public int N { get; set; }
        public int K { get; set; }
        public string Brand { get; set; }
        public double R0 { get; set; }
        public double X0 { get; set; }
        public double L { get; set; }

        public LineModel()
        {
                
        }
    }
}
