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

        [Comment("消费名称"), Required(ErrorMessage = "消费明细不能为空"), MaxLength(500)]
        public string ConsumeName { get; set; }
        [Comment("金额"), Required(ErrorMessage = "消费名称不能为空")]
        public decimal Amount { get; set; }

        [Comment("消费地点"),MaxLength(100)]
        public string Place { get; set; }

        [Comment("备注"),MaxLength(500)]
        public string Remark { get; set; }

        [Comment("分类")]
        public Classify Classify { get; set; }

        [Comment("创建时间")]
        public DateTime CreateTime { get; set; }
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
