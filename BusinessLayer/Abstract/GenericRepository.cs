using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    //IRepository deki interfacede metotları belirttik fakat içerisine çalışması için hiç birsey yapmamıştır bunları burada gercekleştirecegiz

    public class GenericRepository<T> : IRepository<T> where T : class, new()
    {

        DataContext db= new DataContext();
        DbSet<T> data;
        

        public GenericRepository()
        {
            data=db.Set<T>();
        }

        //silme işlemi
        public void Delete(T p)
        {
           data.Remove(p);
           db.SaveChanges();
         
        }
        // ID ye göre teirme işlemi
        public T GetById(int Id)
        {
            return data.Find(Id);
           
        }
        
        //ekleme işlemi

        public void insert(T p)
        {
            data.Add(p);
            db.SaveChanges();
           
        }
        //listeleme işlemi
        public List<T> List()
        {
            var context = new DataContext();
           return  context.Set<T>().ToList();
        }

        public void update(T p)
        {
         
           db.SaveChanges();
        }
    }
}
