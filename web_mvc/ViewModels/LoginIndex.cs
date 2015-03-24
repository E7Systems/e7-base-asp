
using System.ComponentModel.DataAnnotations;
namespace web_mvc.ViewModels
{
    public class LoginIndex
    {
        [Required, MaxLength(128)]
        public string UserName { get; set; }
        
        [Required,DataType(DataType.Password), MaxLength(128)]
        public string Password { get; set; }
    }
}