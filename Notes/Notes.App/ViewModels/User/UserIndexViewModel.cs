using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Notes.App.ViewModels.User
{
    public class UserIndexViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Логин")]
        public string Login { get; set; }      

        [Display(Name="Имя пользователя")]
        public string Name { get; set; }
        [Display(Name="Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name="Последний вход")]
        [DataType(DataType.DateTime)]
        public DateTime? LastLogin { get; set; }
        [Display(Name="Дата регистрации")]
        public DateTime CreatedOnDate { get; set; }
    }
}