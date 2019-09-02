using System;
using System.Collections.Generic;

namespace CustomerList.Models
{
  public class UserRole
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsAdmin { get; set; }
  }
}
