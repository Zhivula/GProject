using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraduationProject.Model
{
    [Serializable]
    public class TransformerModel
    {
        public int N { get; set; }
        public int K { get; set; }
        public string Brand { get; set; }
        public double Pxx { get; set; }
        public double Qxx { get; set; }
        public double Pkz { get; set; }
        public double Ukz { get; set; }
        public double Snom { get; set; }
        public double Ixx { get; set; }
        public double R { get; set; }
        public double X { get; set; }
        public Point Point { get; set; }

        public TransformerModel()
        {
            
        }
    }
}
