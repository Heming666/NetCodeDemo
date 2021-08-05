using Microsoft.EntityFrameworkCore;
using Repository.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Models.Consume
{
    [Table("User_ConsumeEntity"), Comment("消费支出明细表"), Index(nameof(ID), Name = "Index_ID")]
    public class ConsumeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "int")]
        public int ID { get; set; }
        [Display(Name = "消费名称")]
        [Comment("消费名称"), Required(ErrorMessage = "消费明细不能为空"), MaxLength(500)]
        public string ConsumeName { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [Display(Name = "金额")]
        [Comment("金额"), Required(ErrorMessage = "消费名称不能为空")]
        public decimal Amount { get; set; }

        [Display(Name = "消费地点")]
        [Comment("消费地点"),MaxLength(100)]
        public string Place { get; set; }

        [Display(Name = "备注")]
        [Comment("备注"),MaxLength(500)]
        public string Remark { get; set; }

        [Display(Name = "分类")]
        [Comment("分类")]
        public Classify Classify { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Comment("创建时间")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 消费时间
        /// </summary>
        [Display(Name = "消费时间"),DataType(DataType.DateTime,ErrorMessage ="请输入时间类型"),DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        [Comment("消费时间")]
        public DateTime LogTime { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }

    public enum Classify
    {
        饮食,
        穿搭,
        日用,
        房租,
        出行通讯,    
        娱乐,
        医疗保健
    }
}
