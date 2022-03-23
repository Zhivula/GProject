using GraduationProject.DataBase;
using GraduationProject.Model;
using GraduationProject.View;
using GraduationProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GraduationProject.Data
{
    [Serializable]
    public class NodeSerializable
    {
        public int Data { get; set; }

        public double Angel { get; set; }

        public List<NodeSerializable> List { get; set; }

        public object DataContext { get; set; }

        public NodeSerializable Parent { get; set; }

        public NodeSerializable(int data, object dataContext, NodeSerializable parent, double angle)
        {
            Data = data;
            DataContext = dataContext;
            Parent = parent;
            List = new List<NodeSerializable>();
            Angel = angle;
        }
        public void Add(int start, int finish, object view, double angle)
        {
            var node = new NodeSerializable(finish, view, this, angle);
            if (Data.CompareTo(start) == 0)
            {
                List.Add(node);
            }
            else
            {
                foreach (var i in List)
                {
                    i.Add(start, finish, view, angle);
                }
            }
        }
        public void DeserializableNode()
        {
            
            if (DataContext is LineModel contextLine)
            {
                var line = new Button1(contextLine.Brand, contextLine.L, contextLine.R0, contextLine.X0) { Height = 50, Width = 100 };
                ((RotateTransform)line.RenderTransform).Angle = Angel;
                var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                window.GridChange.Children.Add(line);
            }
            else if (DataContext is TransformerModel contextTransformer)
            {
                var global = GlobalGrid.GetInstance();
                var item = new Transformer();
                using (var context = new MyDbContext())
                {
                    item = context.Transformers.Where(x => x.Brand == contextTransformer.Brand).Single();
                }
                var transformer = new TransformerView(item) { Height = 50, Width = 100 };
                ((RotateTransform)transformer.RenderTransform).Angle = Angel;
                global.Transformers.Add(transformer);

                var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                window.GridChange.Children.Add(transformer);
            }
            else
            {
                foreach (var i in List)
                {
                    i.DeserializableNode();
                }
            }
        }
        public bool Contains(NodeSerializable node)
        {
            if (Data.CompareTo(node.Data) == 0)
            {
                return true;
            }
            else
            {
                foreach (var i in List)
                {
                    i.Contains(node);
                }
            }
            return false;
        }
        public int CompareTo(object obj)
        {
            if (obj is NodeSerializable item)
            {
                return Data.CompareTo(item);
            }
            else
            {
                MessageBox.Show("Не совпадение типов");
                return -1;
            }
        }
        public int CompareTo(int other)
        {
            return Data.CompareTo(other);
        }
    }
}
