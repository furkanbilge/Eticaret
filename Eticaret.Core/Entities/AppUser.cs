using System.ComponentModel.DataAnnotations;

namespace Eticaret.Core.Entities
{
    public class AppUser : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        [Required(ErrorMessage = "Ad alanı boş bırakılamaz.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Ad en az 3, en fazla 15 karakter olmalıdır.")]
        public string Name { get; set; }

        [Display(Name = "Soyadı")]
        [Required(ErrorMessage = "Soyadı alanı boş bırakılamaz.")]
        [StringLength(20, ErrorMessage = "Soyad en fazla 20 karakter olmalıdır.")]
        public string Surname { get; set; }

        [Display(Name = "E-Posta")]
        [Required(ErrorMessage = "E-Posta alanı boş bırakılamaz.")]
        public string Email { get; set; }


        [Display(Name = "Telefon")]
        [StringLength(11)]
        public string? Phone { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Şifre en az 6, en fazla 15 karakter olmalıdır.")]
        public string Password { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string? UserName { get; set; }

        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }

        [Display(Name = "Admin?")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [ScaffoldColumn(false)]
        public Guid? UserGuid { get; set; } = Guid.NewGuid();
        public List<Address>? Addresses { get; set; }
    }
}
