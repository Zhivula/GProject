using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data
{
    public interface IElement
    {
        int N { get; set; }
        int K { get; set; }
        string Brand { get; set; }
        bool Flag { get; set; }
        
    }
}
