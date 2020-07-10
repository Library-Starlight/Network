using AbcClient.Core.Datastore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcClient.Core.Migrations
{
    /// <summary>
    /// 指定数据库上下文的设计时工厂
    /// </summary>
    public class AbcContextFactory : IDesignTimeDbContextFactory<AbcDbContext>
    {
        public AbcDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AbcDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=AbcClientDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new AbcDbContext(optionsBuilder.Options);
        }
    }
}
