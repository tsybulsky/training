using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Validataion;
namespace Notes.PL
{
    public class ChangePasswordModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [Display(Name = "Имя пользователя")]
        [Required]
        [Editable(false)]
        public string UserName { get; set; }
        [Display(Name = "Пароль")]
        public string NewPassword { get; set; }
        [Display(Name = "Подтвержение пароля")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
