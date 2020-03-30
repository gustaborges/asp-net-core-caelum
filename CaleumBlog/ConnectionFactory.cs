using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CaelumBlog
{
    public static class ConnectionFactory
    {
        public static SqlConnection CriaConexaoAberta()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();
            var strCon = configuration.GetConnectionString("Blog");

            SqlConnection sqlConnection = new SqlConnection(strCon);
            sqlConnection.Open();

            return sqlConnection;
        }
    }
}
