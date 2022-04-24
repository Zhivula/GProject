using GraduationProject.Data;
using GraduationProject.View;
using GraduationProject.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraduationProject.Model
{
    public class MainWindowModel
    {
        private readonly GlobalGrid global = GlobalGrid.GetInstance();
        private bool FlagViewGrid { get; set; }

        public MainWindowModel()
        {
            FlagViewGrid = true;
        }
        /// <summary>
        /// Добавление нового элемента на рабочее пространство
        /// </summary>
        /// <param name="window"></param>
        public void MainClick(MainWindow window)
        {
            if (window.curr is LineView line)
            {
                AddLine(line);
            }
            if (window.curr is TransformerView transformer)
            {
                AddTransformer(transformer);
            }
            if (window.curr is SourceView source)
            {
                AddSource(source);
            }
            window.curr = null;
        }
        /// <summary>
        /// Подключение к сети нового участка - линии.
        /// </summary>
        /// <param name="activeLine">Активный элемент - линия (View)</param>
        private void AddLine(LineView activeLine)
        {
            var DataContextLine = activeLine.DataContext as LineViewModel;
            var active_left = Canvas.GetLeft(activeLine);
            var active_top = Canvas.GetTop(activeLine);

            if (global.Lines.Count >= 0)
            {
                foreach (var item in global.Lines)
                {
                    var itemDataContext = item.DataContext as LineViewModel;

                    var point = GetPosition(item, activeLine);

                    double X = 0;
                    double Y = 0;
                    if (global.Source != null)
                    {
                        X = Canvas.GetLeft(global.Source) + 150;
                        Y = Canvas.GetTop(global.Source) + 20;
                    }

                    if (active_left == X && active_top == Y && (DataContextLine.Flag == false))
                    {
                        DataContextLine.N = 0;
                        DataContextLine.K = 1;
                        DataContextLine.Flag = true;
                        global.Tree.Add(0, 1, activeLine);
                    }

                    //Есть общаая точка с другой линией и та линия имеет питание
                    if ((active_left == point.X && active_top == point.Y) && (itemDataContext.Flag == true))
                    {
                        DataContextLine.K = global.Tree.Count + 1;
                        DataContextLine.N = itemDataContext.K;
                        DataContextLine.Flag = true;
                        global.Tree.Add(DataContextLine.N, DataContextLine.K, activeLine);
                        continue;
                    }
                    //Реализовать DELETE
                }
            }
        }
        /// <summary>
        /// Подключение к сети нового элемента - трансформатора.
        /// </summary>
        /// <param name="transformer">Активны элемент - трансформатор (View)</param>
        private void AddTransformer(TransformerView transformer)
        {
            var context = transformer.DataContext as TransformerViewModel;
            var active_left = Canvas.GetLeft(transformer);
            var active_top = Canvas.GetTop(transformer);
            if (global.Lines.Count >= 0)
            {
                foreach (var item in global.Lines)
                {
                    var itemDataContext = item.DataContext as LineViewModel;

                    var point = GetPosition(item, transformer);

                    //Реализовать DELETE
                    if (context.Flag == true && (active_left != point.X || active_top != point.Y))
                    {
                        context.Flag = false;
                        var currentNode = global.Tree.Find(context.N, context.K);
                        global.Tree.Delete(currentNode);

                    }
                    if (active_left == point.X && active_top == point.Y && (itemDataContext.Flag == true))
                    {
                        if (global.BoxK.Count > 0)
                        {
                            context.K = global.BoxK.First();
                            global.BoxK.Remove(global.BoxK.First());
                        }
                        else
                        {
                            context.K = global.Tree.Count + 1;
                        }
                        context.N = itemDataContext.K;
                        global.Tree.Add(context.N, context.K, transformer);
                        context.Flag = true;
                    }
                }
            }
        } 
        /// <summary>
        /// Добавление источника питания (без проверок, так как его можно ставить в любом месте).
        /// </summary>
        /// <param name="source"></param>
        private void AddSource(SourceView source)
        {
            var context = source.DataContext as SourceViewModel;
            GlobalGrid.U = context.Voltage;
        }
        /// <summary>
        /// Возвращает точку, которая соответствует точке подключения активного элемента к сети.
        /// </summary>
        /// <param name="item">Линия, к которой будет подключаться активный элемент</param>
        /// <param name="activeElement">Активный элемент</param>
        /// <returns></returns>
        private Point GetPosition(UserControl item, UserControl activeElement)
        {
            double another_left = 0;
            double another_top = 0;

            //Ниже описываются различные варианты положений элементов относительно друг друга

            //предыдуший элемент расположен горизонтально: 0 градусов поворот
            //активный элемент расположен вертикально: 270 или 90 градусов поворот
            if (((RotateTransform)item.RenderTransform).Angle == 0)
            {
                another_left = Canvas.GetLeft(item) + 90;
                another_top = Canvas.GetTop(item);

                if (((RotateTransform)activeElement.RenderTransform).Angle == 270)
                {
                    another_left -= 20;
                    another_top += 30;
                }
                if (((RotateTransform)activeElement.RenderTransform).Angle == 90)
                {
                    another_left += 30;
                    another_top += 20;
                }
            }
            //предыдуший элемент расположен вертикально: 90 градусов поворот
            //активный элемент расположен горизонтально: 0 или 180 градусов поворот
            if (((RotateTransform)item.RenderTransform).Angle == 90)
            {
                another_left = Canvas.GetLeft(item);
                another_top = Canvas.GetTop(item) + 90;

                if (((RotateTransform)activeElement.RenderTransform).Angle == 0)
                {
                    another_left -= 30;
                    another_top -= 20;
                }
                if (((RotateTransform)activeElement.RenderTransform).Angle == 180)
                {
                    another_left -= 20;
                    another_top += 30;
                }
            }
            //предыдуший элемент расположен горизонтально: 180 градусов поворот
            //активный элемент расположен вертикально: 270 или 90 градусов поворот
            if (((RotateTransform)item.RenderTransform).Angle == 180)
            {
                another_left = Canvas.GetLeft(item) - 90;
                another_top = Canvas.GetTop(item);

                if (((RotateTransform)activeElement.RenderTransform).Angle == 270)
                {
                    another_left -= 30;
                    another_top -= 20;
                }
                if (((RotateTransform)activeElement.RenderTransform).Angle == 90)
                {
                    another_left += 20;
                    another_top -= 30;
                }
            }
            //предыдуший элемент расположен вертикально: 270 градусов поворот
            //активный элемент расположен горизонтально: 0 или 180 градусов поворот
            if (((RotateTransform)item.RenderTransform).Angle == 270)
            {
                another_left = Canvas.GetLeft(item);
                another_top = Canvas.GetTop(item) - 90;

                if (((RotateTransform)activeElement.RenderTransform).Angle == 0)
                {
                    another_left += 20;
                    another_top -= 30;
                }
                if (((RotateTransform)activeElement.RenderTransform).Angle == 180)
                {
                    another_left += 30;
                    another_top += 20;
                }
            }
            return new Point(another_left, another_top);
        }
        public void ViewGrid()
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (FlagViewGrid == true)
            {
                for (var i = 0; i < window.GridChange.Children.Count; i++)
                {
                    if (window.GridChange.Children[i] is Line) window.GridChange.Children[i].Visibility = Visibility.Hidden;
                }
                FlagViewGrid = false;
            }
            else
            {
                for (var i = 0; i < window.GridChange.Children.Count; i++)
                {
                    if (window.GridChange.Children[i] is Line) window.GridChange.Children[i].Visibility = Visibility.Visible;
                }
                FlagViewGrid = true;
            }
        }
        public void CorrectLines()
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            window.StaticGrid.Children.Add(new CorrectLineView());
        }
        public void VDT()
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            window.StaticGrid.Children.Add(new VDTView());
        }
        public void BSK()
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            window.StaticGrid.Children.Add(new BSKView());
        }
        /// <summary>
        /// Сохранение сериализируемой модели сети в отдельный файл.
        /// </summary>
        public void SaveModel()
        {
            var obj = GlobalGrid.GetInstance().Tree.ConvertToSerializable();
            BinaryFormatter binFormat = new BinaryFormatter();

            using (Stream fStream = new FileStream("model.dat", FileMode.Create, FileAccess.Write, FileShare.None)) // Сохранить объект в локальном файле.
            {
                binFormat.Serialize(fStream, obj);
                MessageBox.Show("Успешно выполнено!");
            }
        }
        /// <summary>
        /// Открытие файла и десериализация модели сети.
        /// </summary>
        /// <param name="window">Текущее активное рабочее окно (View)</param>
        public void OpenModel(MainWindow window)
        {
            //Format the object as Binary  
            BinaryFormatter formatter = new BinaryFormatter();

            //Reading the file from the server  
            FileStream fs = File.Open("model.dat", FileMode.Open);

            object obj = formatter.Deserialize(fs);
            TreeSerializable tree = (TreeSerializable)obj;
            fs.Flush();
            fs.Close();
            fs.Dispose();

            if (tree.Root != null)
            {
                window.GridChangeFirst.Height = tree.HeightField;
                window.GridChangeFirst.Width = tree.WidthField;

                for (int i = window.GridChange.Children.Count - 1; i >= 0; --i)
                {
                    var childTypeName = window.GridChange.Children[i].GetType().Name;
                    if (childTypeName == nameof(Line))
                    {
                        window.GridChange.Children.RemoveAt(i);
                    }
                }

                var countWidth = Math.Round(tree.WidthField / 10);
                var countHeight = Math.Round(tree.HeightField / 10);

                for (var i = 1; i < countWidth; i++)
                {
                    window.GridChange.Children.Add(new Line() { X1 = i * 10, X2 = i * 10, Y1 = 0, Y2 = countHeight * 10, Stroke = Brushes.Gray });
                }
                for (var i = 1; i < countHeight; i++)
                {
                    window.GridChange.Children.Add(new Line() { X1 = 0, X2 = countWidth * 10, Y1 = i * 10, Y2 = i * 10, Stroke = Brushes.Gray });
                }

                var global = GlobalGrid.GetInstance();
                var source = new SourceView(tree.SourceModel.Name, tree.SourceModel.Voltage) { Height = 90, Width = 150 };
                global.Source = source;
                Canvas.SetLeft(source, tree.XSource);
                Canvas.SetTop(source, tree.YSource);
                window.GridChange.Children.Add(source);

                var context = tree.Root.DataContext as LineModel;

                var line = new LineView(context.Brand, context.L, context.R0, context.X0, context.Idop) { Height = 50, Width = 100 };
                var contextLine = line.DataContext as LineViewModel;
                contextLine.Flag = false;
                ((RotateTransform)line.RenderTransform).Angle = tree.Root.Angle;
                Canvas.SetLeft(line, tree.Root.X);
                Canvas.SetTop(line, tree.Root.Y);
                window.GridChange.Children.Add(line);
                global.Lines.Add(line);
                window.curr = line;
                MainClick(window);

                if (tree.Root.List.Count > 0)
                {
                    foreach (var i in tree.Root.List)
                    {
                        i.DeserializableNode();
                    }
                }
            }

            MessageBox.Show("Успешно выполнено!");
        }
    }
}
