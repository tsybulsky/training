using Notes.BLL.DTOModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Notes.App.ViewModels.User
{
    public class AddUserRoleViewModel
    {
        [HiddenInput]
        public int UserId { get; set; }
        [Editable(false)]
        [Display(Name="Пользователь")]
        public string NameOrLogin { get; set; }
        [Display(Name="Роль")]
        [Range(1,1000000)]
        public int SelectedRole { get; set; }
        public IEnumerable<RoleDTO> Roles { get; set; }
    }
}