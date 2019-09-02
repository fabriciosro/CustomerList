using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerList.Model.Entities
{
  public class User
  {
    public virtual int Id { get; set; }
    public virtual string Login { get; set; }

    public virtual string Password { get; set; }

    public virtual string Name { get; set; }
    public virtual string Email { get; set; }
    public virtual string Hash { get; set; }
    public virtual string Salt { get; set; }
  }
}
