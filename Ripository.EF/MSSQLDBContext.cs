using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Entity.Models.Base;
using Repository.Entity.Models.Consume;
using Repository.Map.Base;
using Repository.Map.Consume;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EF
{
   public class MSSQLDBContext :DbContext
    {
        public MSSQLDBContext()
        {
        }

        public MSSQLDBContext(DbContextOptions<MSSQLDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information)//StreamWriter  记录到文本
                    /*查看详细数据的值。
                    *默认情况下，EF Core 不会将任何数据的值包含在异常消息中。 这是因为此类数据可能是机密的，如果未处理异常，则可能会在生产中显示这些数据。
                    但是，在调试时，知道数据值（特别是对于密钥）会非常有用*/
                    .EnableSensitiveDataLogging()
                    /*出于性能方面的考虑，EF Core 不会在 try-catch 块中包装每次调用以读取数据库提供程序的值。
                    * 但是，这有时会导致难以诊断的异常，尤其是在数据库不允许的情况下，当模型不允许时，它们会返回 NULL。
                    启用 EnableDetailedErrors 将导致 EF 引入这些 try-catch 块，从而提供更详细的错误。*/
                    .EnableDetailedErrors();
                //.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //.HasDefaultValueSql("NOW()");  数据库函数
            //   .HasComputedColumnSql("LEN([LastName]) + LEN([FirstName])", stored: true); 虚拟 计算列，每次从数据库中提取值时都会计算其值。 您还可以指定将计算列 存储 (有时称为 持久化) ，这意味着在每次更新行时计算，并将磁盘与常规列一起存储：

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserMap).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DepartmentMap).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConsumeMap).Assembly);
        }

        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }
        /// <summary>
        /// 部门表
        /// </summary>
        public DbSet<DepartmentEntity> Departments { get; set; }
        /// <summary>
        /// 用户支出明细表
        /// </summary>
        public DbSet<ConsumeEntity> Consumies { get; set; }
    }
}
