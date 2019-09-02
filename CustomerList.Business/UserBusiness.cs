using CustomerList.Model.Dtos;
using CustomerList.Model.Entities;
using CustomerList.Model.Interfaces;
using CustomerList.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CustomerList.Business
{
  public class UserBusiness : IUserBusiness
  {
    private readonly IWorkStation _workStation;

    public UserBusiness(IWorkStation workStation)
    {
      this._workStation = workStation;
    }

    public virtual UserDto Authenticate(LoginDto loginDto)
    {
      var user = this._workStation
          .UserRepository
          .Get(q => q.Login.ToLower().Equals(loginDto.UserName))
          .FirstOrDefault();

      if (!SecurityUtils.Validate(loginDto.PassWord, user.Salt, user.Hash))
        return null;

      return new UserDto
      {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Login = user.Login
      };
    }

    public IEnumerable<UserDto> Fill()
    {
      var query = this._workStation
          .UserRepository
          .Get(null, o => o.OrderBy(u => u.Name))
          .Select(s => new UserDto
          {
            Id = s.Id,
            Name = s.Name,
            Email = s.Email,
            Login = s.Login
          });
      return query.ToList();
    }

    public UserDto Select(int id)
    {
      var user = this._workStation
          .UserRepository
          .GetById(id);

      return new UserDto
      {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Login = user.Login
      };
    }

    public ResultDto Delete(int id)
    {
      this._workStation.UserRepository.Delete(id);
      var sucess = this._workStation.SaveChanges();
      return new ResultDto
      {
        Sucess = sucess
      };
    }

    public ResultDto Save(UserDto userDto)
    {
      var user = new User();

      if (userDto.Id > 0)
      {
        user = this._workStation.UserRepository.GetById(userDto.Id);
        user.Name = userDto.Name;
        user.Email = userDto.Email;

        this._workStation.UserRepository.Update(user);
      }
      else
      {
        var salt = SecurityUtils.CreateSalt();
        var hash = SecurityUtils.CreateHash(userDto.Password, salt);

        user = new User
        {
          Name = userDto.Name,
          Email = userDto.Email,
          Login = userDto.Login,
          Hash = hash,
          Salt = salt
        };

        this._workStation.UserRepository.Add(user);
      }

      var sucess = this._workStation.SaveChanges();

      var result = new ResultDto
      {
        Sucess = sucess,
        Id = user.Id
      };

      return result;
    }

    //txtforEN is PlainText
    //Key is Public Secret Key 
    public string Encryption(string Text, string Key)
    {

      byte[] MsgBytes = Encoding.UTF8.GetBytes(Text);
      byte[] KeyBytes = Encoding.UTF8.GetBytes(Key);

      KeyBytes = SHA256.Create().ComputeHash(KeyBytes);

      byte[] bytesEncrypted = AES_Encryption(MsgBytes, KeyBytes);

      return Convert.ToBase64String(bytesEncrypted);
    }

    public byte[] AES_Encryption(byte[] Msg, byte[] Key)
    {
      byte[] encryptedBytes = null;

      //salt is generated randomly as an additional number to hash password or message in order o dictionary attack
      //against pre computed rainbow table
      //dictionary attack is a systematic way to test all of possibilities words in dictionary wheather or not is true?
      //to find decryption key
      //rainbow table is precomputed key for cracking password
      // Set your salt here, change it to meet your flavor:
      // The salt bytes must be at least 8 bytes.  == 16 bits
      byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

      using (MemoryStream ms = new MemoryStream())
      {
        using (RijndaelManaged AES = new RijndaelManaged())
        {
          AES.KeySize = 256;
          AES.BlockSize = 128;

          var key = new Rfc2898DeriveBytes(Key, saltBytes, 1000);
          AES.Key = key.GetBytes(AES.KeySize / 8);
          AES.IV = key.GetBytes(AES.BlockSize / 8);

          AES.Mode = CipherMode.CBC;

          using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
          {
            cs.Write(Msg, 0, Msg.Length);
            cs.Close();
          }
          encryptedBytes = ms.ToArray();
        }
      }

      return encryptedBytes;
    }

    public string Decryption(string Text2, string Key2)
    {
      // Convert String to Byte
      byte[] MsgBytes = Convert.FromBase64String(Text2);
      byte[] KeyBytes = Encoding.UTF8.GetBytes(Key2);
      KeyBytes = SHA256.Create().ComputeHash(KeyBytes);

      byte[] bytesDecrypted = AES_Decryption(MsgBytes, KeyBytes);

      string decryptionText = Encoding.UTF8.GetString(bytesDecrypted);

      return decryptionText;
    }

    public byte[] AES_Decryption(byte[] Msg, byte[] Key)
    {
      byte[] decryptedBytes = null;

      // Set your salt here, change it to meet your flavor:
      // The salt bytes must be at least 8 bytes.
      byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

      using (MemoryStream ms = new MemoryStream())
      {
        using (RijndaelManaged AES = new RijndaelManaged())
        {
          AES.KeySize = 256;
          AES.BlockSize = 128;

          var key = new Rfc2898DeriveBytes(Key, saltBytes, 1000);
          AES.Key = key.GetBytes(AES.KeySize / 8);
          AES.IV = key.GetBytes(AES.BlockSize / 8);

          AES.Mode = CipherMode.CBC;

          using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
          {
            cs.Write(Msg, 0, Msg.Length);
            cs.Close();
          }
          decryptedBytes = ms.ToArray();
        }
      }

      return decryptedBytes;
    }

  }
}
