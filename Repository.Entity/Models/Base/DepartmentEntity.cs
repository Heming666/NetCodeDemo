﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Models.Base
{
    [Table("Base_Department"), Comment("部门表"), Index(nameof(ID), Name = "Index_ID")]
    public class DepartmentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("DeptId", TypeName = "int")]
        public int ID { get; set; }

        
        [Required(ErrorMessage = "部门名称不能为空"),MaxLength(50),Display(Name ="部门名称")]
        public string DeptName { get; set; }

        [MaxLength(50), Display(Name = "部门编码")]
        public string DeptCode { get; set; }

        [ Display(Name = "创建时间")]
        public DateTime  CreateDate { get; set; }
        [Display(Name = "修改时间")]
        public DateTime ModifyDate { get; set; }


        public List<UserEntity> Users { get; set; }
    }
}
