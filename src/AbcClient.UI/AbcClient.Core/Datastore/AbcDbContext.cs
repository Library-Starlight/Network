using AbcClient.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcClient.Core.Datastore
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class AbcDbContext : DbContext
    {
        #region 公共属性

        /// <summary>
        /// 客户端列表
        /// </summary>
        public DbSet<AwesomeClient> Client { get; set; }

        /// <summary>
        /// 文件夹列表
        /// </summary>
        public DbSet<Folder> Folder { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="options"></param>
        public AbcDbContext(DbContextOptions<AbcDbContext> options) : base(options) { }

        #endregion

        #region 重写方法

        /// <summary>
        /// 自定义模型
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 基础设施初始化
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
