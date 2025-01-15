using System.ComponentModel.DataAnnotations;

namespace ClienteMVC.Models.ViewModels
{
    public class RedirectViewModel
    {
     //public LoginViewModel Login { get; set; }
     public Cliente Cliente { get; set; }

        public string Referer { get; set; }
    }
}
