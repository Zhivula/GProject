using GraduationProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data
{
    [Serializable]
    public class TreeSerializable
    {
        public SourceModel SourceModel { get; set; }

        public double XSource { get; set; }
        public double YSource { get; set; }

        public NodeSerializable Root { get; set; }

        public TreeSerializable()
        {

        }
        public void Add(int start, int finish, object context, double angle, double x, double y)
        {
            if (Root == null)
            {
                var node = new NodeSerializable(finish, context, null, angle, x, y);
                Root = node;
                return;
            }
            if (Root.Data.CompareTo(start) == 0)
            {
                var node = new NodeSerializable(finish, context, Root, angle, x, y);
                Root.List.Add(node);
            }
            if (Root.Data.CompareTo(start) == -1)
            {
                Root.Add(start, finish, context, angle, x, y);
            }
        }
        public bool Contains(NodeSerializable node)
        {
            var flag = false;
            if (Root != null)
            {
                flag = Root.Contains(node);
            }
            return flag;
        }
    }
}
