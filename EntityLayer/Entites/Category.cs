using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entites
{
    public class Category
    {
        public int Id { get; set; }

        [Required/*zoronlu*/ (ErrorMessage ="Boş geçilemez")]
        [Display(Name ="İsim")]
        [StringLength(50,ErrorMessage ="Maxsimum 50 karekter kullanılabilir")]
        public string Name { get; set; }

        [Required/*zoronlu*/ (ErrorMessage = "Boş geçilemez")]
        [Display(Name = "Açıklama")]
        [StringLength(50, ErrorMessage = "Maxsimum 50 karekter kullanılabilir")]
        public string Description { get; set; }
        //Description==Açıklama

        //bir kategoride birden fazla uerun olabilir bunun ilişkisel baglantısı

        public virtual List<Product> Products { get; set; }
    }
}
