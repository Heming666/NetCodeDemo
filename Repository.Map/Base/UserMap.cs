using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entity.Models.Base;
using System;

namespace Repository.Map.Base
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {

            #region 用户表 UserEntity
            builder
                        .ToTable("Base_UserInfo")
                        .HasOne(x => x.DeptInfo).WithMany(p => p.Users).HasForeignKey(p=>p.DeptId);
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.HasIndex(x => x.Account).IsUnique();
            builder.HasMany(x => x.ConsumeEntitys).WithOne().OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
