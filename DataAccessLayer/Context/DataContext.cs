using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class DataContext: DbContext
    {

       
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Sales> sales { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Comment> comments { get; set; }

    }
}
//bir dk 