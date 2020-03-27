using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Notes.BLL.DTOModels;

namespace Notes.App.ViewModels.User
{
    public class UserEditViewModel
    {
        [Display(Name = "Id")]
        [HiddenInput]
        public int Id { get; set; }
        [Display(Name = "Логин")]
        [Required]
        public string Login { get; set; }
        [Display(Name = "Имя пользователя")]
        public string Name { get; set; }
        [Display(Name = "Электронная почта")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }        
        [Display(Name = "Статус")]
        public int Status { get; set; }
        public IEnumerable<UserRoleDTO> Roles { get; set; }
        //public ICollection<Role> Roles { get; }
    }
}