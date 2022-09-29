using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entites
{
    public class Sales
    {
        //SAtışlar tablosu

        public int ID { get; set; }


        [Display(Name = "Ürün")]

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        [Display(Name = "Adet")]
        public int Quantity { get; set; }

        [Display(Name = "Fiat")]
        public decimal Price { get; set; }

        [Display(Name = "Tarih")]
        public DateTime date { get; set; }

        [Display(Name = "Resim")]
        public string Image { get; set; }

        [Display(Name = "Kullanıcı")]
        public int UserId { get; set; }

        public virtual User User { get; set; }


    }

}

