using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerList.Model.Dtos
{
  public class LoginDto
  {
    [Required(AllowEmptyStrings = false, ErrorMessage = "Inform the user")]
    public string UserName { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Inform the password")]
    public string PassWord { get; set; }
  }
}
