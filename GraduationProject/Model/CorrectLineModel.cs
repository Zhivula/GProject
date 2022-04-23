using GraduationProject.Data;
using GraduationProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Model
{
    public class CorrectLineModel
    {
        public CorrectLineModel()
        {

        }
        public List<LoadLine> FullItemSource()
        {
            var list = new List<LoadLine>();
            var nodes = GetElements();
            foreach (var i in nodes)
            {
                if (i.View.DataContext is LineViewModel context)
                {
                    list.Add(new LoadLine()
                    {
                        N = context.N,
                        K = context.K,
                        Length = (context.Length).ToString("0.####"),
                        R0 = (context.R0).ToString("0.####"),
                        X0 = (context.X0).ToString("0.####"),
                        Idop = (context.Idop).ToString("0.####")
                    });
                }
            }
            return list;
        }
        private List<Node<int>> GetElements()
        {
            var global = GlobalGrid.GetInstance();
            return global.Tree.GetElements();
        }
    }
}
