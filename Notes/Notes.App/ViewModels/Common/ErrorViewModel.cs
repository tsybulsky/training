using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notes.App.ViewModels.Common
{
    public class ErrorViewModel
    {
        public ErrorViewModel(string message)
        {
            Message = message;
        }
        public string Message { get; set; }
    }
}