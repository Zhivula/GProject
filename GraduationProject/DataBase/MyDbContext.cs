using System.Data.Entity;

namespace GraduationProject.DataBase
{
    class MyDbContext : DbContext
    {
        public MyDbContext() : base("DbConnectionString")
        {

        }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Transformer> Transformers { get; set; }
    }
}