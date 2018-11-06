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
   public class AppInfoConfig:EntityTypeConfiguration<AppInfo>
    {
        public AppInfoConfig()
        {
            ToTable("T_AppInfos");
            HasKey(e => e.Id);
            //给ID配置自动增长
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.Name).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
            Property(e => e.AppKey).HasMaxLength(100).IsRequired();
            Property(e => e.AppSecret).HasMaxLength(100).IsRequired();
            Property(e => e.IsEnabled).IsRequired();
        }
    }
}
