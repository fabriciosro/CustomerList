using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerList.Model.Interfaces
{
  public interface IWorkStation
  {
    IUserRepository UserRepository { get; }

    bool SaveChanges();

  }
}
