using CustomerList.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerList.Data.Mapping
{
  public class UserConfig : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.ToTable("UserSys");
      builder.HasKey(t => t.Id);
      builder.Property(t => t.Id);
      builder.Property(t => t.Name);
      builder.Property(t => t.Email);
      builder.Property(t => t.Login);
      builder.Property(t => t.Hash);
      builder.Property(t => t.Salt);
    }
  }
}
