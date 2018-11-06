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
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            ToTable("T_Users");
            this.HasKey(s => s.Id);
            //给ID配置自动增长
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasMany(e => e.Groups).WithMany(e => e.Users)
                .Map(e => e.ToTable("T_GroupUsers")
                .MapLeftKey("UserId").MapRightKey("GroupId"));
            this.Property(e => e.NickName).HasColumnType("nvarchar").HasMaxLength(20).IsRequired();
            this.Property(e => e.PasswordHash).HasMaxLength(100).IsRequired();
            this.Property(e => e.PasswordSalt).HasMaxLength(20).IsRequired();
            this.Property(e => e.PhoneNum).HasMaxLength(50).IsRequired();
             
           
        }
    }
}
