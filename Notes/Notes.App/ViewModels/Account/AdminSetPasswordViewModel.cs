using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notes.App.ViewModels.Account
{
    public class AdminSetPasswordViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string NewPassword { get; set; }        
    }
}