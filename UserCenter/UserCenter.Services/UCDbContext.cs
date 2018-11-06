using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserCenter.Services.Models;

namespace UserCenter.Services
{
   public class UCDbContext:DbContext
    {
        public UCDbContext():base("connstr")
        {
            Database.SetInitializer<UCDbContext>(null);
            //Database.CreateIfNotExists();

            this.Database.Log = MyDBLog; //记录ef执行的失去了
        }
        /// <summary>
        /// 开发用
        /// </summary>
        /// <param name="sql"></param>
        private void MyDBLog(string sql)
        {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(sql);
            //using (FileStream fsWrite = new FileStream(@"D:\1.txt", FileMode.Append))
            //{
            //    fsWrite.Write(myByte, 0, myByte.Length);
            //};
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<AppInfo> AppInfos { get; set; }
        public DbSet<Staff> Staffs { get; set; }
    }
}
