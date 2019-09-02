using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerList.Model.Dtos
{
  public class UserDto
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public string Login { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public int UserRoleId { get; set; }
  }
}
