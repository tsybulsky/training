using Notes.BLL.DTOModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Notes.App.ViewModels.User
{
    public class CreateUserViewModel
    {
        [Display(Name="Логин")]
        [Required]
        public string Login { get; set; }
        [Display(Name="Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name="Подтверждение пароля")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Display(Name="Имя пользователя")]
        public string Name { get; set; }
        [Display(Name="Электронная почта")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string EMail { get; set; }
        [Display(Name="Статус")]
        public int Status { get; set; }
        [Display(Name="Роль")]
        public int Role { get; set; }
        public IEnumerable<RoleDTO> Roles { get; set; }
    }
}