using Microsoft.EntityFrameworkCore;
using Repository.Entity.Models.Consume;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Models.Base
{
    [Comment("用户信息表")]
    [Table("Base_UserInfo"),Index(nameof(Account), Name = "Index_Account")]
   public class UserEntity
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//数据库自增长
        [Comment("主键")]
        [Column(TypeName="int" )]
        public int ID { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        [Required(ErrorMessage ="账号不能为空"),MinLength(6),MaxLength(24), Comment("账户"), Display(Name = "账号")]
        //[ConcurrencyCheck]并发标记
        public string  Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空"), MinLength(6), MaxLength(24), Comment("密码"),DataType(DataType.Password), Display(Name = "密码")]
        public string  PassWord { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [Required(ErrorMessage = "用户昵称不能为空"), MinLength(1), MaxLength(24), Comment("用户昵称"), Display(Name = "昵称")]
        public string  UserName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>2
        [Column(TypeName = "int"), Comment("性别"),Display(Name ="性别")]
        public Gender? Gender { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        [Comment("照片"),MaxLength(500),DataType(DataType.ImageUrl), Display(Name = "照片")]
        public string  Photo { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [RegularExpression("^1[34578]\\d{9}$",ErrorMessage ="请输入正确的手机号"),MaxLength(11),Comment("手机号"), DataType(DataType.PhoneNumber), Display(Name = "手机号")]
        public string  Phone { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime  ModifyDate { get; set; }

        [ForeignKey("DeptId"), Display(Name = "部门")]
        public int DeptId { get; set; }
        public DepartmentEntity DeptInfo { get; set; }

        public List<ConsumeEntity> ConsumeEntitys { get; set; }
    }

    public enum Gender
    {
        男,女
    }
}
