using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.Models
{
    public class DBConnection
    {
        public static void ConfigureDbContext(IServiceProvider serviceProvider, DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
        {
            var dbConfig = configuration.GetSection("DBConnect:ConnectString");

            string server = dbConfig["Server"];
            string uid = dbConfig["Uid"];
            string pwd = dbConfig["Pwd"];
            string database = dbConfig["Database"];

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(pwd) || string.IsNullOrEmpty(database))
            {
                throw new Exception("Thiếu thông tin cấu hình database.");
            }

            string connectionString = $"Host={server};Database={database};Username={uid};Password={pwd}";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
