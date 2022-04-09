using GraduationProject.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Model
{
    public class CatalogLineModel
    {
        public ObservableCollection<Line> Collection { get; set; }

        public CatalogLineModel()
        {
            UpDateCollection();
        }
        public void Add(string brand, string r0, string x0, string idop)
        {
            using (var context = new MyDbContext())
            {
                var item = new Line()
                {
                    Brand = brand,
                    R0 = double.Parse(r0),
                    X0 = double.Parse(x0),
                    Idop = double.Parse(idop)
                };
                context.Lines.Add(item);
                context.SaveChanges();
            }
        }
        private void UpDateCollection()
        {
            Collection = new ObservableCollection<Line>();

            using (var context = new MyDbContext())
            {
                foreach (var i in context.Lines)
                {
                    Collection.Add(i);
                }
            }
        }
    }
}
