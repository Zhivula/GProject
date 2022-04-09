using GraduationProject.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Model
{
    public class CatalogTransformerModel
    {
        public ObservableCollection<Transformer> Collection { get; set; }

        public CatalogTransformerModel()
        {
            UpDateCollection();
        }
        public void Add(string Brand, string Snom, string Pkz, string Pxx, string Qxx, string Ixx, string Ukz, string R, string X)
        {
            using (var context = new MyDbContext())
            {
                var item = new Transformer()
                {
                    Brand = Brand,
                    Snom = double.Parse(Snom),
                    Pkz = double.Parse(Pkz),
                    Pxx = double.Parse(Pxx),
                    Qxx = double.Parse(Qxx),
                    Ixx = double.Parse(Ixx),
                    Ukz = double.Parse(Ukz),
                    R = double.Parse(R),
                    X = double.Parse(X)
                };
                context.Transformers.Add(item);
                context.SaveChanges();
            }
        }
        private void UpDateCollection()
        {
            Collection = new ObservableCollection<Transformer>();

            using (var context = new MyDbContext())
            {
                foreach (var i in context.Transformers)
                {
                    Collection.Add(i);
                }
            }
        }
    }
}
