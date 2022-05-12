using GraduationProject.DataBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GraduationProject
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            using (var context = new MyDbContext())
            {
                //var list = context.Lines.ToList();
                //var ac50 = context.Lines.Where(x => x.Brand == "АС-50").FirstOrDefault();
                //ac50.Idop = 210;
                //var ac35 = context.Lines.Where(x => x.Brand == "АС-35").FirstOrDefault();
                //ac35.Idop = 175;
                //var ac70 = context.Lines.Where(x => x.Brand == "АС-70").FirstOrDefault();
                //ac70.Idop = 265;
                //var ashvv = context.Lines.Where(x => x.Brand == "ААШВ 3x75").FirstOrDefault();
                //ashvv.Idop = 162;
                //var ashvv = context.Lines.FirstOrDefault();
                //ashvv.Idop = 210;
                //context.SaveChanges();
                //context.Database.Delete();
                //context.Database.Create();
            }
        }
    }
}
