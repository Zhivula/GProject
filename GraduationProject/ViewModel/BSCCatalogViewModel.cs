using GraduationProject.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Excel = Microsoft.Office.Interop.Excel;

namespace GraduationProject.ViewModel
{
    public class BSCCatalogViewModel : INotifyPropertyChanged
    {
        private string brand;
        private string c;
        private string q;

        public string Brand
        {
            get => brand;
            set
            {
                brand = value;
                OnPropertyChanged(nameof(Brand));
            }
        }
        public string C
        {
            get => c;
            set
            {
                c = value;
                OnPropertyChanged(nameof(C));
            }
        }
        public string Q
        {
            get => q;
            set
            {
                q = value;
                OnPropertyChanged(nameof(Q));
            }
        }
        public BSCCatalogViewModel()
        {
            ////Start Excel and get Application object.
            //var oXL = new Excel.Application();
            ////Get a new workbook.
            //var oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
            //var oSheet = (Excel._Worksheet)oWB.ActiveSheet;

            //    oSheet.Cells[1, 1] = 1;
            //    oSheet.Cells[2, 1] = 1;
            //    oSheet.Cells[3, 1] = 1;
            
            //oWB.SaveAs("modelBSK.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);



        }
        public ICommand Close => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.StaticGrid.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.StaticGrid.Children[i].GetType().Name;
                if (childTypeName == nameof(BSCCatalogView))
                {
                    window.StaticGrid.Children.RemoveAt(i);
                }
            }
        });
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
