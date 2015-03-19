
using System.ComponentModel.DataAnnotations;
namespace web_mvc.ViewModels
{
    public class LoginIndex
    {
        [Required]
        public string UserName { get; set; }
        
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
    }
}