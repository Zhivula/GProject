using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data
{
    public class ChartBeforeVDT
    {
        private static ChartBeforeVDT instance;
        public static PlotModel plotBefore;
        public static Dictionary<string, List<double>> dictionarySecondBefore;
        public static Dictionary<string, double> dictionaryFirstBefore;
        public ChartBeforeVDT()
        {

        }
        public static ChartBeforeVDT GetInstance()
        {
            if (instance == null) instance = new ChartBeforeVDT();
            return instance;
        }
    }
}
