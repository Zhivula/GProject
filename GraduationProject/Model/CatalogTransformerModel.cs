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
        public void Add(string Brand, double Snom, double Pkz, double Pxx, double Ixx, double Ukz, double Unom)
        {
            using (var context = new MyDbContext())
            {
                var item = new Transformer()
                {
                    Brand = Brand,
                    Snom = Snom,
                    Pkz = Pkz,
                    Pxx = Pxx,
                    Qxx = Math.Round((Ixx*Snom)/100,5),
                    Ixx = Ixx,
                    Ukz = Ukz,
                    R = Math.Round((Pkz*Unom*Unom*1000)/(Snom* Snom),5),
                    X = Math.Round(Math.Sqrt(Math.Pow(((Ukz * Unom * Unom*10) / Snom),2) - Math.Pow((Pkz * Unom * Unom*1000) / (Snom * Snom), 2)),5)
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
                    i.Qxx = Math.Round(i.Qxx,5);
                    i.R = Math.Round(i.R,5);
                    i.X = Math.Round(i.X,5);
                    Collection.Add(i);
                }
            }
        }
    }
}
