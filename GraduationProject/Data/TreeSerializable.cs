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
        public NodeSerializable Root { get; set; }
        public TreeSerializable()
        {

        }
        public void Add(int start, int finish, object context, double angle)
        {
            if (Root == null)
            {
                var node = new NodeSerializable(finish, context, null, angle);
                Root = node;
                return;
            }
            if (Root.Data.CompareTo(start) == 0)
            {
                var node = new NodeSerializable(finish, context, Root, angle);
                Root.List.Add(node);
            }
            if (Root.Data.CompareTo(start) == -1)
            {
                Root.Add(start, finish, context, angle);
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
