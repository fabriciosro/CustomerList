using System;
using System.Collections.Generic;

namespace CustomerList.Models
{

  public class UserSys
  {
    public int Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public int UserRoleId { get; set; }
  }
}
