using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entites
{
    public class User
    {
        public int Id { get; set; }

        
        [Required/*zoronlu*/ (ErrorMessage = "Boş geçilemez")]
        [Display(Name = "İsim")]
        [StringLength (50, ErrorMessage ="Maksimum 50 karekterden oluşmalıdır")]
        public string Name { get; set; }

        [Required/*zoronlu*/ (ErrorMessage = "Boş geçilemez")]
        [Display(Name = "Soyisim")]
        [StringLength(50, ErrorMessage = "Maksimum 50  karekterden oluşmalıdır")]
        public string SurName { get; set; }

        //[Required/*zoronlu*/ (ErrorMessage = "Boş geçilemez")]
        //[Display(Name = "E-mail")]
        //[EmailAddress( ErrorMessage = "E-mail  formatından  oluşmalıdır")]
        public string Email { get; set; }

        [Required/*zoronlu*/ (ErrorMessage = "Boş geçilemez")]
        [Display(Name = "Kullanıcı Adı")]
        [StringLength(50, ErrorMessage = "Maksimum 50 karekterden oluşmalıdır")]
        public string UserName { get; set; }


        //[Required/*zoronlu*/ (ErrorMessage = "Boş geçilemez")]
        //[Display(Name = "şifre")]
        //[StringLength(10, ErrorMessage = "Maksimum 10 karekterden oluşmalıdır")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required/*zoronlu*/ (ErrorMessage = "Boş geçilemez")]
        //[Display(Name = "şifre")]
        //[StringLength(10, ErrorMessage = "Maksimum 10 karekterden oluşmalıdır")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage ="Şİfre uyuşmuyor")]//pasaport tekrarında uyumusup uyusmadıgını kontrol etmek amacıyla yazılmıştır
        public string RePassword { get; set; }

        [StringLength(10, ErrorMessage = "maksimum 10 karekterden oluşmalıdır")]
        public string Role { get; set; }
    }
}
