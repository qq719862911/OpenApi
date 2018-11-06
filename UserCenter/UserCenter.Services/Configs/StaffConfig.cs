using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.Services.Models;

namespace UserCenter.Services.Configs
{
    /// <summary>
    /// 员工配置
    /// </summary>
    class StaffConfig : EntityTypeConfiguration<Staff>
    { 
        public StaffConfig()
        {
            this.ToTable("T_Staffs");
            this.HasKey(e => e.Id);
            //给ID配置自动增长
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(e => e.StaffCode).HasMaxLength(50).IsRequired();
            this.Property(e => e.ChName).HasColumnType("nvarchar").HasMaxLength(50);
            this.Property(e => e.EnName).HasMaxLength(50);
            this.Property(e => e.Telephone).HasMaxLength(50);
            this.Property(e => e.CALLNumber).HasMaxLength(50);
          
            this.Property(e => e.JobTitle).HasMaxLength(50);
            this.Property(e => e.Email).HasColumnType("nvarchar").HasMaxLength(50);
            this.Property(e => e.Remark).HasMaxLength(500);

            Property(e => e.CreateDateTime).IsRequired();
            Property(e => e.UpdateTime).IsRequired();
            Property(e => e.Status).IsRequired();
            Property(e => e.UserName).IsRequired();//默认值不在这里配置
            

            // this.HasOptional(e => e.ParentStaff).WithMany().HasForeignKey(s => s.ParentId);//自己关联自己配不配都没事
            HasMany(e => e.Childs).WithOptional(e => e.ParentStaff).HasForeignKey(e => e.ParentId).WillCascadeOnDelete(false);

            this.HasRequired(e => e.Department).WithMany().HasForeignKey(e => e.DepartmentId).WillCascadeOnDelete(false);//员工所属部门

            //配置关系[一个用户只能有一个用户详情！！！] this.HasRequired(s => s.User).WithRequiredDependent(s => s.UserProfile);

           // this.HasOptional(s => s.User).WithOptionalDependent(s => s.Staff);//配置一个员工可以绑定一个用户  //会报错

        }
    }
}
