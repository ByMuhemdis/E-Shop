using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entites
{
    //Ürün tablosu
    public class Product
    {
        public int Id { get; set; }

        [Required/*zoronlu*/ (ErrorMessage = "Boş geçilemez")]
        [Display(Name = "İsim")]
        [StringLength(50, ErrorMessage = "Maxsimum 50 karekter kullanılabilir")]
        public string Name { get; set; }

        [Required/*zoronlu*/ (ErrorMessage = "Boş geçilemez")]
        [Display(Name = "Açıklama")]
        [StringLength(50, ErrorMessage = "Maxsimum 50 karekter kullanılabilir")]
        public string Description { get; set; }

        [Required/*zoronlu*/ (ErrorMessage = "Boş geçilemez")]
        [Display(Name = "Fiat")]
        public decimal Price /*fiat*/ { get; set; }


        [Required/*zoronlu*/ (ErrorMessage = "Boş geçilemez")]
        [Display(Name = "Stok")]
        public int Stock { get; set; }


        [Required/*zoronlu*/ (ErrorMessage = "Boş geçilemez")]
        [Display(Name = "popüler")]
        public bool Popular { get; set; }


        [Required/*zoronlu*/ (ErrorMessage = "Boş geçilemez")]
        [Display(Name = "Onay")]
        public bool IsApproved/*onaylı mı, degil mi*/ { get; set; }


        [Required/*zoronlu*/ (ErrorMessage = "Boş geçilemez")]
        [Display(Name = "Resim")]
        public string Image { get; set; }



        [Required/*zoronlu*/ (ErrorMessage = "Boş geçilemez")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        //bir ürünün sadece bir  kategorisi olabilir bunun ilişkisel baglantısı
        public virtual Category Category { get; set; }
        public virtual List<Cart> Cart { get; set; }
        public virtual List<Sales> Sales { get; set; }


    }
}
