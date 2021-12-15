using GraduationProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data
{
    public class GlobalGrid
    {
        private static GlobalGrid instance;
        private GlobalGrid()
        {
            CountNode = 0;
            Lines = new List<Button1>();
            
            Tree = new Tree<int>();
            //Lines = new List<LineGrid>();
            //Chain = new List<LineGrid>();
            U = new List<float>();
        }
        public static GlobalGrid GetInstance()
        {
            if (instance == null) instance = new GlobalGrid();
            return instance;
        }

        public int CountNode { get; set; }
        public List<Button1> Lines;
        public Tree<int> Tree { get; set; }
        //public List<LineGrid> Lines;
        //public List<LineGrid> Chain;
        public List<float> U;
    }
}
