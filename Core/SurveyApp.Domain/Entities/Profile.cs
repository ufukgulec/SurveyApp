using SurveyApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SurveyApp.Domain.Entities
{
    public class Profile : BaseEntity
    {
        public DateTime? Birthday { get; set; }
        public string? ImageUrl { get; set; }
        [Display(Name = "Biyografi")]
        public string? Biography { get; set; }
        [Display(Name = "Son Giriş Tarihi")]
        public DateTime? LastLogin { get; set; }
        [Display(Name = "Güvenlik Sorusu")]
        public string? SecurityQuestion { get; set; }
        [Display(Name = "Güvenlik Cevabı")]
        public string? SecurityAnswer { get; set; }
        public User? User { get; set; }
        [Display(Name = "Ülke")]
        public Guid CountryId { get; set; }
        public Country? Country { get; set; }
        [Display(Name = "Rol")]
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
