using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL
{
    public class ConnectionStringManager
    {
        public static string GetConnectionString() {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            return builder.Build().GetSection("ConnectionStrings").GetSection("ConnectionStringPersonal").Value;
        }
    }
}
