using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    //interfaceler bunlar s eni yanlış anlamamıssam tamam 
    public interface IRepository<T> where T : class,new() // sart koyuyoruz burada t class özelliginde olacak ve yenilenebilir olacak anlamındadır
    {
        //sil güncelle listele özellikleri için buraya metotları oluşturacagız
        List<T> List();
        void insert(T p);
        void Delete(T p);

        void update(T p);

        T GetById(int Id);
    }
}
