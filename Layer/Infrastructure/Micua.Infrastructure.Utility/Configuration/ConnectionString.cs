namespace Micua.Infrastructure.Utility
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    /// <summary>
    /// 数据库连接字符串类型
    /// </summary>
    public class ConnectionString
    {
        //Data Source=(local);Initial Catalog=master;Persist Security Info=True;User ID=sa;Min Pool Size=1;Max Pool Size=1000
        public ConnectionString()
        {
            MinPoolSize = 1;
            MaxPoolSize = 100;
            ConnectTimeout = 30;
        }

        /// <summary>
        /// 服务器地址
        /// </summary>
        [DisplayName("Data Source")]
        public string DataSource { get; set; }
        /// <summary>
        /// 数据库名
        /// </summary>
        [DisplayName("Initial Catalog")]
        public string InitialCatalog { get; set; }
        /// <summary>
        /// 附加数据库文件名（用于数据库文件形式）
        /// </summary>
        [DisplayName("AttachDbFilename")]
        public string AttachDbFilename { get; set; }
        /// <summary>
        /// 是否集成验证
        /// </summary>
        [DisplayName("Integrated Security")]
        public bool IntegratedSecurity { get; set; }
        /// <summary>
        /// 数据库用户名
        /// </summary>
        [DisplayName("User ID")]
        public string UserID { get; set; }
        /// <summary>
        /// 数据密码
        /// </summary>
        [DisplayName("Password")]
        public string Password { get; set; }
        /// <summary>
        /// 最小池大小
        /// </summary>
        [DisplayName("Min Pool Size")]
        public int MinPoolSize { get; set; }
        /// <summary>
        /// 最大池大小
        /// </summary>
        [DisplayName("Max Pool Size")]
        public int MaxPoolSize { get; set; }
        /// <summary>
        /// 连接超时时间（秒）
        /// </summary>
        [DisplayName("Connect Timeout")]
        public int ConnectTimeout { get; set; }

        /// <summary>
        /// 转换成连接字符串
        /// </summary>
        /// <returns>连接字符串</returns>
        public override string ToString()
        {
            var temp = this.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                .Where(t => t.GetValue(this, null) != null)
                .Select(t => ((DisplayNameAttribute)Attribute.GetCustomAttribute(t, typeof(DisplayNameAttribute))).DisplayName + "=" + t.GetValue(this, null));
            return string.Join(";", temp);
        }
    }
}
