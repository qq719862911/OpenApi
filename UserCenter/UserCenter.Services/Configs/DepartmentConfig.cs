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
    /// 部门配置
    /// </summary>
    class DepartmentConfig : EntityTypeConfiguration<Department>
    {
        public DepartmentConfig()
        {
            this.ToTable("T_Departments");
            this.HasKey(e => e.Id);
            this.Property(e => e.FullName).IsRequired().HasMaxLength(50);
            this.Property(e => e.CreateDateTime).IsRequired();
            this.Property(e => e.EnName).HasMaxLength(50);
        }
    }
}
