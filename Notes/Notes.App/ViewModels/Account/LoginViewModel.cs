using System.ComponentModel.DataAnnotations;

namespace Notes.App.ViewModels.Account
{
    public class LoginViewModel
    {
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }        
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]        
        public string Password { get; set; }
    }
}