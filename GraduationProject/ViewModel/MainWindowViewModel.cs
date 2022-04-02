using GraduationProject.Data;
using GraduationProject.Model;
using GraduationProject.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraduationProject.ViewModel
{
    public class MainWindowViewModel
    {
        private MainWindow window;

        public MainWindowViewModel()
        {
            window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        }
        public void MainClick()
        {
            var global = GlobalGrid.GetInstance();
            if (window.curr is Button1 activeLine)
            {

                var DataContextLine = activeLine.DataContext as ButtonViewModel;
                var active_left = Canvas.GetLeft(activeLine);
                var active_top = Canvas.GetTop(activeLine);

                if (global.Lines.Count >= 0)
                {
                    foreach (var item in global.Lines)
                    {
                        var itemDataContext = item.DataContext as ButtonViewModel;
                        double another_left = 0;
                        double another_top = 0;
                        //Ниже описываются различные варианты положений элементов относительно друг друга
                        //предыдуший элемент расположен горизонтально: 0 градусов поворот
                        //активный элемент расположен вертикально: 270 или 90 градусов поворот
                        if (((RotateTransform)item.RenderTransform).Angle == 0)
                        {
                            another_left = Canvas.GetLeft(item) + 90;
                            another_top = Canvas.GetTop(item);

                            if (((RotateTransform)activeLine.RenderTransform).Angle == 270)
                            {
                                another_left -= 20;
                                another_top += 30;
                            }
                            if (((RotateTransform)activeLine.RenderTransform).Angle == 90)
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

                            if (((RotateTransform)activeLine.RenderTransform).Angle == 0)
                            {
                                another_left -= 30;
                                another_top -= 20;
                            }
                            if (((RotateTransform)activeLine.RenderTransform).Angle == 180)
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

                            if (((RotateTransform)activeLine.RenderTransform).Angle == 270)
                            {
                                another_left -= 30;
                                another_top -= 20;
                            }
                            if (((RotateTransform)activeLine.RenderTransform).Angle == 90)
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

                            if (((RotateTransform)activeLine.RenderTransform).Angle == 0)
                            {
                                another_left += 20;
                                another_top -= 30;
                            }
                            if (((RotateTransform)activeLine.RenderTransform).Angle == 180)
                            {
                                another_left += 30;
                                another_top += 20;
                            }
                        }
                        //MessageBox.Show("X1: " + another_left + "X2: " + active_left + "Y1:" + another_top + "Y2:" + active_top);

                        //Специальный случай для первой линии, чтобы она подключилась к источнику
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
                        if ((active_left == another_left && active_top == another_top) && (itemDataContext.Flag == true))
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
            if (window.curr is TransformerView activeTransformer)
            {
                var context = activeTransformer.DataContext as TransformerViewModel;
                var active_left = Canvas.GetLeft(activeTransformer);
                var active_top = Canvas.GetTop(activeTransformer);
                if (global.Lines.Count >= 0)
                {
                    foreach (var item in global.Lines)
                    {
                        var itemDataContext = item.DataContext as ButtonViewModel;

                        //var another_left = Canvas.GetLeft(item) + 100;
                        //var another_top = Canvas.GetTop(item);

                        double another_left = 0;
                        double another_top = 0;
                        //Ниже описываются различные варианты положений элементов относительно друг друга
                        //предыдуший элемент расположен горизонтально: 0 градусов поворот
                        //активный элемент расположен вертикально: 270 или 90 градусов поворот
                        if (((RotateTransform)item.RenderTransform).Angle == 0)
                        {
                            another_left = Canvas.GetLeft(item) + 90;
                            another_top = Canvas.GetTop(item);

                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 270)
                            {
                                another_left -= 20;
                                another_top += 30;
                            }
                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 90)
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

                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 0)
                            {
                                another_left -= 30;
                                another_top -= 20;
                            }
                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 180)
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

                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 270)
                            {
                                another_left -= 30;
                                another_top -= 20;
                            }
                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 90)
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

                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 0)
                            {
                                another_left += 20;
                                another_top -= 30;
                            }
                            if (((RotateTransform)activeTransformer.RenderTransform).Angle == 180)
                            {
                                another_left += 30;
                                another_top += 20;
                            }
                        }

                        //Реализовать DELETE
                        if (context.Flag == true && (active_left != another_left || active_top != another_top))
                        {
                            context.Flag = false;
                            var currentNode = global.Tree.Find(context.N, context.K);
                            global.Tree.Delete(currentNode);

                        }
                        if (active_left == another_left && active_top == another_top && (itemDataContext.Flag == true))
                        {
                            if (GlobalGrid.GetInstance().BoxK.Count > 0)
                            {
                                context.K = global.BoxK.First();
                                global.BoxK.Remove(global.BoxK.First());
                            }
                            else
                            {
                                context.K = global.Tree.Count + 1;
                            }
                            context.N = itemDataContext.K;
                            global.Tree.Add(context.N, context.K, activeTransformer);
                            context.Flag = true;
                        }
                    }
                }
            }
            if (window.curr is SourceView source)
            {
                var context = source.DataContext as SourceViewModel;
                GlobalGrid.U = context.Voltage;
            }
            window.curr = null;
        }
        public ICommand CreateNewSource => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.StaticGrid.Children.Add(new AddSourceView());
        });
        public ICommand CreateNewLine => new DelegateCommand(o =>
        { 
            //window.GridChange.Children.Clear();
            window.StaticGrid.Children.Add(new AddLineView());
        }); 
        public ICommand CreateNewTransformer => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.StaticGrid.Children.Add(new AddTransformerView());
        });
        public ICommand CatalogLine => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.StaticGrid.Children.Add(new CatalogLineView());
        });
        public ICommand CatalogTransformer => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.StaticGrid.Children.Add(new CatalogTransformerView());
        });
        public ICommand Analysis => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.FullGridChange.Children.Add(new ModeAnalysisView());
        });
        public ICommand Settings => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.GridChangeFirst.Children.Add(new SettingsView());
        });
        public ICommand AddRight => new DelegateCommand(o =>
        {
            var width = window.GridChangeFirst.ActualWidth;
            var height = window.GridChangeFirst.ActualHeight;

            for (var i = 7; i < 27; i++)
            {
                window.GridChange.Children.Add(new Line() { X1 = (i * 10) + window.GridChangeFirst.ActualWidth - window.GridChangeFirst.ActualWidth%10, X2 = (i * 10) + window.GridChangeFirst.ActualWidth - window.GridChangeFirst.ActualWidth % 10, Y1 = 0, Y2 = height, Stroke = Brushes.Gray });
            }

            window.GridChangeFirst.Width = width + 200;

            for (var i = 0; i < window.GridChange.Children.Count; i++)
            {
                if (window.GridChange.Children[i] is Line line)
                {
                    if (line.X1 == 0)
                    {
                        line.X2 += 200;
                    }
                }
            }
        });
        public ICommand AddBottom => new DelegateCommand(o =>
        {
            var width = window.GridChangeFirst.ActualWidth;
            var height = window.GridChangeFirst.ActualHeight;

            for (var i = 53; i < 73; i++)
            {
                window.GridChange.Children.Add(new Line() { X1 = 0, X2 = width, Y1 = (i * 10) + window.GridChangeFirst.ActualHeight - window.GridChangeFirst.ActualHeight % 10, Y2 = (i * 10) + window.GridChangeFirst.ActualHeight - window.GridChangeFirst.ActualHeight % 10, Stroke = Brushes.Gray });
            }

            window.GridChangeFirst.Height = height + 200;

            for (var i = 0; i < window.GridChange.Children.Count; i++)
            {
                if (window.GridChange.Children[i] is Line line)
                {
                    if (line.Y1 == 0)
                    {
                        line.Y2 += 200;
                    }
                }
            }
        });
        public ICommand ClearGrid => new DelegateCommand(o =>
        {
            for (int i = window.GridChange.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.GridChange.Children[i].GetType().Name;
                if (childTypeName == "Button1" | childTypeName == "TransformerView" | childTypeName == "SourceView")
                {
                    window.GridChange.Children.RemoveAt(i);
                }
            }
            GlobalGrid.GetInstance().Tree.DeleteTree();

            MessageBox.Show("Успешно выполнено!");
        });
        public ICommand SaveModel => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            var obj = GlobalGrid.GetInstance().Tree.ConvertToSerializable();
            BinaryFormatter binFormat = new BinaryFormatter();
            
            using (Stream fStream = new FileStream("model.dat", FileMode.Create, FileAccess.Write, FileShare.None)) // Сохранить объект в локальном файле.
            {
                binFormat.Serialize(fStream, obj);
                MessageBox.Show("Успешно выполнено!");
            }  
        });
        public ICommand OpenModel => new DelegateCommand(o =>
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
                    if (childTypeName == "Line")
                    {
                        window.GridChange.Children.RemoveAt(i);
                    }
                }

                var countWidth = Math.Round(tree.WidthField / 10);
                var countHeight = Math.Round(tree.HeightField / 10);

                for (var i = 1; i < countHeight; i++)
                {
                    window.GridChange.Children.Add(new Line() { X1 = i * 10, X2 = i * 10, Y1 = 0, Y2 = countHeight * 10, Stroke = Brushes.Gray });
                }
                for (var i = 1; i < countWidth; i++)
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
                
                var line = new Button1(context.Brand, context.L, context.R0, context.X0) { Height = 50, Width = 100};
                var contextLine = line.DataContext as ButtonViewModel;
                contextLine.Flag = false;
                ((RotateTransform)line.RenderTransform).Angle = tree.Root.Angle;
                Canvas.SetLeft(line, tree.Root.X);
                Canvas.SetTop(line, tree.Root.Y);
                window.GridChange.Children.Add(line);
                global.Lines.Add(line);
                window.curr = line;
                MainClick();
                //window.GridChange.MouseMove += new MouseEventHandler(window.MouseEvent);
                //var click = new MouseButtonEventArgs(o as  , 0, MouseButton.Right);

                if (tree.Root.List.Count > 0)
                {
                    foreach (var i in tree.Root.List)
                    {
                        i.DeserializableNode();
                    }
                }
            }

            MessageBox.Show("Успешно выполнено!");
        });
    }
    
}
