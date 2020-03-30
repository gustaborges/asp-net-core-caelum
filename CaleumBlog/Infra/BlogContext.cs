using CaelumBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CaelumBlog.Infra
{
    public class BlogContext : DbContext
    {

        public BlogContext(DbContextOptions contextOptions) : base(contextOptions)
        {
            /* Agora em BlogContext é injetada a instância de DbContextOptions, configurado,
            e passado para sua superclasse.
             */
        }

        public DbSet<Post> Posts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //// construtor de configuração
            //IConfigurationBuilder confBuilder = new ConfigurationBuilder()
            //        .SetBasePath(Directory.GetCurrentDirectory())
            //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange:true)
            //        .AddEnvironmentVariables();

            //// IConfiguraiton contem chaves e valores
            //IConfiguration configuration = confBuilder.Build();
            
            //string stringConexao = configuration.GetConnectionString("Blog");
            //// optionsBuilder é recebido como argumento
            //optionsBuilder.UseSqlServer(stringConexao);
        }
    }
}
