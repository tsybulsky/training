using System.ComponentModel.DataAnnotations;

namespace Notes.App.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        [System.Web.Mvc.HiddenInput]
        public int Id { get; set; }
        [Display(Name = "Имя пользователя")]
        [Required]
        [Editable(false)]
        public string Login { get; set; }
        
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }
        [Display(Name = "Новый Пароль")]       
        public string NewPassword { get; set; }
        [Display(Name = "Подтвержение пароля")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}