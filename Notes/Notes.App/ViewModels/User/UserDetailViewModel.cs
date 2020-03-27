using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Notes.BLL.DTOModels;

namespace Notes.App.ViewModels.User
{
    public class UserDetailViewModel
    {
        [Display(Name="Id")]
        public int Id { get; set; }
        [Display(Name="Логин")]
        public string Login { get; set; }
        [Display(Name="Имя пользователя")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }        
        [Display(Name = "Статус")]
        public int Status { get; set; }
        [Display(Name="Дата регистрации")]
        public DateTime CreatedOnDate { get; set; }
        [Display(Name="Последний вход")]        
        public DateTime? LastLogin { get; set; }
        public List<UserRoleDTO> Roles { get; set; }

    }
}