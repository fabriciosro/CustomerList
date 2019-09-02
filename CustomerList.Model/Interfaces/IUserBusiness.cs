using CustomerList.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerList.Model.Interfaces
{
  public interface IUserBusiness
  {
    UserDto Authenticate(LoginDto loginDto);
    IEnumerable<UserDto> Fill();
    UserDto Select(int id);
    ResultDto Delete(int id);
    ResultDto Save(UserDto user);
  }

}
