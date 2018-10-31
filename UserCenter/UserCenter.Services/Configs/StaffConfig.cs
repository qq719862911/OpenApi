using System;
using System.Collections.Generic;
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
            this.ToTable("T_Staff");
            this.HasKey(e => e.Id);
            this.Property(e => e.StaffCode).HasMaxLength(50).IsRequired();
            this.Property(e => e.ChName).HasMaxLength(50);
            this.Property(e => e.EnName).HasMaxLength(50);
            this.Property(e => e.Telephone).HasMaxLength(50);
            this.Property(e => e.CALLNumber).HasMaxLength(50);
            this.Property(e => e.DepartmentNumber).HasMaxLength(50);
            this.Property(e => e.JobTitle).HasMaxLength(50);
            // this.HasOptional(e => e.ParentStaff).WithMany().HasForeignKey(s => s.ParentId);
            HasMany(e => e.Childs).WithOptional(e => e.ParentStaff).HasForeignKey(e => e.ParentId);
        }
    }
}
