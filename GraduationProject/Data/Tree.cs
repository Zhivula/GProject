using GraduationProject.Model;
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
    public class Tree<T> where T: IComparable
    {
        public Node<T> Root { get; private set; }
         
        public int Count { get; private set; }

        public Tree()
        {

        }
        public void Add(T start, T finish, UserControl view)
        {       
            if(Root == null)
            {
                var node = new Node<T>(finish, view, null);
                Root = node;
                Count = 1;
                return;
            }
            if (Root.Data.CompareTo(start) == 0)
            {
                var node = new Node<T>(finish, view, Root);
                Count++;
                Root.List.Add(node);
            }
            if(Root.Data.CompareTo(start) == -1)
            {
                Count++;
                Root.Add(start, finish, view);
            }  
        }
        public Node<T> Find(T start, T finish)
        {
            if (Root != null)
            {
                return Root.Find(start, finish);
            }
            else return null;
        }
        public Dictionary<int, double> GetdWxx()
        {
            var dictionary = new Dictionary<int, double>();
            if (Root != null) return Root.GetdWxx(dictionary);
            else return dictionary;
        }
        public double GetSumL()
        {
            var dictionary = new Dictionary<int, double>();
            if (Root != null) dictionary = Root.GetSumL(dictionary);
            return dictionary.Values.Sum();
        }
        public double GetSumT()
        {
            var dictionary = new Dictionary<int, double>();
            if (Root != null) dictionary = Root.GetSumT(dictionary);
            return dictionary.Values.Sum();
        }
        /// <summary>
        /// Изменяет коэффициент загрузки всех трансформаторов
        /// </summary>
        /// <param name="newKz">Новый коэффициент загрузки трансформаторов</param>
        public void ChangedKz(double newKz)
        {
            if (Root != null) Root.ChangedKz(newKz);
        }
        public void Delete(Node<T> node)
        {
            if (node != null)
            {
                if (node.View.DataContext is TransformerViewModel)
                {
                    //node.Delete(node.View);
                    var index = node.Parent.List.IndexOf(node);
                    var transformer = node.View.DataContext as TransformerViewModel;
                    GlobalGrid.GetInstance().BoxK.Add(transformer.K);
                    transformer.N = 0;
                    transformer.K = 0;
                    node.Delete(node.View);
                    node.Parent.List.Remove(node.Parent.List[index]);
                }
                //Здесь нужно "глубокое удаление" для линий
                if (node.View.DataContext is LineViewModel)
                {
                    var contextButton = node.Parent.View.DataContext as LineViewModel;
                    var line = node.View.DataContext as LineViewModel;
                    GlobalGrid.GetInstance().BoxK.Add(line.K);
                    line.N = 0;
                    line.K = 0;
                }
            }
        }
        public void Volt()
        {
            if(Root != null)
            {
                Root.Volt((float)GlobalGrid.U);
            }
        }
        public List<Node<T>> GetTransformers()
        {
            var list = new List<Node<T>>();
            if (Root != null && Root.List.Count > 0)
            {
                Root.GetTransformer(list);
            }
            return list;
        }
        public List<Node<T>> GetElements()
        {
            var list = new List<Node<T>>();
            if (Root != null && Root.List.Count > 0)
            {
                Root.GetElements(ref list);
            }
            return list;
        }
        /// <summary>
        /// Обход дерева
        /// </summary>
        public List<Node<T>> Foreach()
        {
            var list = new List<Node<T>>();
            if (Root != null && Root.List.Count > 0)
            {
                Root.GetNode(list);
            }
            return list;
        }
        public TreeSerializable ConvertToSerializable()
        {
            var global = GlobalGrid.GetInstance();
            var treeSerializable = new TreeSerializable();
            var dataContextSource = global.Source.DataContext as SourceViewModel;

            treeSerializable.SourceModel = dataContextSource.Model;
            treeSerializable.XSource = Canvas.GetLeft(global.Source);
            treeSerializable.YSource = Canvas.GetTop(global.Source);

            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            treeSerializable.HeightField = window.GridChangeFirst.Height;
            treeSerializable.WidthField = window.GridChangeFirst.Width;

            if (Root != null)
            {
                var context = Root.View.DataContext as LineViewModel;
                var x = Canvas.GetLeft(Root.View);
                var y = Canvas.GetTop(Root.View);
                var node = new NodeSerializable(context.K, Root.View.DataContext, null, 0, x, y);

                if (!treeSerializable.Contains(node))
                {                  
                    var model = new LineModel() {
                         K = context.K,
                         N = context.N,
                         Brand = context.Brand,
                         L = context.Length,
                         R0 = context.R0,
                         X0 = context.X0,
                         Idop = context.Idop
                    };
                    var angle = ((RotateTransform)Root.View.RenderTransform).Angle;
                    treeSerializable.Add(context.N, context.K, model, angle, x, y);
                    if (Root.List.Count>0)
                    {
                        foreach (var i in Root.List)
                        {
                            i.AddToSerializable(ref treeSerializable);
                        }
                    }
                }
            }
            
            return treeSerializable;
        }
        /// <summary>
        /// Удаляет дерево. Root ссылается на null, следовательно дерево становится пустым.
        /// </summary>
        public void DeleteTree()
        {
            Root = null;
            Count = 0;
            GlobalGrid.GetInstance().Lines = new List<View.LineView>();
            GlobalGrid.GetInstance().Transformers = new List<View.TransformerView>();
            GlobalGrid.GetInstance().Source = null;
        }
        public void CorrectNKPositions()
        {

        }
    }
}