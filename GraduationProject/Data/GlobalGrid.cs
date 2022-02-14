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
            Transformers = new List<TransformerView>();
            Tree = new Tree<int>();
            U = new List<float>();
            BoxK = new List<int>();
        }
        public static GlobalGrid GetInstance()
        {
            if (instance == null) instance = new GlobalGrid();
            return instance;
        }
        public static int T = 8760;

        public int CountNode { get; set; }
        public List<int> BoxK { get; set; }
        public List<Button1> Lines;
        public List<TransformerView> Transformers;
        public Tree<int> Tree { get; set; }
        public List<float> U;
    }
}
