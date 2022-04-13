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
            Lines = new List<LineView>();
            Transformers = new List<TransformerView>();
            Tree = new Tree<int>();
            BoxK = new List<int>();
        }
        public static GlobalGrid GetInstance()
        {
            if (instance == null) instance = new GlobalGrid();
            return instance;
        }
        public static int T = 8760;
        public static double U = 10.5d;

        public int CountNode { get; set; }
        public List<int> BoxK { get; set; }
        public List<LineView> Lines;
        public List<TransformerView> Transformers;
        public Tree<int> Tree { get; set; }
        public SourceView Source { get; set; }
    }
}
