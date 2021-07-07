using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entity.Models.Consume;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Map.Consume
{
    public class ConsumeMap : IEntityTypeConfiguration<ConsumeEntity>
    {
        public void Configure(EntityTypeBuilder<ConsumeEntity> builder)
        {

            #region 消费支出明细表
            builder.ToTable("User_ConsumeEntity").HasOne(x => x.User).WithMany(x => x.ConsumeEntitys).HasForeignKey(p=>p.UserId);
            builder.Property(x => x.Amount).HasColumnType("decimal(8,2)");
            #endregion
        }
    }
}
