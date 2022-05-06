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
using System.Windows.Controls;
using System.Windows.Media;

namespace GraduationProject.Data
{
    [Serializable]
    public class NodeSerializable
    {
        public int Data { get; set; }

        public double Angle { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public List<NodeSerializable> List { get; set; }

        public object DataContext { get; set; }

        public NodeSerializable Parent { get; set; }

        public NodeSerializable(int data, object dataContext, NodeSerializable parent, double angle, double x, double y)
        {
            Data = data;
            DataContext = dataContext;
            Parent = parent;
            List = new List<NodeSerializable>();
            Angle = angle;
            X = x;
            Y = y;
        }
        public void Add(int start, int finish, object view, double angle, double x, double y)
        {
            var node = new NodeSerializable(finish, view, this, angle, x, y);
            if (Data.CompareTo(start) == 0)
            {
                List.Add(node);
            }
            else
            {
                foreach (var i in List)
                {
                    i.Add(start, finish, view, angle, x, y);
                }
            }
        }
        public void DeserializableNode()
        {
            
            if (DataContext is LineModel contextLine)
            {
                var line = new LineView(contextLine.Brand, contextLine.L, contextLine.R0, contextLine.X0, contextLine.Idop) { Height = 50, Width = 100 };
                ((RotateTransform)line.RenderTransform).Angle = Angle;

                if (Angle == 180 || Angle == 270)
                {
                    RotateTransform rotate = new RotateTransform(-180);
                    line.K.LayoutTransform = rotate;
                    line.N.LayoutTransform = rotate;
                    line.Length.LayoutTransform = rotate;
                    line.Brand.VerticalAlignment = VerticalAlignment.Top;
                    line.Brand.LayoutTransform = rotate;
                }

                var contextL = line.DataContext as LineViewModel;
                contextL.Flag = true;

                var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                
                Canvas.SetLeft(line, X);
                Canvas.SetTop(line, Y);

                window.GridChange.Children.Add(line);
                GlobalGrid.GetInstance().Lines.Add(line);

                var contextMain = window.DataContext as MainWindowViewModel;
                window.curr = line;
                contextMain.Model.MainClick(window);
            }
            else if (DataContext is TransformerModel contextTransformer)
            {
                var global = GlobalGrid.GetInstance();
                var item = new Transformer();
                using (var context = new MyDbContext())
                {
                    item = context.Transformers.Where(x => x.Brand == contextTransformer.Brand).Single();
                }
                var transformer = new TransformerView(item, item.Snom, 0.92) { Height = 50, Width = 100 };
                ((RotateTransform)transformer.RenderTransform).Angle = Angle;
                if (Angle == 180 || Angle == 270)
                {
                    RotateTransform rotate = new RotateTransform(-180);
                    transformer.K.LayoutTransform = rotate;
                    transformer.Brand.VerticalAlignment = VerticalAlignment.Top;
                    transformer.Brand.LayoutTransform = rotate;
                }
                global.Transformers.Add(transformer);

                var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                Canvas.SetLeft(transformer, X);
                Canvas.SetTop(transformer, Y);
                window.GridChange.Children.Add(transformer);
                var contextMain = window.DataContext as MainWindowViewModel;
                window.curr = transformer;
                contextMain.Model.MainClick(window);
            }
            if (List.Count() > 0)
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
