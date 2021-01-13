using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HKParking.Extensions
{
    /// <summary>
    /// Sql帮助类
    /// </summary>
    public static class SqlHelper
    {
        /// <summary>
        /// 生成插入语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="model">待生成脚本的实例</param>
        /// <param name="table">表名称</param>
        /// <param name="filter">过滤列名</param>
        /// <returns></returns>
        public static string GenerateInsert<T>(T model, string table, params string[] filter)
        {
            var type = typeof(T);

            var sbSql = new StringBuilder();
            var sbValues = new StringBuilder();
            sbSql.Append($"INSERT INTO {table}(");
            sbValues.Append($"VALUES(");

            bool isFirstElement = true;
            foreach (var property in type.GetProperties())
            {
                if (filter.Contains(property.Name))
                    continue;

                // 添加逗号
                if (!isFirstElement)
                {
                    sbSql.Append(',');
                    sbValues.Append(',');
                }
                else
                {
                    isFirstElement = false;
                }

                var value = property.GetValue(model);
                // 添加列名
                sbSql.Append($"[{property.Name}]");
                // 添加列值
                if (value == null)
                {
                    sbValues.Append("null");
                }
                else if (property.PropertyType == typeof(string))
                {
                    sbValues.Append($"'{value}'");
                }
                else if (property.PropertyType == typeof(int))
                {
                    sbValues.Append(value.ToString());
                }
                else if (property.PropertyType == typeof(int?))
                {
                    sbValues.Append(value.ToString());
                }
                else if (property.PropertyType == typeof(DateTime))
                {
                    sbValues.Append($"'{((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.fff")}'");
                }
                else if (property.PropertyType == typeof(DateTime?))
                {
                    sbValues.Append($"'{((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.fff")}'");
                }
                else
                {
                    throw new ArgumentException($"不支持属性类型：{property.PropertyType}");
                }
            }
            // 结束
            sbSql.AppendLine(")");
            sbValues.Append(')');
            sbSql.Append(sbValues.ToString());

            return sbSql.ToString();
        }
    }
}
