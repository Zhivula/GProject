using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Model
{
    [Serializable]
    public class AddSourceModel
    {
        public double Voltage { get; set; }
        public string Name { get; set; }
    }
}
