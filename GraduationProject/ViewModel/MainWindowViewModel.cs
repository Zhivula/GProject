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
using System.Windows.Input;
using System.Windows.Media;

namespace GraduationProject.ViewModel
{
    class MainWindowViewModel
    {
        private MainWindow window;

        public MainWindowViewModel()
        {
            window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        }
        public ICommand CreateNewSource => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.GridChangeFirst.Children.Add(new AddSourceView());
        });
        public ICommand CreateNewLine => new DelegateCommand(o =>
        { 
            //window.GridChange.Children.Clear();
            window.GridChangeFirst.Children.Add(new AddLineView());
        }); 
        public ICommand CreateNewTransformer => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.GridChangeFirst.Children.Add(new AddTransformerView());
        });
        public ICommand CatalogLine => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.GridChangeFirst.Children.Add(new CatalogLineView());
        });
        public ICommand CatalogTransformer => new DelegateCommand(o =>
        {
            //window.GridChange.Children.Clear();
            window.GridChangeFirst.Children.Add(new CatalogTransformerView());
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
                var global = GlobalGrid.GetInstance();
                var context = tree.Root.DataContext as LineModel;
                var line = new Button1(context.Brand, context.L, context.R0, context.X0) { Height = 50, Width = 100};
                ((RotateTransform)line.RenderTransform).Angle = tree.Root.Angel;
                window.GridChange.Children.Add(line);

                global.Lines.Add(line);
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
