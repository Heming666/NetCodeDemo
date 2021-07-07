using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Map.Base
{
    public class DepartmentMap : IEntityTypeConfiguration<DepartmentEntity>
    {
        public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
        {
            #region 部门表
            builder.ToTable("Base_Department").HasMany(x => x.Users).WithOne().OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
