using CustomerList.Model.Entities;
using CustomerList.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerList.Data.Repositories
{
  public class UserRepository : GenericRepository<User>, IUserRepository
  {
    public UserRepository(ApplicationContext context)
      : base(context)
    {
    }
  }
}
