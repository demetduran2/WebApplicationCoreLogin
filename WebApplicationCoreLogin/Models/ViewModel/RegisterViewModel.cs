using System.ComponentModel.DataAnnotations;

namespace WebApplicationCoreLogin.Models.ViewModel
{
    public class RegisterViewModel//kayıt yapar
    {
       
        [Required(ErrorMessage = "Kullanıcı Adı Zorunludur")]
        [StringLength(30, ErrorMessage = "Kullanıcı Adı alanı max 30 karakter olmalı")]
        public string UserName { get; set; }
        
        [Required]
        [MinLength(6)]
        [MaxLength(16)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(16)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string Password2 { get; set; }

    }
}
