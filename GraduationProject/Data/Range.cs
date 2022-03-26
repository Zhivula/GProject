using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data
{
    public class Range
    {
        public double Max { get; set; }
        public double Min { get; set; }

        public new RangeFormat ToString()
        {
            return new RangeFormat()
            {
                Max = Max.ToString("#.##"),
                Min = Min.ToString("#.##")
            };
        }
    }
}
